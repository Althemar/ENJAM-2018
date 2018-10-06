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

    private GameObject[] insertedCharacters = new GameObject[4];


    public bool[] selectedCharacters = new bool[4];

    public enum Players : int {
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
        Player4 = 3
    }


    [SerializeField] Players owner;

    ControllerMapping inputManager;

    string validationButton = "0";
    string annulationButton = "1";

    public int GetRandomPlayer()
    {
        return Random.Range(0, 4);
    }

    void Start () {
        for (int i = 0; i < 4; i++)
        {
            Image img = panels[i].GetComponent<Image>();
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

    int nextPlayer()
    {
        int randomPlayerPrefab = GetRandomPlayer();

        if (selectedCharacters[randomPlayerPrefab] == false)
        {
            return randomPlayerPrefab;
        }

        for (int i = 0; i<4; i++)
        {
            if (selectedCharacters[i] == false)
            {
                return i;
            }
        }

        return -1;
    }
	
	void Update () {

        //Tester touts les jouers
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown("joystick " + (int)(i + 1) + " button " + validationButton))
            {
                //If A Acccept, If B Debug.Log("Joueur A " + i);

                int player = nextPlayer();

                if (player != -1 && selectedCharacters[i] == false)
                {
                    Vector3 pos = new Vector3(0, 0, 0);
                    GameObject a = Instantiate(charactersPrefabs[player]);
                    a.transform.SetPositionAndRotation(pos, Quaternion.identity);
                    a.transform.SetParent(panels[i].transform, false);

                    Image img = panels[i].GetComponent<Image>();
                    img.color = UnityEngine.Color.green;

                    insertedCharacters[i] = a;
                    selectedCharacters[i] = true;
                }
            }

            if (Input.GetKeyDown("joystick " + (int)(i+1) + " button " + annulationButton)) // Dans le cas xBox
            {
                //If A Acccept, If B Debug.Log("Joueur B " + i);
                selectedCharacters[i] = false;
                Image img = panels[i].GetComponent<Image>();
                img.color = UnityEngine.Color.grey;

                Destroy(insertedCharacters[i]);
            }
        }
    }
}
