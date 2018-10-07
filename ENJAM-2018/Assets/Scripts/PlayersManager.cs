using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ENJAM2018
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance;

        List<Player> players;
        public int beginTile = 4;

        public List<Player> Players
        {
            get { return players; }
        }

        

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
        /*
                [Header("Events")]
                public UnityEvent onRightInput;
                public UnityEvent onWrongInput;
                */
        public float timeBetweenPunch;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            players = new List<Player>();
        }

        public List<Player> GetOtherPlayersOnTile(Player player) {
            List<Player> otherPlayers = new List<Player>();
            for (int i = 0; i < players.Count; i++) {
                if (players[i] != player && players[i].Tile == player.Tile) {
                    otherPlayers.Add(players[i]);
                }
            }
            return otherPlayers;
        }

    }
}
