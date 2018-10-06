using UnityEngine;

namespace ENJAM2018 {
	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoBehaviour {

		[System.Serializable]
		public class AudioAsset {
			public string name;
			public AudioClip clip;
		}

		private static SoundManager instance;
		public static SoundManager I {
			get {
				if (instance == null) {
					instance = FindObjectOfType<SoundManager>();
					instance.Init();
				}
				return instance;
			}
		}

		public AudioAsset[] assets;
		private AudioSource source;

		public void Init() {
			transform.position = Camera.main.transform.position;
			source = GetComponent<AudioSource>();
		}

		public void PlayUnlocalized(string name) {
			AudioAsset asset = GetAsset(name);
			source.PlayOneShot(asset.clip);
		}

		public AudioAsset GetAsset(string name) {
			for (int i = 0; i < assets.Length; i++) {
				if (assets[i].name == name) return assets[i];
			}
			return null;
		}

	}
}
