using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static bool dialogueOn;

    public Queue<string> sentences;
    private string _name;

    public TextMeshProUGUI _dialogue_text;
    public TextMeshProUGUI _name_text;

    public GameObject _textBox;

    public KeyCode interact;

    private DialogueTreeManager _dialogueTreeManager;


    // Start is called before the first frame update
    void Awake()
    {
        this.sentences = new Queue<string>();
        this._name = "";

    }

    public void SetTreeManager(DialogueTreeManager treeManager)
    {
        _dialogueTreeManager = treeManager;
    }

    void Update()
    {
        if ((Input.GetKeyDown(interact) || Input.GetMouseButtonDown(0)) && dialogueOn)
        {
            this.DisplayNextSentence();
        }
    }


    public void StartDialogue(DialogueSO dialogue)
    {
        this.sentences.Clear();
        dialogueOn = true;
        foreach (var sentence in dialogue.Sentences)
        {
            this.sentences.Enqueue(sentence);
        }

        this._name = dialogue.Name;

        this._textBox.SetActive(true);

        this.DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            Debug.Log("ending the dialogue");
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        this._dialogue_text.text = sentence;
        this._name_text.text = this._name;
    }

    public void EndDialogue()
    {
        _dialogueTreeManager.DisplayChoices();
        dialogueOn = false;
        this._textBox.SetActive(false);
    }
}