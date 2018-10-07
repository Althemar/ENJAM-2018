using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018 {
	public class BackgroundManager : MonoBehaviour {

		public struct GlobalSprite {
			public Sprite sprite;
			public float requiredScale;
			[System.NonSerialized] public SpriteRenderer renderer;
		}

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
		public Sprite[] availableSprites;
		public float relativeSpeed;

		private LevelSequence level;
		private GlobalSprite[] globalSprites;
		private List<GlobalSprite> activeSprites;

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

			// GlobalSprites
			if (availableSprites.Length != 0) {
				globalSprites = new GlobalSprite[availableSprites.Length];
				for (int i = 0; i < availableSprites.Length; i++) {
					globalSprites[i].sprite = availableSprites[i];
					globalSprites[i].requiredScale = level.cameraBounds.height / availableSprites[i].bounds.size.y;
				}
				CreateAllGlobalSprites();
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

			// Global Sprites
			if (availableSprites.Length != 0) {
				foreach (GlobalSprite gs in activeSprites) {
					gs.renderer.transform.position += Vector3.left * (level.speed + relativeSpeed) * Time.fixedDeltaTime;
				}
				if (activeSprites[activeSprites.Count - 1].renderer.transform.position.x + activeSprites[activeSprites.Count - 1].sprite.bounds.size.x * activeSprites[activeSprites.Count - 1].requiredScale / 2f < level.cameraBounds.xMax) {
					GenerateRightGlobalSprite();
				}
				if (activeSprites[0].renderer.transform.position.x + activeSprites[0].sprite.bounds.size.x * activeSprites[0].requiredScale / 2f < level.cameraBounds.xMin) {
					DeleteLeftGlobalSprite();
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

		private void CreateAllGlobalSprites() {
			activeSprites = new List<GlobalSprite>();

			float filled = 0f;
			GlobalSprite prev = CreateRandomGlobalSprite();
			prev.renderer.transform.position = new Vector3(level.cameraBounds.xMin + prev.sprite.bounds.size.x * prev.requiredScale / 2f, 0f, transform.position.z);
			filled += prev.sprite.bounds.size.x * prev.requiredScale;
			activeSprites.Add(prev);
			while (filled < level.cameraBounds.width) {
				GlobalSprite gs = CreateRandomGlobalSprite();
				gs.renderer.transform.position = new Vector3(prev.renderer.transform.position.x + prev.sprite.bounds.size.x * prev.requiredScale / 2f + gs.sprite.bounds.size.x * prev.requiredScale / 2f, 0f, transform.position.z);
				filled += gs.sprite.bounds.size.x * gs.requiredScale;
				prev = gs;
				activeSprites.Add(gs);
			}
		}

		private void DeleteLeftGlobalSprite() {
			Destroy(activeSprites[0].renderer.gameObject);
			activeSprites.RemoveAt(0);
		}

		private void GenerateRightGlobalSprite() {
			GlobalSprite gs = CreateRandomGlobalSprite();
			gs.renderer.transform.position = new Vector3(activeSprites[activeSprites.Count - 1].renderer.transform.position.x + activeSprites[activeSprites.Count - 1].sprite.bounds.size.x * activeSprites[activeSprites.Count - 1].requiredScale / 2f + gs.sprite.bounds.size.x * gs.requiredScale / 2f, 0f, transform.position.z);
			activeSprites.Add(gs);
		}

		private GlobalSprite CreateRandomGlobalSprite() {
			GameObject go = new GameObject("BG_Sprite", typeof(SpriteRenderer));
			go.transform.SetParent(transform);
			GlobalSprite gs = globalSprites[Random.Range(0, globalSprites.Length)];
			go.transform.localScale = new Vector3(gs.requiredScale, gs.requiredScale);
			gs.renderer = go.GetComponent<SpriteRenderer>();
			gs.renderer.sprite = gs.sprite;
			return gs;
		}

	}
}
