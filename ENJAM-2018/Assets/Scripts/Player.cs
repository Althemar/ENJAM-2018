using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 moveTarget;
    float moveProgress;
    float moveSpeed;

    int score;
    int scoreMultiplicator;
    int combo;

    CharacterState characterState = CharacterState.Waiting;
    PlayersManager playerManager;

    enum CharacterState
    {
        Waiting,
        Moving
    }

    private void Start() {
        playerManager = GetComponentInParent<PlayersManager>();
    }

    void Update () {

        switch (characterState) {

            case CharacterState.Waiting :
                transform.position += Vector3.left * playerManager.MovingBackSpeed * Time.deltaTime;
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
            moveTarget = transform.position + new Vector3(playerManager.DashDistance, 0, 0); // TODO : Get next tile
            moveSpeed = playerManager.DashSpeed;
        }
        else {
            moveTarget = transform.position - new Vector3(playerManager.DashDistance, 0, 0); // TODO : Get previous tile
            moveSpeed = playerManager.MovebackSpeed;
        }
    }

    public void IncreaseScore() {
        // TODO Temp variables
        int tilePosition = 0;
        int totalTile = 0;
        if (tilePosition <= totalTile - playerManager.bestTiles) {
            score += playerManager.basicScore;
        }
        else {
            score += playerManager.bestTilesScore * scoreMultiplicator;
        }
        combo++;
        if (combo > playerManager.comboMax) {
            combo = 0;
            scoreMultiplicator++;
        }
    }

    public void CheckInput(int keyId) {
        if (keyId == 0) { // Check input and move forward if right
            Move(true);
            
        }
        else {
            Move(false);
            combo = 0;
            scoreMultiplicator = 0;
        }
    }
}
