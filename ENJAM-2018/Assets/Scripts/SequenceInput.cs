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
			Up = new SequenceInput(prefabUp);
			Right = new SequenceInput(prefabRight);
			Down = new SequenceInput(prefabDown);
			Left = new SequenceInput(prefabLeft);
		}

		public static SequenceInput GetRandom() {
            int rand = Random.Range(0, 4);
			SequenceInput sequenceInput = ((SequenceInputType) rand).GetSequenceInput();
            sequenceInput.inputKey = rand;
            return sequenceInput;
        }

        public GameObject prefab;
		public int inputKey;

		public SequenceInput(GameObject prefab) {
			this.prefab = prefab;
		}

	}
}
