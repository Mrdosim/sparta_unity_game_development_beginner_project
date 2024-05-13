using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance;
    public GameObject CharacterList;
    public Text CharacterListText;
    public GameObject CharacterChange;
    public GameObject characterNameChange;
    public InputField characterNameInputField;
    private string _changedName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        characterNameInputField.onValueChanged.AddListener(delegate { ValidateNameInput(); });
    }

    public void UpdateCharacterList()
    {
        List<string> texts = CollectTextsFromLayers(new string[] { "Player", "Npc" });
        CharacterListText.text = "";
        foreach (string text in texts)
        {
            CharacterListText.text += text + "\n";
        }
    }

    private List<string> CollectTextsFromLayers(string[] layers)
    {
        List<string> collectedTexts = new List<string>();
        Text[] allTexts = FindObjectsOfType<Text>(true);

        foreach (Text textComponent in allTexts)
        {
            GameObject rootObj = GetRootGameObject(textComponent.gameObject);
            if (rootObj != null)
            {
                int objLayer = rootObj.layer;
                foreach (string layer in layers)
                {
                    int layerMask = LayerMask.NameToLayer(layer);
                    if (objLayer == layerMask && !collectedTexts.Contains(textComponent.text))
                    {
                        collectedTexts.Add(textComponent.text);
                    }
                }
            }
        }
        return collectedTexts;
    }

    private GameObject GetRootGameObject(GameObject obj)
    {
        Transform current = obj.transform;
        while (current.parent != null)
        {
            current = current.parent;
        }
        return current.gameObject;
    }

    public void OnCharacterSelected(GameObject characterObject)
    {
        if (characterObject == null)
        {
            return;
        }

        SpriteRenderer spriteRenderer = characterObject.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
        {
            GameObject prefab = GameManager.Instance.FindPrefabWithSprite(spriteRenderer.sprite);
            if (prefab != null)
            {
                ReplaceCharacter(prefab, characterObject.transform.position, GameManager.Instance.playerName);
                PopupManager.Instance.ClosePopup(CharacterChange);
            }
        }
    }

    private void ReplaceCharacter(GameObject newPrefab, Vector3 spawnPosition, string characterName)
    {
        // 기존 캐릭터 찾기
        GameObject existingCharacter = GameObject.FindWithTag("Player");
        if (existingCharacter != null)
        {
            spawnPosition = existingCharacter.transform.position;
            characterName = existingCharacter.GetComponentInChildren<Text>().text;
            Destroy(existingCharacter);
        }

        // 새로운 캐릭터 인스턴스화
        GameObject newCharacter = Instantiate(newPrefab, spawnPosition, Quaternion.identity);
        newCharacter.tag = "Player";
        Text nameTextComponent = newCharacter.GetComponentInChildren<Text>();
        if (nameTextComponent != null)
        {
            nameTextComponent.text = characterName;
        }
    }
    private bool ValidateNameInput()
    {
        bool isValid = !string.IsNullOrEmpty(characterNameInputField.text) && characterNameInputField.text.Length >= 2 && characterNameInputField.text.Length <= 10;
        _changedName = isValid ? characterNameInputField.text : "";
        return isValid;
    }
    public void ChangeName()
    {
        if (ValidateNameInput())
        {
            GameManager.Instance.SetPlayerName(_changedName);
            PlayerPrefs.SetString("CharacterName", _changedName);
            GameObject existingCharacter = GameObject.FindWithTag("Player");
            Text characterText = existingCharacter.GetComponentInChildren<Text>();
            characterText.text = characterNameInputField.text;
            characterNameChange.SetActive(false);
        }
    }
}
