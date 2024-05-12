using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 5.0f;
    public string targetLayerName = "Player"; // 추적할 대상의 레이어 이름

    private Vector3 offset;

    private void Start()
    {
        // 레이어 이름을 이용해 추적 대상 찾기
        if (target == null)
        {
            target = FindTargetByLayerName(targetLayerName);
        }

        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }

    // 레이어 이름에 따른 첫 번째 객체의 Transform을 반환
    private Transform FindTargetByLayerName(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        GameObject[] gameObjects = FindGameObjectsInLayer(layer);
        if (gameObjects.Length > 0)
        {
            return gameObjects[0].transform;
        }
        return null;
    }

    // 지정된 레이어에 있는 모든 GameObject를 배열로 반환
    private GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        return goList.ToArray();
    }
}