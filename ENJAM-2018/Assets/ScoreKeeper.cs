using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{

    public class ScoreKeeper : MonoBehaviour
    {

        List<int> scores;

        public List<int> Scores
        {
            get { return scores; }
        }

        // Use this for initialization
        void Start() {
            DontDestroyOnLoad(gameObject);
            scores = new List<int>();
        }

        public void RecordScore() {
            for (int i = 0; i < PlayersManager.Instance.players.Count; i++) {
                scores.Add(PlayersManager.Instance.players[i].Score);
            }
        }
    }
}