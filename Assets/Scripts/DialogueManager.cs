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

    public List<string> sentences;
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

    private AudioClip currentVoice;


    // Start is called before the first frame update
    void Awake()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        this.sentences = new List<string>();
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
        this.currentVoice = dialogue.Voice;
        this.sentences.Clear();
        dialogueOn = true;
        foreach (var sentence in dialogue.Sentences)
        {
            this.sentences.Add(sentence);
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

            if (dialogue.objFocus.CompareTag("Uelp") && UelpSystem._featuredReview != null)
            {
                string[] monologueReact = UelpSystem._featuredReview.monologueReaction;
                for (int i = 0; i < monologueReact.Length; i++)
                {
                    sentences.Add(monologueReact[i]);
                }
            }
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
        else
        {
            if (typewriterEffect.DoneTyping())
            {
                string sentence = sentences[0];
                sentences.RemoveAt(0);
                this._name_text.text = this._name;

                this.ShowTypewriterDialogue(sentence, this.currentVoice);
            }
            else
            {
                typewriterEffect.EndEffect();
            }
        }
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

    public void ShowTypewriterDialogue(string currentDialogue, AudioClip voice)
    {
        if (currentDialogue != null)
        {
            typewriterEffect.Run(currentDialogue, _dialogue_text, voice);
        }
    }
}