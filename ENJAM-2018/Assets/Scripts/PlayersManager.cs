using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ENJAM2018
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance;

        public List<Player> players;
        public int beginTile = 4;

        [Header("Move variables")]
        public float MovingBackSpeed;
        public float DashSpeed;
        public float DashDistance;
        public float MovebackSpeed;

        [Header("Score")]
        public int basicScore;
        public int bestTiles;
        public int bestTilesScore;
        public int comboMax;

        [Header("Events")]
        public UnityEvent onRightInput;
        public UnityEvent onWrongInput;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

    }
}
