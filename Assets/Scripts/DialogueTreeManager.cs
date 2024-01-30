using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTreeManager : MonoBehaviour
{
    public DialoguePiece _currentDialoguePiece;
    public Transform _choiceButtonParent;
    public GameObject _choiceButtonPrefab;
    public DialogueManager _dialogueManager;

    private GameObject[] _activeChoiceButtons;

    void Start()
    {
        ClearChoices();
        _dialogueManager.SetTreeManager(this);
        _dialogueManager.StartDialogue(_currentDialoguePiece.clientDialogue);
    }

    public void UpdateDialoguePiece(DialoguePiece dp)
    {
        _currentDialoguePiece = dp;
        ClearChoices();
        _dialogueManager.StartDialogue(_currentDialoguePiece.clientDialogue);
    }

    public void DisplayChoices()
    {
        // make sure that the dialogue piece has the right number of choice names and outcomes
        if (_currentDialoguePiece.choiceNames.Length != _currentDialoguePiece.choiceOutcomes.Length)
        {
            Debug.LogError("Dialogue Piece " + _currentDialoguePiece.name + " must have the same number of choice names and choice outcomes.");
        }

        _activeChoiceButtons = new GameObject[_currentDialoguePiece.choiceOutcomes.Length];

        // set new choice buttons
        for (int i = 0; i < _activeChoiceButtons.Length; i++)
        {
            _activeChoiceButtons[i] = Instantiate(_choiceButtonPrefab, _choiceButtonParent);
            ChoiceButtonManager cbManager = _activeChoiceButtons[i].GetComponent<ChoiceButtonManager>();
            cbManager.treeManager = this;
            cbManager.SetName(_currentDialoguePiece.choiceNames[i]);
            cbManager.outcome = _currentDialoguePiece.choiceOutcomes[i];
        }
    }

    public void ClearChoices()
    {
        // clear any active choice buttons we might currently have
        if (_activeChoiceButtons != null && _activeChoiceButtons.Length > 0)
        {
            for (int i = _activeChoiceButtons.Length - 1; i >= 0; i--)
            {
                Destroy(_activeChoiceButtons[i]);
            }
        }
    }
}
