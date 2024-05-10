using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNameInput : MonoBehaviour
{
    public InputField nameInputField;
    public Button joinButton;

    private const string playerNamePrefsKey = "PlayerName";

    private void Start()
    {
        if (PlayerPrefs.HasKey(playerNamePrefsKey))
        {
            string savedPlayerName = PlayerPrefs.GetString(playerNamePrefsKey);
            nameInputField.text = savedPlayerName;
        }
        // Join ��ư Ŭ�� �� �̺�Ʈ �߰�
        joinButton.onClick.AddListener(OnJoinButtonClicked);
    }

    private void OnJoinButtonClicked()
    {
        string playerName = nameInputField.text;
        // �̸��� ���̰� ���ǿ� �´��� Ȯ��
        if (playerName.Length >= 2 && playerName.Length <= 10)
        {
            PlayerPrefs.SetString(playerNamePrefsKey, playerName);
            PlayerPrefs.Save();
            // ������ �̵��ϴ� ������ ���⿡ �߰�
            SceneManager.LoadScene("MainScene");
            Debug.Log("Joining with name: " + playerName);
        }
        else
        {
            Debug.Log("Name must be between 2 and 10 characters long.");
        }
    }
}