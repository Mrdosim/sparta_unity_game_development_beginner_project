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
        // Join 버튼 클릭 시 이벤트 추가
        joinButton.onClick.AddListener(OnJoinButtonClicked);
    }

    private void OnJoinButtonClicked()
    {
        string playerName = nameInputField.text;
        // 이름의 길이가 조건에 맞는지 확인
        if (playerName.Length >= 2 && playerName.Length <= 10)
        {
            PlayerPrefs.SetString(playerNamePrefsKey, playerName);
            PlayerPrefs.Save();
            // 맵으로 이동하는 로직을 여기에 추가
            SceneManager.LoadScene("MainScene");
            Debug.Log("Joining with name: " + playerName);
        }
        else
        {
            Debug.Log("Name must be between 2 and 10 characters long.");
        }
    }
}