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

        public void SetPodium(Character character, int score = -1, int position = 0) {
            gameObject.SetActive(true);
            PlayerNameText.text = character.name;
            if (score != -1) {
                PlayerScoreText.text = "Score : " + score;
            }
            CharacterImage.sprite = character.sprite;

            Animator animator = CharacterImage.GetComponent<Animator>();
            animator.runtimeAnimatorController = (RuntimeAnimatorController)character.animator;
            
            if (position == 0) {
                animator.SetTrigger("Win");
            }
        }
    }
}