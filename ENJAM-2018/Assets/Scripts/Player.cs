using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class Player : MonoBehaviour
    {

        bool moving;
        float moveProgress;
        float moveSpeed;

        bool lost;

        int score;
        int scoreMultiplicator;
        int combo;
        
        PlayersManager playerManager;

        private SequenceTile tile;
        private LevelSequence level;

        private void Start() {
            playerManager = GetComponentInParent<PlayersManager>();
            level = GetComponentInParent<LevelSequence>();

            tile = level.Tiles[playerManager.beginTile];
            tile.AddPlayerOnTile(this);
            float tileX = tile.gameObject.transform.position.x;
            transform.position = new Vector3(tileX, transform.position.y, transform.position.z);
        }

        void FixedUpdate() {

            if (moving) {
                moveProgress += moveSpeed * Time.fixedDeltaTime;
                Vector3 moveTarget = new Vector3(tile.gameObject.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, moveTarget, moveProgress);

                if (moveProgress >= 1) {
                    moving = false;
                    moveProgress = 0;
                }
            }
        }

        public void Move(bool goForward) {
            moveProgress = 0;
            moving = true;

            tile.RemovePlayerFromTile(this);
            if (goForward) {
                tile = tile.next;
                moveSpeed = playerManager.DashSpeed;
            }
            else {
                tile = tile.previous;
                moveSpeed = playerManager.MovebackSpeed;
            }
            tile.AddPlayerOnTile(this);
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
            if (lost) {
                return;
            }

            if (keyId == tile.requiredInput.inputKey && level.TilePosition(tile) < 10) {
                Move(true);
            }
            else if (keyId != tile.requiredInput.inputKey) {
                Move(false);
                combo = 0;
                scoreMultiplicator = 0;
            }
        }

        public void Lose() {
            lost = true;
        }

    }
}
