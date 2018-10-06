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
        float moveProgress;
        float moveSpeed;

        bool lost;

        int score;
        int scoreMultiplicator = 1;
        int combo;
        
        PlayersManager playerManager;
        PlayerController playerController;
        CameraShake cameraShake;
        Animator animator;

        private SequenceTile tile;
        private LevelSequence level;

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
        
        private void Start() {
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
            transform.position = new Vector3(tileX, transform.position.y, transform.position.z);
        }

        void FixedUpdate() {

            if (!playing || GameManager.Instance.GameState == GameManager.GameStates.ending) {
                return;
            }

            if (moving) {
                moveProgress += moveSpeed * Time.fixedDeltaTime;
                Vector3 moveTarget = new Vector3(tile.gameObject.transform.position.x, transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, moveTarget, moveProgress);

                if (moveProgress >= 1) {
                    moving = false;
                    animator.SetBool("Is Moving", false);
                    moveProgress = 0;
                }
            }
        }

        public void Move(bool goForward) {
            moveProgress = 0;
            animator.SetBool("Is Moving", true);
            moving = true;

            tile.RemovePlayerFromTile(this);
            if (goForward) {
                tile = tile.next;
                moveSpeed = playerManager.DashSpeed;
            }
            else {
                tile = tile.previous;
                moveSpeed = playerManager.MovebackSpeed;
                
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
            }
        }

        public void CheckInput(int keyId) {
            if (lost) {
                return;
            }

            if (keyId == tile.next.requiredInput.inputKey && level.TilePosition(tile) < 10) {
                Move(true);
                IncreaseScore();
            }
            else if (keyId != tile.next.requiredInput.inputKey) {
                Move(false);
                combo = 0;
                scoreMultiplicator = 1;
                scoreUI.SetCombo(combo);
                scoreUI.SetMultiplicator(scoreMultiplicator);
            }
        }

        public void Lose() {
            lost = true;
            GameManager.Instance.EndGame();
        }

    }
}
