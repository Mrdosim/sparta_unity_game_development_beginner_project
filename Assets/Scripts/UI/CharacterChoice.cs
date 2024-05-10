using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoice : MonoBehaviour
{
    public GameManager gameManager;
    public Button thisButton;
    public SpriteRenderer characterSprite;
    public string characterName;  // ĳ���� �̸� �߰�

    private void Start()
    {
        if (thisButton == null) thisButton = GetComponent<Button>();
        if (thisButton != null)
        {
            thisButton.onClick.AddListener(() => gameManager.SelectCharacter(characterSprite.sprite, characterName));
        }
    }
}
