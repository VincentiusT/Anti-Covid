using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private List<string> paragraphs;
    private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject canvas;
    private int paragraphIndex = 0;

    private void Start()
    {
        dialogueText = canvas.transform.Find("DialoguePanel/DialogueBox/DialogueText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (paragraphIndex == paragraphs.Count)
            {
                //close dialogue panel
                canvas.transform.Find("DialoguePanel").gameObject.SetActive(false);
                return;
            }

            StopAllCoroutines();
            StartCoroutine(putTextInDialogueBox(paragraphs[paragraphIndex]));
            paragraphIndex++;
        }
    }

    private IEnumerator putTextInDialogueBox(string paragraph)
    {
        dialogueText.text = "";
        int index = 0, paragraphLen = paragraph.Length;

        while(true)
        {
            if (index >= paragraphLen)
                break;
            dialogueText.text += paragraph[index];
            index++;
            yield return null;
        }
    }

}
