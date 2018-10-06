using System;
using UnityEngine;

namespace ENJAM2018 {
	public class SequenceTile : MonoBehaviour {

		[NonSerialized]
		public SequenceInput requiredInput;
		public SequenceTile previous;
		public SequenceTile next;

		public SequenceTile(SequenceInput required) {
			requiredInput = required;
		}

	}
}
