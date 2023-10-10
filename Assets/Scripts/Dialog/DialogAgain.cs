using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAgain : MonoBehaviour
{
    public string dialogName;
    public string[] sentences;

    public void Display()
    {
        DialogManager.DisplayText(dialogName, sentences);
    }
}
