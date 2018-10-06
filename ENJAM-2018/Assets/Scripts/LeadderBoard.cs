using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ENJAM2018
{

    public class LeadderBoard : MonoBehaviour
    {

        public GameObject panel;

        // Score Pair ##
        //  - Name
        //  - Score
        public GameObject scorePairPrefab;

        ScoreKeeper scoreKeeper;

        void Start() {

            scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();

            for (int i = 0; i < scoreKeeper.Scores.Count; i++) {
                Vector3 pos = new Vector3(0, -((i) * 20), 0);
                GameObject scorePair = Instantiate(scorePairPrefab);
                scorePair.transform.SetPositionAndRotation(pos, Quaternion.identity);
                scorePair.transform.SetParent(panel.transform, false);

                scorePair.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Player " + i;
                scorePair.transform.GetChild(1).gameObject.GetComponent<Text>().text = scoreKeeper.Scores[i].ToString(); ;
            }
        }
    }
}
