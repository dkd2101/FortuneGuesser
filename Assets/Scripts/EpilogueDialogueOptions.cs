using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilogueDialogueOptions : MonoBehaviour
{
    public int clientID;

    [TextArea(1, 10)]
    public string[] oneStarDialogue;
    [TextArea(1, 10)]
    public string[] twoStarDialogue;
    [TextArea(1, 10)]
    public string[] fourStarDialogue;
    [TextArea(1, 10)]
    public string[] fiveStarDialogue;


    // if review score isn't properly set up, will default to 2-star
    public string[] GetEpilogue()
    {
        int starCount = (UelpSystem.finalScores.Count > clientID) ? UelpSystem.finalScores[clientID] : 2;
        switch(starCount){
            case 1:
                return oneStarDialogue;
            case 2:
                return twoStarDialogue;
            case 4:
                return fourStarDialogue;
            case 5:
                return fiveStarDialogue;
            default:
                return twoStarDialogue;
        }

    }
}
