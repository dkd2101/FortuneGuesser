using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class DialogueManager : MonoBehaviour
{
    public static bool dialogueOn;

    public Queue<string> sentences;
    private string _name;

    private bool isPrologue;

    private Queue<DialogueSO> _dialogueQueue;

    public TextMeshProUGUI _dialogue_text;
    public TextMeshProUGUI _name_text;

    public GameObject _textBox;

    public KeyCode interact;

    private DialogueTreeManager _dialogueTreeManager;

    public UnityEvent OnDialogueEnded;

    private UnityEvent OnPrologueDialogueEnd;

    public Transform objFocusParent;
    private GameObject currentObjFocus;
    private bool destroyFocus;

    private TypewriterEffect typewriterEffect;


    // Start is called before the first frame update
    void Awake()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        this.sentences = new Queue<string>();
        this._name = "";
        this._dialogueQueue = new Queue<DialogueSO>();
        OnPrologueDialogueEnd = new UnityEvent();
        OnDialogueEnded = new UnityEvent();

    }

    public void SetTreeManager(DialogueTreeManager treeManager)
    {
        _dialogueTreeManager = treeManager;
    }

    public void SetPrologueEndListener(UnityAction action) {
        OnPrologueDialogueEnd.AddListener(action);
    }

    void Update()
    {
        if ((Input.GetKeyDown(interact) || Input.GetMouseButtonDown(0)) && dialogueOn)
        {
            if (PauseMechanic.pauseState)
            {
                return;
            }

            this.DisplayNextSentence();
        }
    }

    public void PlayPrologue(DialogueSO[] dialogue) {
        this.isPrologue = true;
        foreach(DialogueSO d in dialogue) {
            _dialogueQueue.Enqueue(d);
        }
        this.StartDialogue(_dialogueQueue.Dequeue());
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        this.sentences.Clear();
        dialogueOn = true;
        foreach (var sentence in dialogue.Sentences)
        {
            this.sentences.Enqueue(sentence);
        }

        // spawn objFocus (for monologue) if one exists
        if (dialogue.objFocus != null)
        {
            if (currentObjFocus != null)
            {
                Destroy(currentObjFocus);
            }
            currentObjFocus = Instantiate(dialogue.objFocus, objFocusParent);
            destroyFocus = dialogue.eraseAfterDialogue;
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
        this._name_text.text = this._name;
        //this._dialogue_text.text = sentence;

        this.ShowTypewriterDialogue(sentence);
    }

    public void EndDialogue()
    {
        // destroy objFocus (for monologue) if one exists and should be destroyed
        if (destroyFocus && currentObjFocus != null)
        {
            Destroy(currentObjFocus);
            currentObjFocus = null;
        }

        if (!isPrologue)
            _dialogueTreeManager.DisplayChoices();
        if(isPrologue){
            if(_dialogueQueue.Count == 0){
                OnPrologueDialogueEnd.Invoke();
                isPrologue = false;
                return;
            }

            StartDialogue(_dialogueQueue.Dequeue());
            return;
        }
            
        dialogueOn = false;
        // this._textBox.SetActive(false);
    }

    public void ShowTypewriterDialogue(string currentDialogue)
    {
        if (currentDialogue != null)
        {
            typewriterEffect.Run(currentDialogue, _dialogue_text);
        }
    }
}