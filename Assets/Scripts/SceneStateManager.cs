using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    public enum Mode {
        Observer,
        Dialogue
    }

    [SerializeField] private GameObject _dialogueBox;

    [SerializeField] private GameObject _choiceBox;
    public Mode CurMode;

    private List<ClickableObject> interactables = new List<ClickableObject>();

    void Start() {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Interactable")) {
            interactables.Add(g.GetComponent<ClickableObject>());
        }
    }

    public void ToggleMode() {
        Debug.Log("toggling");
        switch(CurMode) {
            case Mode.Observer:
                SetDialogueMode();
                break;
            case Mode.Dialogue:
                SetObserverMode();
                break;
        }
    }
    public void SetObserverMode() {
        CurMode = Mode.Observer;
        DialogueManager.dialogueOn = false;
        _dialogueBox.SetActive(false);
        _choiceBox.SetActive(false);
        foreach(ClickableObject i in interactables) {
            i.SetInteractivity(CurMode);
        }
    }

    public void SetDialogueMode() {
        CurMode = Mode.Dialogue;
        DialogueManager.dialogueOn = true;
        _dialogueBox.SetActive(true);
        _choiceBox.SetActive(true);
        foreach(ClickableObject i in interactables) {
            i.SetInteractivity(CurMode);
        }
    }

}
