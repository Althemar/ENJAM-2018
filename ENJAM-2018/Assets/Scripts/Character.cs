using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset/Character", menuName = "Character")]
public class Character : ScriptableObject {

    public string name;
    public Sprite sprite;
    public RuntimeAnimatorController animator;
	
}
