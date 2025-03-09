using System.Collections;
using UnityEngine;
using TMPro;

public class SpicyText : MonoBehaviour
{
    public TextMeshPro tmp;
    public string fullText1;
    public string fullText2;
    public string fullText3;
    public TextMeshPro skip;
    public GameObject x;
    public TextMeshPro continueTxt;

    public GameObject alienSpeech;
    public BarterBox barterBox;  // Reference to BarterBox script

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
        if (Input.GetButtonDown("Submit"))
        {
            if (!isTextFullyRevealed)
            {
                SkipText();
            }
            else
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
        else
        {
            SlideInBarterBox();
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
        continueTxt.text = "CONTINUE";
    }

    private void ResetUI()
    {
        tmp.text = "";
        isTextFullyRevealed = false;
        skip.text = "SKIP";
        continueTxt.text = "";
    }

    private void SlideInBarterBox()
    {
        barterBox.StartCoroutine(barterBox.SlideInBarterBox());
        alienSpeech.SetActive(false);
    }
}
