using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLeaves : MonoBehaviour {

    public GameObject leaf;
    public GameObject canvas;

    float speed = -0.01f;
    public List<GameObject> leaves;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 9; i++) { 
            GameObject go = Instantiate(leaf);
            
            go.transform.SetParent(canvas.transform, false);
            go.transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10), 5 + Random.Range(-1.5f, 1.5f), 0), Quaternion.identity);
            leaves.Add(go);
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject go in leaves)
        {
            

            if (go.transform.localPosition.y < -216 )
            {
                go.transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10), 5 + Random.Range(-1.5f, 1.5f), 0), Quaternion.identity);
            }
            go.transform.Translate(Mathf.Sin(Random.Range(-0.05f, 0.05f) * Time.time)/10, speed, 0);
        }
	}
}
