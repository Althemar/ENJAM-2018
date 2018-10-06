using UnityEngine;

namespace ENJAM2018 {
	public class SequenceGenerator : MonoBehaviour {

		public const int PREVIOUS_BUFFER_SIZE = 3;

		private SequenceInput[] previous;
		private int sequenceIndex = 0;
		private PhaseManager phaseManager;

		public void Init() {
			phaseManager = PhaseManager.I;
			previous = new SequenceInput[PREVIOUS_BUFFER_SIZE];
		}

		public SequenceInput[] GenerateSequence(int count) {
			SequenceInput[] sequence = new SequenceInput[count];
			for (int i = 0; i < count; i++) {
				sequence[i] = GenerateNext();
			}
			return sequence;
		}

		public SequenceInput GenerateNext() {
			float rnd = Random.value;
			SequenceInput inputType;
			if (rnd < phaseManager.currentPhase.probDown) {
				inputType = SequenceInput.Down.Copy();
			} else if (rnd < phaseManager.currentPhase.probDown + phaseManager.currentPhase.probRight) {
				inputType = SequenceInput.Right.Copy();
			} else if (rnd < phaseManager.currentPhase.probDown + phaseManager.currentPhase.probRight + phaseManager.currentPhase.probLeft) {
				inputType = SequenceInput.Left.Copy();
			} else {
				inputType = SequenceInput.Up.Copy();
			}
			rnd = Random.value;
			if (rnd < phaseManager.currentPhase.probDouble) {
				inputType.isDouble = true;
			}
			PushLast(inputType);
			sequenceIndex++;
			return inputType;
		}

		private void PushLast(SequenceInput input) {
			for (int i = 1; i < PREVIOUS_BUFFER_SIZE; i++) {
				previous[i - 1] = previous[i];
			}
			previous[PREVIOUS_BUFFER_SIZE - 1] = input;
		}

	}
}
