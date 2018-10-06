using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENJAM2018;

public class Player : MonoBehaviour {

    Vector3 dashTarget;
    float dashProgress;

    CharacterState characterState = CharacterState.Waiting;
    PlayersManager playerController;

	private SequenceTile tile;

    enum CharacterState
    {
        Waiting,
        MovingForward
    }

    private void Start() {
        playerController = GetComponentInParent<PlayersManager>();
    }

    void Update () {

        switch (characterState) {

            case CharacterState.Waiting :
                transform.position += Vector3.left * playerController.MovingBackSpeed * Time.deltaTime;
                break;

            case CharacterState.MovingForward:

                dashProgress += playerController.DashSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, dashTarget, dashProgress);

                if (dashProgress >= 1) {
                    characterState = CharacterState.Waiting;
                    dashProgress = 0;
                }
                break;
        } 
    }

    public void MoveForward() {
        dashProgress = 0;
        dashTarget = transform.position + new Vector3(playerController.DashDistance, 0, 0);
		//dashTarget = tile.next.transform.position;
        characterState = CharacterState.MovingForward;
    }
}
