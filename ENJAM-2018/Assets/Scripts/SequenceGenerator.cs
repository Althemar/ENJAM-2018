using UnityEngine;

namespace ENJAM2018 {
	public class SequenceGenerator : MonoBehaviour {

		public const int PREVIOUS_BUFFER_SIZE = 3;

		private SequenceInputType[] previous;
		private int sequenceIndex = 0;
		private PhaseManager phaseManager;

		public void Init() {
			phaseManager = PhaseManager.I;
			previous = new SequenceInputType[PREVIOUS_BUFFER_SIZE];
		}

		public SequenceInputType[] GenerateSequence(int count) {
			SequenceInputType[] sequence = new SequenceInputType[count];
			for (int i = 0; i < count; i++) {
				sequence[i] = GenerateNext();
			}
			return sequence;
		}

		public SequenceInputType GenerateNext() {
			float rnd = Random.value;
			SequenceInputType inputType;
			if (rnd < phaseManager.currentPhase.probDown) {
				inputType = SequenceInputType.Down;
			} else if (rnd < phaseManager.currentPhase.probDown + phaseManager.currentPhase.probRight) {
				inputType = SequenceInputType.Right;
			} else if (rnd < phaseManager.currentPhase.probDown + phaseManager.currentPhase.probRight + phaseManager.currentPhase.probLeft) {
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
