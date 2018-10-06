using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018 {
	public class LevelSequence : MonoBehaviour {

		[SerializeField] private Camera camera;
		[SerializeField] private float tileSize = 2f;
		[SerializeField] private float speed = 2f;
		[SerializeField] private List<SequenceInputType> debugInputs;
		[Header("Temporary")]
		[SerializeField] private GameObject prefabUp;
		[SerializeField] private GameObject prefabRight;
		[SerializeField] private GameObject prefabDown;
		[SerializeField] private GameObject prefabLeft;
		
		private SequenceTile[] tiles;

		private void Awake() {
			float vertExtent = camera.orthographicSize;
			float horizExtent = vertExtent * (Screen.width / Screen.height);
			tiles = new SequenceTile[(int) (horizExtent / tileSize) + 1];
			SequenceInput.Init(prefabUp, prefabRight, prefabDown, prefabLeft);
		}

		private void Start() {
			GenerateSequence(debugInputs);
		}

		private void FixedUpdate() {
			transform.position -= Vector3.right * speed * Time.fixedDeltaTime;
		}

		private void GenerateSequence(List<SequenceInputType> inputs) {
			for (int i = 0; i < inputs.Count; i++) {
				GameObject go = Instantiate(inputs[i].GetSequenceInput().prefab, transform);
				SequenceTile tile = go.GetComponent<SequenceTile>();
				tile.requiredInput = inputs[i].GetSequenceInput();
				go.transform.position = new Vector3(tileSize * i, 0f);
			}
		}

	}
}
