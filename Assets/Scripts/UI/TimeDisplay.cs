using UnityEngine;
using TMPro;
using UnityEngine.UI;  // TextMeshPro 네임스페이스를 사용합니다.

public class TimeDisplay : MonoBehaviour
{
    private Text timeText;  // TextMeshProUGUI 컴포넌트를 사용합니다.

    void Start()
    {
        timeText = GetComponentInChildren<Text>();  // 시작할 때 Text 컴포넌트를 가져옵니다.
        if (timeText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found!");
        }
    }

    void Update()
    {
        if (timeText != null)
        {
            timeText.text = System.DateTime.Now.ToString("HH:mm:ss");  // 현재 시간을 "시:분:초" 형태로 표시합니다.
        }
    }
}