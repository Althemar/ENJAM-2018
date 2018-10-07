using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "Asset/Character", menuName = "Character")]
public class Character : ScriptableObject {

    public string name;
    public Sprite sprite;
    public AnimatorController animator;
	
}
