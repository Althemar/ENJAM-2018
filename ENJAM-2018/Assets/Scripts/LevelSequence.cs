using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018 {
	public class LevelSequence : MonoBehaviour {

		[SerializeField] private Camera mainCamera;
		[SerializeField] private float tileSize = 2f;
		[SerializeField] private float speed = 2f;
		[SerializeField] private float acceleration = .5f;
		[Header("Temporary")]
		[SerializeField] private GameObject prefabUp;
		[SerializeField] private GameObject prefabRight;
		[SerializeField] private GameObject prefabDown;
		[SerializeField] private GameObject prefabLeft;
		
		private SequenceTile[] tiles;
		private Rect cameraBounds;
		private float distanceTravelled = 0f;

        public SequenceTile[] Tiles
        {
            get { return tiles; }
        }

		private void Awake() {
			if (mainCamera == null) {
				mainCamera = Camera.main;
			}
			float vertExtent = mainCamera.orthographicSize;
			float horizExtent = vertExtent * (Screen.width / (float) Screen.height);
			cameraBounds = new Rect(-horizExtent, -vertExtent, horizExtent * 2, vertExtent * 2);
			tiles = new SequenceTile[(int) (cameraBounds.width / tileSize) + 2];
			SequenceInput.Init(prefabUp, prefabRight, prefabDown, prefabLeft);
		}

		private void Start() {
			// Place level origin at left of screen
			transform.position = new Vector3(cameraBounds.xMin + tileSize / 2f, 0f);

			GenerateAllTiles();
		}

		private void FixedUpdate() {
			speed += acceleration * Time.fixedDeltaTime;
			float distance = speed * Time.fixedDeltaTime;
			transform.position -= Vector3.right * distance;
			distanceTravelled += distance;
			int tilesOutCount = (int) ((cameraBounds.xMax - tiles[tiles.Length - 1].transform.position.x + tileSize / 2f) / tileSize);
			Debug.Log(tilesOutCount);
			for (int i = 0; i < tilesOutCount; i++) {
				DeleteLeftTile();
				GenerateRightTile();
			}
		}

		private void DeleteLeftTile() {
			Destroy(tiles[0].gameObject);
			for (int i = 1; i < tiles.Length; i++) {
				tiles[i - 1] = tiles[i];
			}
		}

		private void GenerateRightTile() {
			SequenceTile tile = CreateRandomTile(tiles[tiles.Length - 2].transform.localPosition.x + tileSize);
			tiles[tiles.Length - 1] = tile;
			tile.previous = tiles[tiles.Length - 2];
			tiles[tiles.Length - 2].next = tile;
		}

		private void GenerateAllTiles() {
			for (int i = 0; i < tiles.Length; i++) {
				SequenceTile tile = CreateRandomTile(tileSize * i);
				tiles[i] = tile;
			}
			for (int i = 1; i < tiles.Length - 1; i++) {
				tiles[i].next = tiles[i + 1];
				tiles[i].previous = tiles[i - 1];
			}
			tiles[0].next = tiles[1];
			tiles[tiles.Length - 1].previous = tiles[tiles.Length - 2];
		}

		private SequenceTile CreateRandomTile(float distanceFromStart) {
			SequenceInput input = SequenceInput.GetRandom();
			GameObject go = Instantiate(input.prefab, transform);
			SequenceTile tile = go.GetComponent<SequenceTile>();
			tile.requiredInput = input;
			go.transform.localPosition = new Vector3(distanceFromStart, 0f);
			return tile;
		}

        public int TilePosition(SequenceTile tile) {
            for (int i = 0; i < tiles.Length; i++) {
                if (tiles[i] == tile) {
                    return i;
                }
            }
            return -1;
        }

	}
}
