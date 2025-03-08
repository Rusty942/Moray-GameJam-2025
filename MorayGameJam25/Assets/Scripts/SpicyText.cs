using System.Collections;
using UnityEngine;
using TMPro;

public class SpicyText : MonoBehaviour
{
    public TextMeshPro tmp;
    public string fullText1;
    public string fullText2;
    public string fullText3;
    public GameObject square;
    public TextMeshPro skip;
    public GameObject x;
    public TextMeshPro continueTxt;
    
    private bool isSkipping = false;
    private bool isTextFullyRevealed = false;
    private int dialogueIndex = 0;
    private string[] dialogues;

    void Start()
    {
        dialogues = new string[] { fullText1, fullText2, fullText3 };
        ResetUI();
        StartCoroutine(RevealText(dialogues[dialogueIndex]));
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isTextFullyRevealed)
            {
                SkipText();
            }
        }
        else if (Input.GetButtonDown("Submit"))
        {
            if (isTextFullyRevealed)
            {
                ContinueDialogue();
            }
        }
    }

    private void SkipText()
    {
        isSkipping = true;
        tmp.text = dialogues[dialogueIndex];
        FinalizeTextReveal();
    }

    private void ContinueDialogue()
    {
        if (dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;
            ResetUI();
            StartCoroutine(RevealText(dialogues[dialogueIndex]));
        }
    }

    private IEnumerator RevealText(string fullText)
    {
        tmp.text = "";
        isSkipping = false;
        isTextFullyRevealed = false;

        for (int i = 0; i < fullText.Length; i++)
        {
            if (isSkipping)
            {
                tmp.text = fullText;
                break;
            }
            tmp.text += fullText[i];
            yield return new WaitForSeconds(0.05f);
        }

        FinalizeTextReveal();
    }

    private void FinalizeTextReveal()
    {
        isTextFullyRevealed = true;
        skip.text = "";
        square.SetActive(false);
        x.SetActive(true);
        continueTxt.text = "CONTINUE";
    }

    private void ResetUI()
    {
        tmp.text = "";
        isTextFullyRevealed = false;
        skip.text = "SKIP";
        square.SetActive(true);
        x.SetActive(false);
        continueTxt.text = "";
    }
}
