using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    public GameObject CharacterChoicePopup;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenPopup(GameObject popup)
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }

    public void ClosePopup(GameObject popup)
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }
}
