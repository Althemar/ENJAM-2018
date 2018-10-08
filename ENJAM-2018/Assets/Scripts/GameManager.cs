using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor.Animations;

namespace ENJAM2018
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public ScoreKeeper scoreKeeper;

        public RuntimeAnimatorController testAnimator;
        public Character testCharacter;

                                
        public GameObject PlayerPrefab;
        public GameObject PlayerScoreUIPrefab;
        public GameObject Level;

        public int NumberOfPlayers;
        public float XspaceBetweenPlayers;
        public float YspaceBetweenPlayers;

        public enum GameStates
        {
            beginning,
            playing,
            ending
        }

        GameStates gameState = GameStates.beginning;

        public GameStates GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }


        }


        private void Start() {


            SoundManager.Instance.PlayUnlocalized("Game");
            GameObject selectedPlayersGo = GameObject.Find("SelectedPlayersKeeper");
            SelectedPlayersKeeper selectedPlayers = null;
          
            if (selectedPlayersGo) {
                selectedPlayers = selectedPlayersGo.GetComponent<SelectedPlayersKeeper>();
                NumberOfPlayers = selectedPlayers.SelectedCharacters.Count;
            }
           
            float yPos, xPos;
            
            if (NumberOfPlayers % 2 == 0) {
                yPos = 0 + YspaceBetweenPlayers * (NumberOfPlayers / 2) - YspaceBetweenPlayers / 2;
                xPos = 0 - XspaceBetweenPlayers * ( NumberOfPlayers / 2 ) + XspaceBetweenPlayers / 2 ;
               
            }
            else if (NumberOfPlayers > 1){
                yPos = 0 + YspaceBetweenPlayers * (NumberOfPlayers - 2);
                xPos = 0 - XspaceBetweenPlayers * (NumberOfPlayers - 2);

            }
            else {
                yPos = 0;
                xPos = 0;
            }

            if (XspaceBetweenPlayers == 0) {
                xPos = 0;
            }
            yPos += Level.GetComponent<LevelSequence>().TileLineYPos + 0.6f;

            int playerId = 0;

            for (int i = 0; i < NumberOfPlayers; i++) {
                Player player = Instantiate(PlayerPrefab, Level.transform).GetComponent<Player>();
                player.transform.position = new Vector3(0, yPos, 0);
                player.XOffset = xPos;

                yPos -= YspaceBetweenPlayers;
                xPos += XspaceBetweenPlayers;


                player.GetComponent<PlayerController>().Owner = (PlayerController.Players)playerId;
                playerId++;
                player.scoreUI = UIManager.Instance.CreatePlayerScoreUI(PlayerScoreUIPrefab);
                

                Animator animator = player.GetComponent<Animator>();
                if (selectedPlayers) {
                    animator.runtimeAnimatorController = (RuntimeAnimatorController)selectedPlayers.SelectedCharacters[i].animator;
                    float scale = player.transform.localScale.x;
                    player.Character = selectedPlayers.SelectedCharacters[i];
                    player.transform.localScale = new Vector3(scale * player.Character.adaptSize, scale * player.Character.adaptSize, 1);
                    player.scoreUI.SetName(player.Character.name);

                }
                else {
                    animator.runtimeAnimatorController = (RuntimeAnimatorController) testAnimator;
                    player.Character = testCharacter ;
                    

                }


                PlayersManager.Instance.Players.Add(player);
            }

            if (selectedPlayers) {
                Destroy(selectedPlayersGo);
            }
        }

        public void KillPlayer(Player player) {
            scoreKeeper.AddScore(player);
            EndGame();
        }

        public void EndGame() {
            for (int i = 0; i < PlayersManager.Instance.Players.Count; i++) {
                if (!PlayersManager.Instance.Players[i].Lost) {
                    return;
                }
            }
            gameState = GameStates.ending;

            UIManager.Instance.DisplayEndingText();
            StartCoroutine(LoadingLeaderboard());
        }

        public IEnumerator LoadingLeaderboard() {
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene("LeaderBoard");
        }
    }
}

