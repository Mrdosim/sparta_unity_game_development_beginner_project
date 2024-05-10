using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviour
{
    public Text playerNameText; // �̸��� ǥ���� UI Text ���

    private void Start()
    {
        // PlayerPrefs���� ����� �÷��̾� �̸��� ������
        string playerName = PlayerPrefs.GetString("PlayerName");

        // �÷��̾� �̸��� UI Text�� ����
        playerNameText.text = playerName.ToString();
    }
}