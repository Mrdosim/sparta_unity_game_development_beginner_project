using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager :MonoBehaviour
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
        // Player와 NPC 레이어의 객체들에서 텍스트를 수집
        List<string> texts = CollectTextsFromLayers(new string[] { "Npc","Player" });

        // 수집된 텍스트를 하나씩 출력
        foreach (string text in texts)
        {
            CharacterListText.text += text + "\n";
        }
    }
    List<string> CollectTextsFromLayers(string[] layers)
    {
        List<string> collectedTexts = new List<string>();
        Text[] allTexts = FindObjectsOfType<Text>();

        foreach (Text textComponent in allTexts)
        {
            GameObject obj = textComponent.gameObject;
            int objLayer = obj.layer;
            foreach (string layer in layers)
            {
                int layerMask = LayerMask.NameToLayer(layer);
                if (objLayer == layerMask)
                {
                    if (!collectedTexts.Contains(textComponent.text))
                    {
                        collectedTexts.Add(textComponent.text);
                    }
                    break; 
                }
            }
        }
        return collectedTexts;
    }
}