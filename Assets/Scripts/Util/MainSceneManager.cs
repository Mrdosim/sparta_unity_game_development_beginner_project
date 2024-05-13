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
        // Player�� NPC ���̾��� ��ü�鿡�� �ؽ�Ʈ�� ����
        List<string> texts = CollectTextsFromLayers(new string[] { "Npc","Player" });

        // ������ �ؽ�Ʈ�� �ϳ��� ���
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