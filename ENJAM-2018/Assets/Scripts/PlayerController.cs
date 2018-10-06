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


        void Start() {
            playerString = ((int)owner + 1).ToString();
            player = GetComponent<Player>();
        }

        void Update() {
            for (int i = 0; i < InputManager.Instance.keyMapping.Count; i++) {
                int keyId = InputManager.Instance.keyMapping[i].KeyId;
                if (Input.GetKeyDown("joystick " + playerString + " button " + keyId)) {
                    player.CheckInput(keyId);
                }
            }
        }
    }
}
