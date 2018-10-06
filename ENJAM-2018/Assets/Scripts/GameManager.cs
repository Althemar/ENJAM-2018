using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        PlayersManager playersManager;

        public enum GameState
        {
            beginning,
            playing,
            ending
        }

        public GameState gameState = GameState.playing;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void EndGame() {
            gameState = GameState.ending;
            
            // Return if not all players have lost
            for (int i = 0; i < playersManager.players.Count; i++) {
                if (!playersManager.players[i].Lost) {
                    return;
                }
            }

            UIManager.Instance.DisplayEndingText();
        }

        public IEnumerator LoadingLeaderboard() {
            yield return new WaitForSeconds(4);
            // Load leaderboard scene

        }

        
    }
}

