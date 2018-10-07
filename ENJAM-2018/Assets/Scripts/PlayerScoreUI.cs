using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ENJAM2018
{
    public class PlayerScoreUI : MonoBehaviour
    {

        public Text playerName;
        public Text scoreText;
        public Text multiplicatorText;
        public Text comboText;
        public Image comboFillingImage;

        Player player;
        string playerString;

        Animator animator;

        private void Start() {
            animator = multiplicatorText.GetComponent<Animator>();
        }

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

        public void SetName(string name) {
            playerName.text = name;
        }

        public void SetScore(int score) {
            scoreText.text = score.ToString();
        }

        public void SetMultiplicator(int multiplicator) {
            multiplicatorText.text = " x " + multiplicator.ToString();
            if (multiplicator != 1) {
                animator.SetTrigger("MultiplicatorUp");
            }
            else {
                animator.SetTrigger("MultiplicatorDecrease");
            }
        }
        

        public void SetCombo(int combo) {
            comboText.text = "Combo : " + combo.ToString();
            comboFillingImage.fillAmount = combo / 10f;
        }

    }
}

