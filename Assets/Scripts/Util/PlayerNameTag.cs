using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviour
{
    public Text playerNameText; // 이름을 표시할 UI Text 요소

    private void Start()
    {
        // PlayerPrefs에서 저장된 플레이어 이름을 가져옴
        string playerName = PlayerPrefs.GetString("PlayerName");

        // 플레이어 이름을 UI Text에 적용
        playerNameText.text = playerName.ToString();
    }
}