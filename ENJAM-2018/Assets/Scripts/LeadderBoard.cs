﻿using System.Collections;
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
            

            scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();

            for (int i = scoreKeeper.Scores.Count - 1 ; i >= 0; i--) {
                Debug.Log(i);

                playerPodiums[i].SetPodium(scoreKeeper.Scores[i], scoreKeeper.Characters[i]);
            }

            Destroy(scoreKeeper.gameObject);
        }
    }
}
