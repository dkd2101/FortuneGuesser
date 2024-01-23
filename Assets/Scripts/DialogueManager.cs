using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static bool dialogueOn;

    public Queue<string> sentences;
    private string _name;

    public Text _dialogue_text;
    public Text _name_text;

    public GameObject _textBox;

    public KeyCode interact;


    // Start is called before the first frame update
    void Awake()
    {
        this.sentences = new Queue<string>();
        this._name = "";

    }

    void Update()
    {
        if (Input.GetKeyDown(interact) && dialogueOn)
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
        dialogueOn = false;
        this._textBox.SetActive(false);
    }
}