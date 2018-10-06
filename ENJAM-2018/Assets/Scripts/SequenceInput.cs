using UnityEngine;

namespace ENJAM2018 {
	public enum SequenceInputType : int {
		Down = 0,
		Right = 1,
		Left = 2,
		Up = 3
	}
	public static class SequenceInputTypeExt {
		public static SequenceInput GetSequenceInput(this SequenceInputType type) {
			switch (type) {
				case SequenceInputType.Up:
					return SequenceInput.Up;
				case SequenceInputType.Right:
					return SequenceInput.Right;
				case SequenceInputType.Down:
					return SequenceInput.Down;
				case SequenceInputType.Left:
					return SequenceInput.Left;
			}
			return null;
		}
	}
	public class SequenceInput {
		
		public static SequenceInput Up;
		public static SequenceInput Right;
		public static SequenceInput Down;
		public static SequenceInput Left;

		public static void Init(GameObject prefabUp, GameObject prefabRight, GameObject prefabDown, GameObject prefabLeft) {
			Up = new SequenceInput(prefabUp, 3);
			Right = new SequenceInput(prefabRight, 1);
			Down = new SequenceInput(prefabDown, 0);
			Left = new SequenceInput(prefabLeft, 2);
		}

		public static SequenceInput GetRandom() {
            return ((SequenceInputType) Random.Range(0, 4)).GetSequenceInput();
        }

		public static SequenceInputType GetRandomType() {
			return (SequenceInputType) Random.Range(0, 4);
		}

        public GameObject prefab;
		public int inputKey;

		public SequenceInput(GameObject prefab, int key) {
			this.prefab = prefab;
			this.inputKey = key;
		}

	}
}
