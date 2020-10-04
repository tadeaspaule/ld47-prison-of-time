using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class L3KeypadUI : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    public string correctCode;
    L3Keypad keypad;
    public GameManager gm;

    void Start()
    {
        keypad = FindObjectOfType<L3Keypad>();
    }
    
    public void EnterKey(GameObject key)
    {
        string keyText = key.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        Debug.Log(keyText);
        if (keyText.Equals("erase") && inputText.text.Length > 0) inputText.text = inputText.text.Substring(0,inputText.text.Length-1);
        else if (keyText.Equals("enter")) {
            if (inputText.text.Equals(correctCode)) {
                keypad.keypadOpened = true;
                gameObject.SetActive(false);
                gm.blockInput = false;
            }
            else {
                inputText.text = ""; // TODO some error warning show
            }
        }
        else if (inputText.text.Length < 10) inputText.text += keyText;
    }

    public void OpenKeypad(string correctCode)
    {
        Debug.Log("openkeypadui");
        gameObject.SetActive(true);
        this.correctCode = correctCode;
    }
}
