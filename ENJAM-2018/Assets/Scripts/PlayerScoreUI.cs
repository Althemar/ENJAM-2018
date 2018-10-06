using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ENJAM2018
{
    public class PlayerScoreUI : MonoBehaviour
    {

        public Text scoreText;
        public Text multiplicatorText;
        public Text comboText;
        public Image comboFillingImage;

        Player player;
        string playerString;

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public string PlayerString
        {
            get { return playerString; }
            set { playerString = value; }
        }

        public void SetScore(int score) {
            scoreText.text = "Player " + playerString + " : " + score.ToString();
        }

        public void SetMultiplicator(int multiplicator) {
            multiplicatorText.text = " x " + multiplicator.ToString();
        }
        

        public void SetCombo(int combo) {
            comboText.text = "Combo : " + combo.ToString();
            comboFillingImage.fillAmount = combo / 10f;
        }

    }
}

