using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ENJAM2018
{

    public class LeadderBoard : MonoBehaviour
    {

       public List<PlayerPodium> playerPodiums;

        ScoreKeeper scoreKeeper;

        void Start() {

            SoundManager.Instance.PlayUnlocalized("Podium");
            
            scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();

            int podiumId = 0;
            for (int i = scoreKeeper.Scores.Count - 1 ; i >= 0; i--) {

                playerPodiums[podiumId].SetPodium( scoreKeeper.Characters[i], scoreKeeper.Scores[i]);
                podiumId++;
            }

            Destroy(scoreKeeper.gameObject);
        }
    }
}
