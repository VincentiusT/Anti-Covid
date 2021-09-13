using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public string BeginningStoryID;
    [SerializeField] private string[] paragraphs;
    private TextMeshProUGUI dialogueText;
    private Image dialogueCharacterSprite;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mainPanel, extraPanel;
    [SerializeField] private Sprite defaultCharacterSprite;
    private DialogueDatabase dialogueDatabase;
    private int paragraphIndex = 0;

    private bool isDoneShowing; 

    private void Start()
    {
        dialogueDatabase = GetComponent<DialogueDatabase>();
        dialogueText = canvas.transform.Find("DialoguePanel/DialogueBox/DialogueText").GetComponent<TextMeshProUGUI>();
        dialogueCharacterSprite = canvas.transform.Find("DialoguePanel/char").GetComponent<Image>();

        SetUpDialoguePanel(BeginningStoryID, null);
    }

    private void SetUpDialoguePanel(string dialogueName, Sprite charSprite)
    {
        dialogueCharacterSprite.sprite = charSprite == null? defaultCharacterSprite : charSprite;
        paragraphs = dialogueDatabase.dialogue[dialogueName];

        canvas.SetActive(true);
        mainPanel.SetActive(false);
        if(extraPanel!=null)extraPanel.SetActive(false);
        Time.timeScale = 0f;
        isDoneShowing = false;

        paragraphIndex = 0;
        ShowNextSentence();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDoneShowing)
        {
            if (paragraphIndex == paragraphs.Length)
            {
                //close dialogue panel
                //canvas.transform.Find("DialoguePanel").gameObject.SetActive(false);
                canvas.SetActive(false);
                isDoneShowing = true;
                Time.timeScale = 1f;
                Tutorial.instance.StartTutorial();
                mainPanel.SetActive(true);
                if (extraPanel != null) extraPanel.SetActive(true);
                return;
            }
            ShowNextSentence();
        }
    }

    private void ShowNextSentence()
    {
        StopAllCoroutines();
        StartCoroutine(putTextInDialogueBox(paragraphs[paragraphIndex]));
        paragraphIndex++;
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

    public void PlayNewDialogue(string dialogueName)
    {
        SetUpDialoguePanel(dialogueName, null);
    }

    public void PlayNewDialogue(string dialogueName, Sprite characterSprite)
    {
        SetUpDialoguePanel(dialogueName, characterSprite);
    }
}
