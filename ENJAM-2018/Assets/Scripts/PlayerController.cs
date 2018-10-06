using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class PlayerController : MonoBehaviour
    {
        
        enum Players { Player1, Player2, Player3, Player4 }

        [SerializeField] Players owner;
        Player player;
        string playerString;
        ControllerMapping inputManager;

        public string PlayerString
        {
            get { return playerString; }
        }


        void Start() {
            

            playerString = ((int)owner + 1).ToString();
            player = GetComponent<Player>();

            string[] names = Input.GetJoystickNames();
            if (names[(int)owner].Contains("Xbox")) {
                inputManager = InputManager.Instance.xboxController;
            }
            else if (names[(int)owner].Contains("Wireless Controller")) {
                inputManager = InputManager.Instance.psMapping;
            }
            else {
                inputManager = InputManager.Instance.xboxController;
            }

        }

        void Update() {
            for (int i = 0; i < inputManager.keyMapping.Count; i++) {
                int keyId = inputManager.keyMapping[i].JoystickKeyId;
                if (Input.GetKeyDown("joystick " + playerString + " button " + keyId)) {
                    player.CheckInput(inputManager.keyMapping[i].KeyCode);
                }
            }
        }
    }
}
