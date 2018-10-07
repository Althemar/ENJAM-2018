using ENJAM2018;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyPlayers : MonoBehaviour {
    

    public List<Character> characters;
    public List<CharacterSelection> characterSelections;

    List<Character> availableCharacters;

    List<ControllerMapping> inputManager;

    public Button validationButton;


    public SelectedPlayersKeeper selectedPlayersKeeper;

    void Start () {


        /*
        string[] names = Input.GetJoystickNames();
        for (int i = 0; i < names.Length; i++) {
            if (names[i].Contains("Xbox")) {
                inputManager.Add(InputManager.Instance.xboxController);
            }
            else if (names[i].Contains("Wireless Controller")) {
                inputManager.Add(InputManager.Instance.psMapping);
            }
            else if (names[i] == ""){
                inputManager.Add(InputManager.Instance.xboxController);
            }
            else {
                inputManager.Add(null);
            }
        }*/

        validationButton.interactable = false;

        availableCharacters = new List<Character>();
        for (int i = 0; i < characters.Count; i++) {
            availableCharacters.Add(characters[i]);
        }
        

       
    }

    void Update() {

        for (int i = 0; i < 4; i++) {
            if (Input.GetKeyDown("joystick " + (int)(i + 1) + " button " + 0)) {
                if (!AllCharactersSelected() && PlayerHasNotJoined(i)) {
                    int characterSelectionId = GetFirstUnselectedCharacter();
                    characterSelectionId = i;
                    int randomCharacter = Random.Range(0, characters.Count);
                    characterSelections[characterSelectionId].JoinGame(i, characters[randomCharacter]);
                    characters.RemoveAt(randomCharacter);
                    validationButton.interactable = true;
                }
            }
            if (Input.GetKeyDown("joystick " + (int)(i + 1) + " button " + 1)) {
                if (!PlayerHasNotJoined(i)) {
                    CharacterSelection characterSelection = GetSelectedCharacted(i);
                    characters.Add(characterSelection.selectedCharacter);
                    characterSelection.QuitGame();

                    if (!AtLeastOneSelected()) {
                        validationButton.interactable = false;
                    }
                }
            }
            if  (Input.GetKeyDown("joystick " + (int)(i + 1) + " button " + 2)) {
                if (!PlayerHasNotJoined(i) && AtLeastOneSelected()) {
                    selectedPlayersKeeper.SelectedCharacters.Clear();
                    for (int j = 0; j < characterSelections.Count; j++) {
                        if (characterSelections[j].selectedCharacter != null) {
                            selectedPlayersKeeper.SelectedCharacters.Add(characterSelections[j].selectedCharacter);
                        }
                    }
                    SceneManager.LoadScene("damien_scene");
                }
            }
        }
    }

    public int GetFirstUnselectedCharacter() {
        for (int i = 0; i < characterSelections.Count; i++) {
            if (!characterSelections[i].Selected) {
                return i;
            }
        }
        return -1;
    }

    public bool AllCharactersSelected() {
        for (int i = 0; i < characterSelections.Count; i++) {
            if (!characterSelections[i].Selected) {
                return false;
            }
        }
        return true;
    }

    public bool PlayerHasNotJoined(int player) {
        for (int i = 0; i < characterSelections.Count; i++) {
            if (characterSelections[i].Player == player) {
                return false;
            }
        }
        return true;
    }

    public CharacterSelection GetSelectedCharacted(int player) {
        for (int i = 0; i < characterSelections.Count; i++) {
            if (characterSelections[i].Player == player) {
                return characterSelections[i];
            }
        }
        return null;
    }

    public bool AtLeastOneSelected() {
        for (int i = 0; i < characterSelections.Count; i++) {
            if (characterSelections[i].Selected) {
                return true;
            }
        }
        return false;
    }

}

