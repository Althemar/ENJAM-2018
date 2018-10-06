using System;
using UnityEngine;

namespace ENJAM2018 {
	public class SequenceTile : MonoBehaviour {

		[NonSerialized] public SequenceInput requiredInput;
		[NonSerialized] public SequenceTile previous;
		[NonSerialized] public SequenceTile next;

		public SequenceTile(SequenceInput required) {
			requiredInput = required;
		}

	}
}
