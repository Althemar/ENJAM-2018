using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset/Character", menuName = "Character")]
public class Character : ScriptableObject {

    public string name;

    [Header("Sprite/Anims")]
    public Sprite sprite;
    public Sprite faceSprite;
    public RuntimeAnimatorController animator;

    [Header("Size")]
    public float adaptSize;
    public float uiAdaptSize;

    [Header("Sounds")]
    public AudioClip selectionSound;
    public AudioClip winSound;
	
}
