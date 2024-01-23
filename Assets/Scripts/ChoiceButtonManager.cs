using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceButtonManager : MonoBehaviour
{
    [HideInInspector] public DialogueTreeManager treeManager;
    [HideInInspector] public DialoguePiece outcome;

    [SerializeField] private TextMeshProUGUI nameText;

    public void OnClick()
    {
        treeManager.UpdateDialoguePiece(outcome);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
