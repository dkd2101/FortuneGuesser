using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTreeManager : MonoBehaviour
{
    public DialoguePiece currentDialoguePiece;
    public Transform choiceButtonParent;
    public GameObject choiceButtonPrefab;

    private GameObject[] activeChoiceButtons;

    void Start()
    {
        ResetChoiceButtons();
    }

    public void UpdateDialoguePiece(DialoguePiece dp)
    {
        currentDialoguePiece = dp;
        ResetChoiceButtons();
    }

    private void ResetChoiceButtons()
    {
        // clear any active choice buttons we might currently have
        if (activeChoiceButtons != null && activeChoiceButtons.Length > 0)
        {
            for (int i = activeChoiceButtons.Length - 1; i >= 0; i--)
            {
                Destroy(activeChoiceButtons[i]);
            }
        }

        // make sure that the dialogue piece has the right number of choice names and outcomes
        if (currentDialoguePiece.choiceNames.Length != currentDialoguePiece.choiceOutcomes.Length)
        {
            Debug.LogError("Dialogue Piece " + currentDialoguePiece.name + " must have the same number of choice names and choice outcomes.");
        }

        activeChoiceButtons = new GameObject[currentDialoguePiece.choiceOutcomes.Length];

        // set new choice buttons
        for (int i = 0; i < activeChoiceButtons.Length; i++)
        {
            activeChoiceButtons[i] = Instantiate(choiceButtonPrefab, choiceButtonParent);
            ChoiceButtonManager cbManager = activeChoiceButtons[i].GetComponent<ChoiceButtonManager>();
            cbManager.treeManager = this;
            cbManager.SetName(currentDialoguePiece.choiceNames[i]);
            cbManager.outcome = currentDialoguePiece.choiceOutcomes[i];
        }
    }
}
