using UnityEngine;
using TMPro;
using UnityEngine.UI;  // TextMeshPro ���ӽ����̽��� ����մϴ�.

public class TimeDisplay : MonoBehaviour
{
    private Text timeText;  // TextMeshProUGUI ������Ʈ�� ����մϴ�.

    void Start()
    {
        timeText = GetComponentInChildren<Text>();  // ������ �� Text ������Ʈ�� �����ɴϴ�.
    }

    void Update()
    {
        if (timeText != null)
        {
            timeText.text = System.DateTime.Now.ToString("HH:mm:ss");  // ���� �ð��� "��:��:��" ���·� ǥ���մϴ�.
        }
    }
}