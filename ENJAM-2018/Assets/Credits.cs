using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENJAM2018
{

    public class Credits : MonoBehaviour
    {

        // Use this for initialization
        void Start() {
            SoundManager.Instance.PlayUnlocalized("Credits");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
