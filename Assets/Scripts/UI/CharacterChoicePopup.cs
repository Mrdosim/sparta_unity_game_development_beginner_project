using UnityEngine;

public class ChracterChoicePopup : MonoBehaviour
{
    public void OnCharacterClicked(GameObject characterObject)
    {
        SpriteRenderer spriteRenderer = characterObject.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
        {
            GameManager.Instance.characterDisplay.sprite = spriteRenderer.sprite;
            GameManager.Instance.selectedPrefab = GameManager.Instance.FindPrefabWithSprite(spriteRenderer.sprite);
            PopupManager.Instance.ClosePopup(PopupManager.Instance.CharacterChoicePopup);
        }

    }
}