using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{

    public class ScoreKeeper : MonoBehaviour
    {

        List<int> scores;
        List<Character> characters;

        public List<int> Scores
        {
            get { return scores; }
        }

        public List<Character> Characters
        {
            get { return characters; }
        }
        

        // Use this for initialization
        void Start() {
            DontDestroyOnLoad(gameObject);
            scores = new List<int>();
            characters = new List<Character>();
        }

        public void RecordScore() {
            for (int i = 0; i < PlayersManager.Instance.Players.Count; i++) {
                scores.Add(PlayersManager.Instance.Players[i].Score);
                characters.Add(PlayersManager.Instance.Players[i].Character);
            }
        }

        public void AddScore(Player player) {
            scores.Add(player.Score);
            characters.Add(player.Character);
        }
    }
}