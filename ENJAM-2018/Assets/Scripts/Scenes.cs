using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes : MonoBehaviour {

	public void ChangeScene(string scene) {
        Debug.Log("Changed to scene: " + scene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void Quit() {
		Application.Quit();
    }
}
