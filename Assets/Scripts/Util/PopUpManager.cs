using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    private Dictionary<string, GameObject> popups = new Dictionary<string, GameObject>();

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

    public void RegisterPopup(string popupName, GameObject popup)
    {
        if (!popups.ContainsKey(popupName))
        {
            popups.Add(popupName, popup);
        }
    }

    public void UnregisterPopup(string popupName)
    {
        if (popups.ContainsKey(popupName))
        {
            popups.Remove(popupName);
        }
    }

    public void OpenPopup(string popupName)
    {
        if (popups.ContainsKey(popupName))
        {
            popups[popupName].SetActive(true);
        }
    }

    public void ClosePopup(string popupName)
    {
        if (popups.ContainsKey(popupName))
        {
            popups[popupName].SetActive(false);
        }
    }
}
