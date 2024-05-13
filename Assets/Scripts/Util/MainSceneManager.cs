using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance;
    public GameObject CharacterList;
    public Text CharacterListText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Debug.Log("Script started");
        // Player와 NPC 레이어의 객체들에서 텍스트를 수집
        List<string> texts = CollectTextsFromLayers(new string[] { "Player", "Npc" });
        Debug.Log("Texts collected: " + texts.Count);

        // 수집된 텍스트를 하나씩 출력
        foreach (string text in texts)
        {
            CharacterListText.text += text + "\n";
            Debug.Log("Collected Text: " + text);
        }
    }
    List<string> CollectTextsFromLayers(string[] layers)
    {
        List<string> collectedTexts = new List<string>();
        foreach (string layer in layers)
        {
            int layerMask = LayerMask.GetMask(layer);
            GameObject[] objects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in objects)
            {
                if (((1 << obj.layer) & layerMask) != 0)
                {
                    // 최상위 GameObject에서 Text 컴포넌트를 찾기
                    Text textComponent = GetRootGameObject(obj).GetComponentInChildren<Text>(true);
                    if (textComponent != null && !collectedTexts.Contains(textComponent.text))
                    {
                        collectedTexts.Add(textComponent.text);
                        Debug.Log($"Found text in {layer} layer: {textComponent.text}");
                    }
                }
            }
        }
        return collectedTexts;
    }

    GameObject GetRootGameObject(GameObject obj)
    {
        Transform current = obj.transform;
        while (current.parent != null)
        {
            current = current.parent;
        }
        return current.gameObject;
    }
}