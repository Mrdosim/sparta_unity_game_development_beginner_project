using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] characterPrefabs;
    public GameObject selectedPrefab;
    public SpriteRenderer characterDisplay;

    public string playerName;

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

    public string SetPlayerName(string name)
    {
        playerName = name;
        return playerName;
    }

    private void Initialize()
    {
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
    }

    private void UpdateCharacterName(GameObject characterInstance)
    {
        Text nameText = characterInstance.GetComponentInChildren<Text>();
        if (nameText)
        {
            nameText.text = playerName;
        }
    }

    public GameObject FindPrefabWithSprite(Sprite sprite)
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
}