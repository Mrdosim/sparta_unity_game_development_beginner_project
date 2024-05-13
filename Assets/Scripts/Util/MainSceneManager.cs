using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance;
    public GameObject CharacterList;
    public Text CharacterListText;
    public GameObject CharacterChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
    }

    public void UpdateCharacterList()
    {
        List<string> texts = CollectTextsFromLayers(new string[] { "Player", "Npc" });
        Debug.Log("Texts collected: " + texts.Count);

        CharacterListText.text = "";  // 기존 텍스트 초기화
        foreach (string text in texts)
        {
            CharacterListText.text += text + "\n";
            Debug.Log("Collected Text: " + text);
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
                        Debug.Log($"Found text in {layer} layer: {textComponent.text}");
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
            Debug.LogError("Character object is null");
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
            else
            {
                Debug.LogError("No matching prefab found for the selected character.");
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
            Debug.Log("Existing character destroyed.");
        }

        // 새로운 캐릭터 인스턴스화
        GameObject newCharacter = Instantiate(newPrefab, spawnPosition, Quaternion.identity);
        newCharacter.tag = "Player";
        Text nameTextComponent = newCharacter.GetComponentInChildren<Text>();
        if (nameTextComponent != null)
        {
            nameTextComponent.text = characterName;
        }
        Debug.Log($"Character instantiated successfully at {spawnPosition} with name {characterName}");
    }
}
