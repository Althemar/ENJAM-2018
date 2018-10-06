using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 moveTarget;
    float moveProgress;
    float moveSpeed;

    

    CharacterState characterState = CharacterState.Waiting;
    PlayersManager playerController;

    enum CharacterState
    {
        Waiting,
        Moving
    }

    private void Start() {
        playerController = GetComponentInParent<PlayersManager>();
    }

    void Update () {

        switch (characterState) {

            case CharacterState.Waiting :
                transform.position += Vector3.left * playerController.MovingBackSpeed * Time.deltaTime;
                break;

            case CharacterState.Moving:

                moveProgress += moveSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, moveTarget, moveProgress);

                if (moveProgress >= 1) {
                    characterState = CharacterState.Waiting;
                    moveProgress = 0;
                }
                break;
        } 
    }

    public void Move(bool goForward) {
        moveProgress = 0;
        characterState = CharacterState.Moving;

        if (goForward) {
            moveTarget = transform.position + new Vector3(playerController.DashDistance, 0, 0); // TODO : Get next tile
            moveSpeed = playerController.DashSpeed;
        }
        else {
            moveTarget = transform.position - new Vector3(playerController.DashDistance, 0, 0); // TODO : Get previous tile
            moveSpeed = playerController.MovebackSpeed;
        }
    }

    public void CheckInput(int keyId) {
        if (keyId == 0) { // Check input and move forward if right
            Move(true);
        }
        else {
            Move(false);
        }
    }
}
