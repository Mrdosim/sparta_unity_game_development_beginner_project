﻿using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void OpenPopup(GameObject popupObject)
    {
        popupObject.SetActive(true);
    }

    public void ClosePopup(GameObject popupObject)
    {
        popupObject.SetActive(false);
    }
}
