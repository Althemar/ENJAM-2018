using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ENJAM2018
{
    public class ControllerMapping : MonoBehaviour
    {
        [System.Serializable]
        public struct KeyMapping
        {
            public string JoystickKeyName;
            public int JoystickKeyId;
            public int KeyCode;
        }

        public List<KeyMapping> keyMapping;

        public static ControllerMapping Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}
