using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class Player : MonoBehaviour
    {
        public PlayerScoreUI scoreUI;

        bool playing = true;
        bool moving;
        bool movingBack;
        float moveProgress;
        float moveSpeed;

        bool lost;

        int score;
        int scoreMultiplicator = 1;
        int combo;

        bool punched;
        bool canPunch = true;
        float punchProgress;
        
        PlayersManager playerManager;
        PlayerController playerController;
        CameraShake cameraShake;
        Animator animator;
        Character character;

        private SequenceTile tile;
        private LevelSequence level;
		private ParticleSystem particleSystemDash;
		private ParticleSystem particleSystemFail;
		private ParticleSystem particleSystemCombo;

        float xOffset;

		public bool Lost
        {
            get { return lost; }
        }

        public bool Playing
        {
            get { return playing; }
            set { playing = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Character Character
        {
            get { return character; }
            set { character = value; }
        }

        public SequenceTile Tile
        {
            get { return tile; }
        }

        public bool MovingBack
        {
            get { return movingBack; }
        }

        public float MovingProgress
        {
            get { return moveProgress; }
        }

        public bool Moving
        {
            get { return moving; }
        }

        public bool Punched
        {
            get { return punched; }
            set { punched = value; }
        }

        public float XOffset
        {
            get { return xOffset; }
            set { xOffset = value; }
        }
        
        private void Start() {
			particleSystemDash = transform.Find("ParticlesDash").GetComponent<ParticleSystem>();
			particleSystemFail = transform.Find("ParticlesFail").GetComponent<ParticleSystem>();
			particleSystemCombo = transform.Find("ParticlesCombo").GetComponent<ParticleSystem>();
			ParticleSystem.MainModule mainMod = particleSystemDash.main;
			mainMod.simulationSpace = ParticleSystemSimulationSpace.Custom;
			mainMod.customSimulationSpace = transform.parent;

            playerManager = GetComponentInParent<PlayersManager>();
            playerController = GetComponent<PlayerController>();
            level = GetComponentInParent<LevelSequence>();
            cameraShake = Camera.main.GetComponent<CameraShake>();
            animator = GetComponent<Animator>();

            scoreUI.Player = this;
            scoreUI.PlayerString = playerController.PlayerString;

            tile = level.Tiles[playerManager.beginTile];
            tile.AddPlayerOnTile(this);
            float tileX = tile.gameObject.transform.position.x;
            transform.position = new Vector3(tileX + xOffset, transform.position.y, transform.position.z);
        }

		private void Update() {
			if (Input.GetKeyDown(KeyCode.A) && tile.next != null) {
				Move(true);
				IncreaseScore();
			}
		}

		void FixedUpdate() {
			ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystemDash.particleCount];
			particleSystemDash.GetParticles(particles);
			for (int i = 0; i < particles.Length; i++) {
				particles[i].position = transform.localPosition;
			}
			particleSystemDash.SetParticles(particles, particles.Length);
			
            if (!playing || lost || GameManager.Instance.GameState == GameManager.GameStates.ending) {
                return;
            }

            if (moving) {
                moveProgress += moveSpeed * Time.fixedDeltaTime;
                Vector3 moveTarget = new Vector3(tile.gameObject.transform.position.x + xOffset, transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, moveTarget, moveProgress);

                if (moveProgress >= 1) {
                    moving = false;
                    movingBack = false;
                    punched = false;
                    animator.SetBool("Is Moving", false);
                    moveProgress = 0;
                }
            }
            if (!canPunch) {
                punchProgress += 1 / playerManager.timeBetweenPunch * Time.fixedDeltaTime;
                if (punchProgress >= 1) {
                    canPunch = true;
                    punchProgress = 0;
                    Debug.Log("Can Punch");
                }
            }
        }

        void OnBecameInvisible() {
            Lose();
        }

        public void Move(bool goForward) {
            moveProgress = 0;
            animator.SetBool("Is Moving", true);
            moving = true;
            movingBack = !goForward;

            tile.RemovePlayerFromTile(this);
            if (goForward) {
				if (tile.next == null) return;
                tile = tile.next;
                moveSpeed = playerManager.DashSpeed;
				particleSystemDash.Emit(1); // Particles
            }
            else {
				if (tile.previous == null) return;
                tile = tile.previous;
                moveSpeed = playerManager.MovebackSpeed;
				particleSystemFail.Play(); // Particles
            }
            tile.AddPlayerOnTile(this);
        }

        public void IncreaseScore() {
            int tilePosition = level.TilePosition(tile);
            int totalTile = level.Tiles.Length;
            if (tilePosition <= totalTile - playerManager.bestTiles) {
                score += playerManager.basicScore * scoreMultiplicator;
            }
            else {
                score += playerManager.bestTilesScore * scoreMultiplicator ;
            }
            scoreUI.SetScore(score);
            combo++;
            scoreUI.SetCombo(combo);
            if (combo >= playerManager.comboMax) {
                StartCoroutine(cameraShake.Shake(0.15f, .3f));

                combo = 0;
                scoreMultiplicator++;
                scoreUI.SetMultiplicator(scoreMultiplicator);
				particleSystemCombo.Play(); // Particles
			}
        }

        public void CheckInput(int keyId) {
            if (lost) {
                return;
            }

            if (keyId <= 3) {
                if (tile.next != null && keyId == tile.next.requiredInput.inputKey ){//&& level.TilePosition(tile) < 10) {
                    Move(true);
                    IncreaseScore();
                }
                else if (keyId != tile.next.requiredInput.inputKey) {
                    Move(false);
                    movingBack = true;
                    combo = 0;
                    scoreMultiplicator = 1;
                    scoreUI.SetCombo(combo);
                    scoreUI.SetMultiplicator(scoreMultiplicator);
                }
            }
            
        }

        public void Punch() {
            if (!punched || moveProgress > 0.9) {
                /*
                List<Player> others = playerManager.GetOtherPlayersOnTile(this);
                if (others.Count == 0) {
                    return;
                }
                for (int i = 0; i < others.Count; i++) {
                    if ((others[i].MovingBack && moveProgress > 0.9) || (!others[i].MovingBack && moveProgress < 0.2) || (!others[i].Moving)) {
                        others[i].Move(false);
                        scoreMultiplicator--;
                        scoreUI.SetMultiplicator(scoreMultiplicator);
                        others[i].Punched = true;
                    }
                }*/
                animator.SetTrigger("Punch");
                canPunch = false;
                punchProgress = 0f;

            }
        }

        public void Lose() {
            if (!lost) {
                lost = true;
                GameManager.Instance.KillPlayer(this);
            }
            
        }

    }
}
