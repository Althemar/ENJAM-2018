using UnityEngine;

namespace ENJAM2018 {
	public class SequenceGenerator : MonoBehaviour {

		public const int PREVIOUS_BUFFER_SIZE = 3;

		[System.Serializable]
		public struct Phase {
			public float probDown;		// A
			public float probRight;		// B
			public float probLeft;		// X
			public float probUp;		// Y
			public int length;
		}

		public Phase[] phases;
		public Phase phase { get; private set; }
		private int phaseIndex = 0;
		private SequenceInputType[] previous;
		private int sequenceIndex = 0;
		private int nextPhaseChange;

		public void Init() {
			previous = new SequenceInputType[PREVIOUS_BUFFER_SIZE];
			phase = phases[phaseIndex];
			nextPhaseChange = phase.length;
		}

		public SequenceInputType[] GenerateSequence(int count) {
			SequenceInputType[] sequence = new SequenceInputType[count];
			for (int i = 0; i < count; i++) {
				sequence[i] = GenerateNext();
			}
			return sequence;
		}

		public SequenceInputType GenerateNext() {
			if (sequenceIndex > nextPhaseChange && phaseIndex < phases.Length - 1) {
				phase = phases[++phaseIndex];
				nextPhaseChange = sequenceIndex + phase.length;
			}
			float rnd = Random.value;
			SequenceInputType inputType;
			if (rnd < phase.probDown) {
				inputType = SequenceInputType.Down;
			} else if (rnd < phase.probDown + phase.probRight) {
				inputType = SequenceInputType.Right;
			} else if (rnd < phase.probDown + phase.probRight + phase.probLeft) {
				inputType = SequenceInputType.Left;
			} else {
				inputType = SequenceInputType.Up;
			}
			PushLast(inputType);
			sequenceIndex++;
			return inputType;
		}

		private void PushLast(SequenceInputType input) {
			for (int i = 1; i < PREVIOUS_BUFFER_SIZE; i++) {
				previous[i - 1] = previous[i];
			}
			previous[PREVIOUS_BUFFER_SIZE - 1] = input;
		}

	}
}
