using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameObject prefab = GameManager.Instance.FindPrefabBySprite(GameManager.Instance.selectedCharacterSprite.sprite);
            if (prefab != null)
            {
                Instantiate(prefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.LogError("No matching prefab found for the selected sprite.");
            }
        }
    }
}