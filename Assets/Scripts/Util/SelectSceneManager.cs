using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneManager :MonoBehaviour
{
    public GameObject characterSelectPopup;  // Ä³¸¯ÅÍ ¼±ÅÃ ÆË¾÷ °´Ã¼

    public InputField nameInputField;
    public Button joinButton;

    private string selectedName;


    void Start()
    {
        PopupManager.Instance.RegisterPopup("CharacterChoicePopup", characterSelectPopup);
        nameInputField.onValueChanged.AddListener(delegate { ValidateNameInput(); });
    }

    void OnDestroy()
    {
        PopupManager.Instance.UnregisterPopup("CharacterChoicePopup");
    }

    public void JoinGame()
    {
        if (ValidateNameInput())
        {
            GameManager.Instance.SetPlayerName(selectedName);
            PlayerPrefs.SetString("CharacterName", selectedName);
            SceneManager.LoadScene("MainScene");
        }
    }
    private bool ValidateNameInput()
    {
        bool isValid = !string.IsNullOrEmpty(nameInputField.text) && nameInputField.text.Length >= 2 && nameInputField.text.Length <= 10;
        selectedName = isValid ? nameInputField.text : "";
        return isValid;
    }
}