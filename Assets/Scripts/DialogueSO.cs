using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    // the name of the character to be displayed
    [SerializeField] private string name;

    // an array of strings with each index being a sentence
    // Text area allows us to input from the Unity Scene
    [TextArea(3, 10)]
    [SerializeField] private string[] sentences;

    // objFocus represents a gameObject relevant to the monologue,
    // it is instantiated during this dialogue if filled out
    [Header("use only during prologue/monologue")]
    public GameObject objFocus;
    public bool eraseAfterDialogue;

    public string Name => name;

    public string[] Sentences => sentences;
}