using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SpriteRenderer selectedCharacterSprite;
    public GameObject[] characterPrefabs;
    public GameObject selectionPopup;


    public Button chooseCharacter;

    private const string characterPrefsKey = "SelectedCharacter";

    private void Awake()
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
    void Start()
    {
        chooseCharacter.onClick.AddListener(OnCharacterClick);
        
    }
    public void OnCharacterClick()
    {
        ShowPopup();
    }
    void ShowPopup()
    {
        selectionPopup.SetActive(true);
    }

    void HidePopup()
    {
        selectionPopup.SetActive(false);
    }

    public void SelectCharacter(Sprite sprite, string characterName)
    {
        selectedCharacterSprite.sprite = sprite;
        HidePopup();
        PlayerPrefs.SetString(characterPrefsKey, characterName);
        PlayerPrefs.Save();
    }
    public GameObject FindPrefabBySprite(Sprite sprite)
    {
        foreach (GameObject prefab in characterPrefabs)
        {
            SpriteRenderer renderer = prefab.GetComponent<SpriteRenderer>();
            if (renderer != null && renderer.sprite == sprite)
            {
                return prefab;
            }
        }
        return null;
    }
}
