using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class Player : MonoBehaviour
    {
        float moveProgress;
        float moveSpeed;

        int score;
        int scoreMultiplicator;
        int combo;

        CharacterState characterState = CharacterState.Waiting;
        PlayersManager playerManager;

        private SequenceTile tile;
        private LevelSequence level;

        enum CharacterState
        {
            Waiting,
            Moving
        }

        private void Start() {
            playerManager = GetComponentInParent<PlayersManager>();
            level = GetComponentInParent<LevelSequence>();

            tile = level.Tiles[playerManager.beginTile];
            float tileX = tile.gameObject.transform.position.x;
            transform.position = new Vector3(tileX, transform.position.y, transform.position.z);
        }

        void FixedUpdate() {

            switch (characterState) {

                case CharacterState.Moving:

                    moveProgress += moveSpeed * Time.fixedDeltaTime;
                    Vector3 moveTarget = new Vector3(tile.gameObject.transform.position.x, transform.position.y, transform.position.z);
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
                tile = tile.next;
                moveSpeed = playerManager.DashSpeed;
            }
            else {
                tile = tile.previous;
                moveSpeed = playerManager.MovebackSpeed;
            }
        }

        public void IncreaseScore() {
            int tilePosition = level.TilePosition(tile);
            int totalTile = level.Tiles.Length;
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
}
