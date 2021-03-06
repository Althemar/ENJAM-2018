﻿using UnityEngine;

namespace ENJAM2018 {
	public class PhaseManager : MonoBehaviour {

        AudioSource audioSource;

		[System.Serializable]
		public struct Phase {
			public float probDown;		// A
			public float probRight;		// B
			public float probLeft;      // X
			public float probUp;        // Y
			public float probDouble;
			public float length;
			public float startSpeed;
			public float endSpeed;
            public AudioClip nextPhaseClip;
		}

		private static PhaseManager instance;
		public static PhaseManager I {
			get {
				if (instance == null) {
					instance = FindObjectOfType<PhaseManager>();
					instance.Init();
				}
				return instance;
			}
		}

        private void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        [SerializeField] private Phase[] phases;
		public Phase currentPhase { get; private set; }
		private int phaseIndex = 0;
		private float phaseStartTime;

		public float PhaseProgress {
			get {
				return (Time.time - phaseStartTime) / currentPhase.length;
			}
		}

		private void Init() {
			currentPhase = phases[phaseIndex];
			phaseStartTime = Time.time;
		}

		private void Update() {
			if (Time.time - phaseStartTime > currentPhase.length) {
				NextPhase();
			}
		}

		public void NextPhase() {
			phaseStartTime = Time.time;
			if (phaseIndex < phases.Length - 1) {
				Debug.Log("Phase " + phaseIndex + " Ended! ... Starting Phase " + (phaseIndex + 1));
                audioSource.PlayOneShot(phases[phaseIndex + 1].nextPhaseClip);
				currentPhase = phases[++phaseIndex];
			}

		}

		public bool IsOnEndPhase() {
			return phaseIndex == phases.Length - 1;
		}

	}
}
