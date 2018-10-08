using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ENJAM2018
{
    public class PlayerPodium : MonoBehaviour
    {
        public Text PlayerNameText;
        public Text PlayerScoreText;
        public Image CharacterImage;

        public void Awake() {
            PlayerNameText.text = "";
            PlayerScoreText.text = "";
            gameObject.SetActive(false);
        }

        public void SetPodium(Character character, int score = -1) {
            gameObject.SetActive(true);
            PlayerNameText.text = character.name;
            if (score != -1) {
                PlayerScoreText.text = "Score : " + score;
            }
            CharacterImage.sprite = character.sprite;
        }
    }
}