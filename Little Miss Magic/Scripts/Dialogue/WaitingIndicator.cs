using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] string waitingText;
    [SerializeField] float rate;

    char[] characters;

    string builtText;
    float t;
    int i;

    private void Awake()
    {
        characters = waitingText.ToCharArray();
    }

    private void OnDisable()
    {
        t = 0;
        i = 0;
        builtText = string.Empty;
        textField.text = builtText;
    }

    void Update()
    {
        t += Time.deltaTime * rate;

        while (t > 1)
        {
            if (i < characters.Length)
            {
                builtText += characters[i];
                i++;
                t -= 1;
            }
            else
            {
                builtText = string.Empty;
                i = 0;
                t -= 1;
            }

            textField.text = builtText;
        }
    }

}
