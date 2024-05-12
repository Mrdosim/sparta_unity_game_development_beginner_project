using UnityEngine;

public class SelectSceneManager :MonoBehaviour
{
    public GameObject characterSelectPopup;  // ĳ���� ���� �˾� ��ü

    void Start()
    {
        PopupManager.Instance.RegisterPopup("CharacterChoicePopup", characterSelectPopup);
    }

    void OnDestroy()
    {
        PopupManager.Instance.UnregisterPopup("CharacterChoicePopup");
    }
}