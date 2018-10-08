using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public Text name;

    public Image characterSprite;
    public Image characterButton;

    public Sprite JoinButton;
    public Sprite QuitButton;

    public Character selectedCharacter;

    AudioSource audioSource;
    Animator animator;

    bool selected;
    int player = -1;

    public int Player
    {
        get { return player; }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        animator = characterSprite.GetComponent<Animator>();
        QuitGame();


    }

    public bool Selected
    {
        get { return selected; }
    }

    public void JoinGame(int player, Character character) {
        this.player = player;
        selected = true;
        name.text = character.name;
        selectedCharacter = character;
        characterSprite.gameObject.SetActive(true);
        characterSprite.sprite = character.sprite;
        characterButton.sprite = QuitButton;
        audioSource.PlayOneShot(character.selectionSound);
        animator.runtimeAnimatorController = (RuntimeAnimatorController) character.animator;

        characterSprite.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, character.uiAdaptSize);
        characterSprite.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, character.uiAdaptSize);

    }

    public void QuitGame() {
        player = -1;
        selected = false;
        name.text = "Empty";
        selectedCharacter = null;
        characterSprite.sprite = null;
        characterSprite.gameObject.SetActive(false);
        characterButton.sprite = JoinButton;
        animator.runtimeAnimatorController = null;
    }
}
