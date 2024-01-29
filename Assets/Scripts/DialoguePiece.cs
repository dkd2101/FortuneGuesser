using UnityEngine;

[CreateAssetMenu(fileName = "New DialoguePiece", menuName = "DialoguePiece")]
public class DialoguePiece : ScriptableObject
{
    public DialogueSO clientDialogue;
    [TextArea(1, 10)]
    public string[] choiceNames;
    public DialoguePiece[] choiceOutcomes;

    [Header("End Piece Variables")]
    public bool endPiece = false;
    public int scoreValue = 0;
}
