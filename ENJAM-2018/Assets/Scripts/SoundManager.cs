using UnityEngine;

namespace ENJAM2018 {
	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoBehaviour {

		[System.Serializable]
		public class AudioAsset {
			public string name;
			public AudioClip clip;
		}

		public static SoundManager Instance;
		
		public AudioAsset[] assets;
		private AudioSource source;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                source = GetComponent<AudioSource>();

                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        public void Start() {
            
			transform.position = Camera.main.transform.position;
		}

		public void PlayUnlocalized(string name) {
			AudioAsset asset = GetAsset(name);
            source.Stop();
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
