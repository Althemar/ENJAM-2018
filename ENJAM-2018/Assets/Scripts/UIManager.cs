using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ENJAM2018
{


    public class UIManager : MonoBehaviour
    {

        public Text endingText;
        public GameObject playerScoresParents;

        public static UIManager Instance;

        int numberOfPlayerUI = 0;
        public int spaceBetweenPlayerUI = 200;

        RectTransform rectTransform;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
            rectTransform = GetComponent<RectTransform>();

        }

        private void Start() {
            endingText.gameObject.SetActive(false);
        }

        public void DisplayEndingText() {
            endingText.gameObject.SetActive(true);
        }

        public PlayerScoreUI CreatePlayerScoreUI(GameObject scorePrefab) {
            PlayerScoreUI playerScoreUI = Instantiate(scorePrefab, playerScoresParents.transform).GetComponent<PlayerScoreUI>();
         

            playerScoreUI.GetComponent<RectTransform>().localPosition = new Vector3(numberOfPlayerUI * spaceBetweenPlayerUI, 0, 0);
            numberOfPlayerUI++;
            return playerScoreUI;
        }
    }
}