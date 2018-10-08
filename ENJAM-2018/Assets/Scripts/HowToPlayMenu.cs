using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class HowToPlayMenu : MonoBehaviour
{

    public List<Button> buttons;
    public GameObject howToPlayPanel;

    bool howToPlayDisplayed;

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisplayHowToPlay() {
        howToPlayDisplayed = true;
        howToPlayPanel.SetActive(true);
        for (int i = 0; i < buttons.Count; i++) {
            buttons[i].interactable = false;
        }
    }

    private void Update() {

        if (howToPlayDisplayed && Input.GetKeyDown("joystick 1 button 1")) {
            Debug.Log("Quit how to play");
            howToPlayDisplayed = false;
            howToPlayPanel.SetActive(false);
            for (int i = 0; i < buttons.Count; i++) {
                buttons[i].interactable = true;
            }

            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameObject);
        }
    }

}
