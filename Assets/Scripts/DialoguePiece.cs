using UnityEngine;

[CreateAssetMenu(fileName = "New DialoguePiece", menuName = "ScriptableObjects/DialoguePiece")]
public class DialoguePiece : ScriptableObject
{
    public string[] clientDialogue;
    public string[] choiceNames;
    public DialoguePiece[] choiceOutcomes;

    [Header("The following values should only be edited if this dialogue piece has no choices and ends the level.")]
    public bool endPiece = false;
    public int scoreValue = 0;
}
