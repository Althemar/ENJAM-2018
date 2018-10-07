using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018 {
	[RequireComponent(typeof(SequenceGenerator))]
	public class LevelSequence : MonoBehaviour {

		[SerializeField] private Camera mainCamera;
		[SerializeField] private float tileSize = 2f;
		public float speed = 2f;
		[Header("Temporary")]
		[SerializeField] private GameObject prefabUp;
		[SerializeField] private GameObject prefabRight;
		[SerializeField] private GameObject prefabDown;
		[SerializeField] private GameObject prefabLeft;
		
		public SequenceTile[] Tiles { get; private set; }
		public Rect cameraBounds { get; private set; }
		public float distanceTravelled { get; private set; }
		private SequenceGenerator generator;
		private PhaseManager phaseManager;

		private void Awake() {
			phaseManager = PhaseManager.I;
			generator = GetComponent<SequenceGenerator>();
			generator.Init();
			if (mainCamera == null) {
				mainCamera = Camera.main;
			}
			float vertExtent = mainCamera.orthographicSize;
			float horizExtent = vertExtent * (Screen.width / (float) Screen.height);
			cameraBounds = new Rect(-horizExtent, -vertExtent, horizExtent * 2, vertExtent * 2);
			Tiles = new SequenceTile[(int) (cameraBounds.width / tileSize) + 2];
			SequenceInput.Init(prefabUp, prefabRight, prefabDown, prefabLeft);

			GenerateAllTiles();
		}

		private void Start() {
			// Place level origin at left of screen
			transform.position = new Vector3(cameraBounds.xMin + tileSize / 2f, 0f);

		}
		
		private void FixedUpdate() {
			speed = Mathf.Lerp(phaseManager.currentPhase.startSpeed, phaseManager.currentPhase.endSpeed, phaseManager.PhaseProgress);
			transform.position -= Vector3.right * speed * Time.fixedDeltaTime;
			distanceTravelled = -transform.position.x - cameraBounds.width / 2f + tileSize / 2f;

			int tilesOutCount = (int) ((cameraBounds.xMax - Tiles[Tiles.Length - 1].transform.position.x + tileSize / 2f) / tileSize);
			for (int i = 0; i < tilesOutCount; i++) {
				DeleteLeftTile();
				GenerateRightTile();
			}
		}

		private void DeleteLeftTile() {
			Destroy(Tiles[0].gameObject);
			for (int i = 1; i < Tiles.Length; i++) {
				Tiles[i - 1] = Tiles[i];
			}
		}

		private void GenerateRightTile() {
			SequenceTile tile = CreateTile(generator.GenerateNext(), Tiles[Tiles.Length - 2].transform.localPosition.x + tileSize);
			Tiles[Tiles.Length - 1] = tile;
			tile.previous = Tiles[Tiles.Length - 2];
			Tiles[Tiles.Length - 2].next = tile;
		}

		private void GenerateAllTiles() {
			SequenceInput[] seq = generator.GenerateSequence(Tiles.Length);
			for (int i = 0; i < Tiles.Length; i++) {
				SequenceTile tile = CreateTile(seq[i], tileSize * i);
				Tiles[i] = tile;
			}
			for (int i = 1; i < Tiles.Length - 1; i++) {
				Tiles[i].next = Tiles[i + 1];
				Tiles[i].previous = Tiles[i - 1];
			}
			Tiles[0].next = Tiles[1];
			Tiles[Tiles.Length - 1].previous = Tiles[Tiles.Length - 2];
		}

		private SequenceTile CreateRandomTile(float distanceFromStart) {
			return CreateTile(SequenceInput.GetRandom(), distanceFromStart);
		}

		private SequenceTile CreateTile(SequenceInput input, float distanceFromStart) {
			GameObject go = Instantiate(input.prefab, transform);
			SequenceTile tile = go.GetComponent<SequenceTile>();
			tile.requiredInput = input;
			go.transform.localPosition = new Vector3(distanceFromStart, 0f);
			return tile;
		}

        public int TilePosition(SequenceTile tile) {
            for (int i = 0; i < Tiles.Length; i++) {
                if (Tiles[i] == tile) {
                    return i;
                }
            }
            return -1;
        }

	}
}
