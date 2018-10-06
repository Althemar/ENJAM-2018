using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public List<int> scores;

    public List<int> Scores
    {
        get { return scores; }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}


	
}
