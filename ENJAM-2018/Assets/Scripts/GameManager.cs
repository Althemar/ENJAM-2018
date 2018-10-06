using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Animations;

namespace ENJAM2018
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public ScoreKeeper scoreKeeper;

        public AnimatorController testAnimator;


        public int NumberOfPlayers;
        public GameObject PlayerPrefab;
        public GameObject PlayerScoreUIPrefab;
        public GameObject Level;
        public float spaceBetweenPlayers;

        public enum GameStates
        {
            beginning,
            playing,
            ending
        }

        GameStates gameState = GameStates.playing;

        public GameStates GameState
        {
            get { return gameState; }
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

            float yPos;
            if (NumberOfPlayers % 2 == 0) {
                yPos = 0 + NumberOfPlayers / 2 - spaceBetweenPlayers / 2;
               
            }
            else if (NumberOfPlayers > 1){
                yPos = 0 + spaceBetweenPlayers * (NumberOfPlayers - 2);
            }
            else {
                yPos = 0;
            }

            int playerId = 0;

            for (int i = 0; i < NumberOfPlayers; i++) {
                Player player = Instantiate(PlayerPrefab, Level.transform).GetComponent<Player>();
                player.transform.position = new Vector3(0, yPos, 0);
                yPos -= spaceBetweenPlayers;

                player.GetComponent<PlayerController>().Owner = (PlayerController.Players)playerId;
                playerId++;
                player.scoreUI = UIManager.Instance.CreatePlayerScoreUI(PlayerScoreUIPrefab);

                Animator animator = player.GetComponent<Animator>();
                animator.runtimeAnimatorController = (RuntimeAnimatorController)testAnimator;

                PlayersManager.Instance.Players.Add(player);
            }
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

            scoreKeeper.RecordScore();

        }

        public IEnumerator LoadingLeaderboard() {
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene("LeaderBoard");
        }
    }
}

