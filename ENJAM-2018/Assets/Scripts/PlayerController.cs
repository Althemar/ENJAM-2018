using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class PlayerController : MonoBehaviour
    {
        
        public enum Players { Player1, Player2, Player3, Player4 }

        [SerializeField] Players owner;
        Player player;
        string playerString;
        ControllerMapping inputManager;

        public string PlayerString
        {
            get { return playerString; }
        }

        public Players Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        void Start() {
            
            playerString = ((int)owner + 1).ToString();
            player = GetComponent<Player>();

            string[] names = Input.GetJoystickNames();
            if ((int)owner > names.Length - 1) {
                player.Playing = false;
            }
            else if (names[(int)owner].Contains("Xbox")) {
                inputManager = InputManager.Instance.xboxController;
            }
            else if (names[(int)owner].Contains("Wireless Controller")) {
                inputManager = InputManager.Instance.psMapping;
            }
            else if (names[(int)owner] != "") {
                inputManager = InputManager.Instance.xboxController;
            }
            else {
                player.Playing = false;
            }
        }

        

        void Update() {
            if (!player.Playing) {
                return;
            }

            for (int i = 0; i < inputManager.keyMapping.Count; i++) {
                int keyId = inputManager.keyMapping[i].JoystickKeyId;
                if (Input.GetKeyDown("joystick " + playerString + " button " + keyId)) {
                    player.CheckInput(inputManager.keyMapping[i].KeyCode);
                }
            }
        }
    }
}
