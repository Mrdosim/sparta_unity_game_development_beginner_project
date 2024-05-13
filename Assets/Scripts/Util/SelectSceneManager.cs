using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneManager :MonoBehaviour
{
    public static SelectSceneManager Instance;

    public GameObject characterSelectPopup;  // Ä³¸¯ÅÍ ¼±ÅÃ ÆË¾÷ °´Ã¼

    public InputField nameInputField;

    private string selectedName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        nameInputField.onValueChanged.AddListener(delegate { ValidateNameInput(); });
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

    public void OnCharacterClicked(GameObject characterObject)
    {
        SpriteRenderer spriteRenderer = characterObject.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
        {
            GameManager.Instance.characterDisplay.sprite = spriteRenderer.sprite;
            GameManager.Instance.selectedPrefab = GameManager.Instance.FindPrefabWithSprite(spriteRenderer.sprite);
            PopupManager.Instance.ClosePopup(characterSelectPopup);
        }
    }
}