using System;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018 {
	public class SequenceTile : MonoBehaviour {

		[NonSerialized] public SequenceInput requiredInput;
		[NonSerialized] public SequenceTile previous;
		[NonSerialized] public SequenceTile next;

        public Sprite imageNoPlayerOnTile;
        public Sprite imagePlayerOnTile;

        List<Player> playersOnTile;

        SpriteRenderer spriteRenderer;

		public SequenceTile(SequenceInput required) {
			requiredInput = required;
		}

        public void Awake() {
            playersOnTile = new List<Player>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDestroy() {
            for (int i = 0; i < playersOnTile.Count; i++) {
                playersOnTile[i].Lose();
            }
        }

        public void AddPlayerOnTile(Player player) {
            playersOnTile.Add(player);
            spriteRenderer.sprite = imagePlayerOnTile;
        }

        public void RemovePlayerFromTile(Player player) {
            playersOnTile.Remove(player);
            if (playersOnTile.Count == 0) {
                spriteRenderer.sprite = imageNoPlayerOnTile;
            }
        }
    }
}
