using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public static DialogManager Instance;

    public GameObject container;
    public Image imageBox;
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI nameDisplay;
    public string[] sentences;
    public float typingSpeed = 0.05f;
    public int index = 0;

    private Coroutine typingCoroutine;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Update()
    {
        Debug.Log(container.name);
        if (sentences.Length > 0 && Input.anyKeyDown)
        {
            StopCoroutine(typingCoroutine);

            if (index < sentences.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(Type(sentences[index]));
            }
            else
            {
                HideDialog();
            }
        }
    }

    public void ShowDialog()
    {
        Time.timeScale = 0;
        container.SetActive(true);
    }

    public void HideDialog()
    {
        Time.timeScale = 1;
        nameDisplay.text = "";
        textDisplay.text = "";
        container.SetActive(false);
    }

    public static void DisplayText(string name, string[] sentences)
    {
        Instance.ShowDialog();
        Instance.nameDisplay.text = name;
        Instance.sentences = sentences;
        Instance.index = 0;

        Instance.textDisplay.text = "";
        Instance.typingCoroutine = Instance.StartCoroutine(Instance.Type(Instance.sentences[Instance.index]));
    }

    IEnumerator Type(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
}
