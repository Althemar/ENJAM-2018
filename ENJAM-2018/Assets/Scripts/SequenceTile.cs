using System;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018 {
	public class SequenceTile : MonoBehaviour {

		[NonSerialized]
		public SequenceInput requiredInput;
		public SequenceTile previous;
		public SequenceTile next;

        List<Player> playersOnTile;

		public SequenceTile(SequenceInput required) {
			requiredInput = required;
		}

        public void Awake() {
            playersOnTile = new List<Player>();
        }

        private void OnDestroy() {
            for (int i = 0; i < playersOnTile.Count; i++) {
                playersOnTile[i].Lose();
            }
        }

        public void AddPlayerOnTile(Player player) {
            playersOnTile.Add(player);
        }

        public void RemovePlayerFromTile(Player player) {
            playersOnTile.Remove(player);
        }
    }
}
