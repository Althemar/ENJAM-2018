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

        public void SetPodium(int score, Character character) {
            gameObject.SetActive(true);
            PlayerNameText.text = character.name;
            PlayerScoreText.text = "Score : " + score;
            CharacterImage.sprite = character.sprite;
        }
    }
}