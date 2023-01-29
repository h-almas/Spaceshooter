using System.Collections;
using TMPro;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.enabled = false;
    }


    public void DisplayMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayMessageCoroutine(message));

    }

    private IEnumerator DisplayMessageCoroutine(string message)
    {
        text.enabled = true;
        text.text = message;

        for (int i = 0; i < 6; i++)
        {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);

        text.enabled = false;
    }
}
