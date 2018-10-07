using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{
    public class MainMenu : MonoBehaviour
    {
        // Use this for initialization
        void Start() {
            SoundManager.Instance.PlayUnlocalized("Menu");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}