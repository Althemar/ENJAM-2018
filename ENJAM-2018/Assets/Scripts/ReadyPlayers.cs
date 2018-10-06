using ENJAM2018;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPlayers : MonoBehaviour {

    public GameObject canvas;
    public GameObject tileDown;

    public GameObject[] panels = new GameObject[4];
    public GameObject[] charactersPrefabs = new GameObject[4];

    public enum Players : int {
        Player1 = 1,
        Player2 = 2,
        Player3 = 3,
        Player4 = 4
    }


    [SerializeField] Players owner;

    ControllerMapping inputManager;

    string validationButton = "0";
    string annulationButton = "1";

    public int GetRandomPlayer()
    {
        return Random.Range(1, 5);
    }

    void Start () {
        for (int i = 1; i <= 4; i++)
        {
            Image img = panels[i - 1].GetComponent<Image>();
            img.color = UnityEngine.Color.grey;
        }

        string[] names = Input.GetJoystickNames();
        if (names[(int)owner].Contains("Xbox"))
        {
            inputManager = InputManager.Instance.xboxController;
        }
        else if (names[(int)owner].Contains("Wireless Controller"))
        {
            inputManager = InputManager.Instance.psMapping;
            validationButton = "1";
            annulationButton = "2";
        }
        else
        {
            inputManager = InputManager.Instance.xboxController;
        }
    }
	
	void Update () {
        //Tester touts les jouers
        
        for (int i = 1; i <= 4; i++)
        {
            if (Input.GetKeyDown("joystick " + i + " button " + validationButton))
            {
                //If A Acccept, If B Debug.Log("Joueur A " + i);
                int randomPlayerPrefab = GetRandomPlayer();

                Vector3 pos = new Vector3(0, 0, 0);
                GameObject a = Instantiate(charactersPrefabs[i - 1]);
                a.transform.SetPositionAndRotation(pos, Quaternion.identity);
                a.transform.SetParent(panels[i - 1].transform, false);

                Image img = panels[i - 1].GetComponent<Image>();
                img.color = UnityEngine.Color.green;
            }

            if (Input.GetKeyDown("joystick " + i + " button " + annulationButton)) // Dans le cas xBox
            {
                //If A Acccept, If B Debug.Log("Joueur B " + i);

                Image img = panels[i-1].GetComponent<Image>();
                img.color = UnityEngine.Color.grey;
            }
        }
    }
}
