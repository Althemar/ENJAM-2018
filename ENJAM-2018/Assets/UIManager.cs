using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text endingText;
    public static UIManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        endingText.gameObject.SetActive(false);
    }

    public void DisplayEndingText() {
        endingText.gameObject.SetActive(true);
    }
}
