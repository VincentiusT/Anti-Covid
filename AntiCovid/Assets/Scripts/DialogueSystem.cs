using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private List<string> paragraphs;
    private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mainPanel;
    private int paragraphIndex = 0;

    private bool isDoneShowing; 

    private void Start()
    {
        canvas.SetActive(true);
        mainPanel.SetActive(false);
        dialogueText = canvas.transform.Find("DialoguePanel/DialogueBox/DialogueText").GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDoneShowing)
        {
            if (paragraphIndex == paragraphs.Count)
            {
                //close dialogue panel
                canvas.transform.Find("DialoguePanel").gameObject.SetActive(false);
                isDoneShowing = true;
                Time.timeScale = 1f;
                Tutorial.instance.StartTutorial();
                mainPanel.SetActive(true);
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
