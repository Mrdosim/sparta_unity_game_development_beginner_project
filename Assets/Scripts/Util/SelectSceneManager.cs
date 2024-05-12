using UnityEngine;

public class SelectSceneManager :MonoBehaviour
{
    public GameObject characterSelectPopup;  // Ä³¸¯ÅÍ ¼±ÅÃ ÆË¾÷ °´Ã¼

    void Start()
    {
        PopupManager.Instance.RegisterPopup("CharacterChoicePopup", characterSelectPopup);
    }

    void OnDestroy()
    {
        PopupManager.Instance.UnregisterPopup("CharacterChoicePopup");
    }
}