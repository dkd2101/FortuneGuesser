using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Audio clips for this level
    public AudioClip levelFailedSFX;
    public AudioClip levelPassedSFX;

    [SerializeField] private bool hasPrologue;

    [SerializeField] private DialogueSO[] _prologueDialogue;

    // If the game is over
    // public static bool isGameOver = false;

    // Player object, this might actually not be that neccessary
    // public GameObject player;

    void Start() {
        LevelStart();
    }

    // The never scene title, could also do this with a number and the scene thing i forget what its called but like when you build it and the scenes have #s
    // public string nextScene;
    // ^^ yeah for now lets just do it with build indices and then we can use strings later if we want branching paths

    private void OnGUI()
    {
        // fun GUI stuff in here
    }

    // A method to change the relationship score for the given level
    //public void addRelationshipScore(float amount)
    //{
    //    // This ofc needs to get flushed out more, like does it go below 0 and such
    //    this.relationshipScore += amount;
    //}

    // A method to play audio directly to the player. This can be elaborated on if we want to use pitch as a param
    public void playAudio(AudioClip clip) {
        Camera.main.GetComponent<AudioSource>().pitch = 1;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    //  Pass in a scene to be played at the beginning of the level
    public void LevelStart() {
        if (!this.hasPrologue)
        {
            return;
        }

        DialogueManager dm = this.gameObject.GetComponent<DialogueManager>();

        Debug.Log(dm);

        dm.SetPrologueEndListener(this.LoadNextScene);
        dm.PlayPrologue(this._prologueDialogue);
    }

    // goes directly to the rummaging scene referenced in the Serialized field

    // Simply casues the level to repeat on call
    // Lets change this later. We probably won't repeat levels? Not sure.
    public void LevelLost()
    {
        //isGameOver = true;
        playAudio(levelFailedSFX);
        LoadCurrentLevel(); // Could also envoke it for a certain amount of time
    }

    // Simply causes the level to progress to the next
    public void LevelBeat()
    {
        //isGameOver = true;
        playAudio(levelPassedSFX);
        LoadNextScene();
    }

    // loads the next level
    public void LoadNextScene()
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
    }

    // restarts current level
    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex);
    }
}
