using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    enum Players { Player1, Player2, Player3, Player4 }

    [SerializeField] Players owner;
    Player player;
    string playerString;
    
	void Start () {
        playerString = ((int) owner + 1).ToString();
        player = GetComponent<Player>(); 
	}
	
	void Update () {
        if (Input.GetKeyDown("f" + playerString)) { 
            player.MoveForward();
        }
    }
}
