using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayersKeeper : MonoBehaviour {

    List<Character> selectedCharacters;

    public List<Character> SelectedCharacters
    {
        get { return selectedCharacters; }
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        selectedCharacters = new List<Character>();
    }
}
