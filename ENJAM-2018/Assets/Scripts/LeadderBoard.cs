using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeadderBoard : MonoBehaviour {

    public GameObject panel;

    // Score Pair ##
    //  - Name
    //  - Score
    public GameObject scorePair;

    public GameObject[] scores = new GameObject[4];


    void Start () { 
        for(int i = 0; i< 4; i++)
        {
            Vector3 pos = new Vector3(0, -((i)*20), 0);
            GameObject a = Instantiate(scorePair);
            a.transform.SetPositionAndRotation(pos, Quaternion.identity);
            a.transform.SetParent(panel.transform, false);
            scores[i] = a;
        }

        setScores(1, 100);
	}
	
	void Update () {
   
    }

    public void setScores(int player, int score)
    {
        //GetChild() -> 1 = Score -> GetComponent<Text>
        Text s = scores[player].transform.GetChild(1).GetComponent<Text>();
        s.text = score.ToString();
    }
}
