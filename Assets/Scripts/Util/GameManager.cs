using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputField nameInputField;
    public GameObject characterSelectPopup;
    public SpriteRenderer characterDisplay;
    public Button joinButton;
    public GameObject[] characterPrefabs;

    private string selectedName;
    private GameObject selectedPrefab;

    void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        Initialize();
    }

    void LateUpdate()
    {
        UpdateSelectedPrefab();
    }

    void OnDestroy()
    {
        Cleanup();
    }

    private void SetupSingleton()
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

    private void Initialize()
    {
        nameInputField.onValueChanged.AddListener(delegate { ValidateNameInput(); });
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Cleanup()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void UpdateSelectedPrefab()
    {
        if (!selectedPrefab)
        {
            selectedPrefab = FindPrefabWithSprite(characterDisplay.sprite);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            InstantiateCharacter();
        }
    }

    private void InstantiateCharacter()
    {
        if (selectedPrefab)
        {
            GameObject instance = Instantiate(selectedPrefab);
            UpdateCharacterName(instance);
        }
        else
        {
            Debug.LogError("Selected prefab is null. Cannot instantiate character.");
        }
    }

    private void UpdateCharacterName(GameObject characterInstance)
    {
        Text nameText = characterInstance.GetComponentInChildren<Text>();
        if (nameText)
        {
            nameText.text = selectedName;
        }
    }

    public void OpenCharacterSelectPopup()
    {
        characterSelectPopup.SetActive(true);
    }

    public void SelectCharacter(GameObject characterObject)
    {
        SpriteRenderer spriteRenderer = characterObject.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
        {
            characterDisplay.sprite = spriteRenderer.sprite;
            selectedPrefab = FindPrefabWithSprite(spriteRenderer.sprite);
            characterSelectPopup.SetActive(false);
        }
    }

    private GameObject FindPrefabWithSprite(Sprite sprite)
    {
        foreach (var prefab in characterPrefabs)
        {
            SpriteRenderer[] renderers = prefab.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var renderer in renderers)
            {
                if (renderer.sprite.name == sprite.name)
                {
                    return prefab;
                }
            }
        }
        return null;
    }

    public void JoinGame()
    {
        if (ValidateNameInput())
        {
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
