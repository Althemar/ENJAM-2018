using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [System.Serializable]
    public struct KeyMapping
    {
        public int KeyId;
        public string KeyCode;
    }

    public List<KeyMapping> keyMapping;

    public static InputManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}
