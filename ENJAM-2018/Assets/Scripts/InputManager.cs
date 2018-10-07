using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class InputManager : MonoBehaviour
    {

        public static InputManager Instance;

        public ControllerMapping xboxController;
        public ControllerMapping psMapping;

       

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}