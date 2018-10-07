using UnityEngine;

namespace ENJAM2018 {
	public class BackgroundManager : MonoBehaviour {

		[System.Serializable]
		public struct BackgroundLayer {
			public Transform transf;
			public float relativeSpeed;
			[Tooltip("Used to override calculated width (0 to use calculated)")]
			public float widthOverride;
			[System.NonSerialized] public SpriteRenderer spriteRenderer;
			[System.NonSerialized] public SpriteRenderer[] renderers;
			[System.NonSerialized] public float spriteWidth;
		}
		
		public bool firstIsBack = true;
		public BackgroundLayer[] backgroundLayers;

		private LevelSequence level;

		private void Awake() {
			level = FindObjectOfType<LevelSequence>();
			if (level == null) {
				Debug.LogWarning("BackgroundManager did not find any Level");
			}
			for (int i = 0; i < backgroundLayers.Length; i++) {
				backgroundLayers[i].spriteRenderer = backgroundLayers[i].transf.GetComponentInChildren<SpriteRenderer>();
				backgroundLayers[i].spriteRenderer.sortingLayerName = "Background";
				if (firstIsBack) {
					backgroundLayers[i].spriteRenderer.sortingOrder = i;
				} else {
					backgroundLayers[i].spriteRenderer.sortingOrder = backgroundLayers.Length - i - 1;
				}

				if (backgroundLayers[i].widthOverride != 0) {
					backgroundLayers[i].spriteWidth = backgroundLayers[i].widthOverride;
				} else {
					backgroundLayers[i].spriteWidth = backgroundLayers[i].spriteRenderer.sprite.bounds.size.x * backgroundLayers[i].spriteRenderer.transform.lossyScale.x;
				}
				backgroundLayers[i].renderers = new SpriteRenderer[(int) (level.cameraBounds.width / backgroundLayers[i].spriteWidth) + 3];

				GenerateAllSprites(backgroundLayers[i]);

				Destroy(backgroundLayers[i].spriteRenderer.gameObject);
			}
		}

		private void FixedUpdate() {
			for (int i = 0; i < backgroundLayers.Length; i++) {
				backgroundLayers[i].transf.position += Vector3.left * (level.speed + backgroundLayers[i].relativeSpeed) * Time.fixedDeltaTime;
				
				int toGenCount = (int) ((level.cameraBounds.xMax - backgroundLayers[i].renderers[backgroundLayers[i].renderers.Length - 1].transform.position.x + backgroundLayers[i].spriteWidth / 2f) / backgroundLayers[i].spriteWidth);
				for (int j = 0; j < toGenCount; j++) {
					DeleteLeftSprite(backgroundLayers[i]);
					GenerateRightSprite(backgroundLayers[i]);
				}
			}
		}

		private void DeleteLeftSprite(BackgroundLayer layer) {
			Destroy(layer.renderers[0].gameObject);
			for (int i = 1; i < layer.renderers.Length; i++) {
				layer.renderers[i - 1] = layer.renderers[i];
			}
		}

		private void GenerateRightSprite(BackgroundLayer layer) {
			GameObject go = Instantiate(layer.renderers[0].gameObject, layer.transf);
			go.transform.position = new Vector3(layer.renderers[layer.renderers.Length - 2].transform.position.x + layer.spriteWidth, layer.renderers[layer.renderers.Length - 2].transform.position.y, layer.renderers[layer.renderers.Length - 2].transform.position.z);
			layer.renderers[layer.renderers.Length - 1] = go.GetComponent<SpriteRenderer>();
		}

		private void GenerateAllSprites(BackgroundLayer layer) {
			for (int i = 0; i < layer.renderers.Length; i++) {
				GameObject go = Instantiate(layer.spriteRenderer.gameObject, layer.transf);
				go.transform.position = new Vector3(layer.spriteRenderer.transform.position.x + layer.spriteWidth * i, layer.spriteRenderer.transform.position.y, layer.spriteRenderer.transform.position.z);
				layer.renderers[i] = go.GetComponent<SpriteRenderer>();
			}
		}

	}
}
