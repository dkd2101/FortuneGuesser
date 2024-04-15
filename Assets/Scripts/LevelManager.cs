using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Audio clips for this level
    public AudioClip levelFailedSFX;
    public AudioClip levelPassedSFX;

    public UnityEvent tutorialEnded;

    [SerializeField] private bool hasPrologue;

    [SerializeField] private bool isTutorial;

    [SerializeField] private DialogueSO[] _prologueDialogue;

    [SerializeField] private DialogueSO[] _tutorialFinishedDialogue;

    public Image fadeToBlack;

    private bool tutorialCompleted;

    private int tutorialCount;

    // If the game is over
    // public static bool isGameOver = false;


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

    // A method to play audio directly to the player. This can be elaborated on if we want to use pitch as a param
    public void playAudio(AudioClip clip) {
        Camera.main.GetComponent<AudioSource>().pitch = 1;
        Camera.main.GetComponent<AudioSource>().loop = true;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playRepeatAudio(AudioClip clip)
    {
        Camera.main.GetComponent<AudioSource>().pitch = 1;
        Camera.main.GetComponent<AudioSource>().loop = true;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    public void cancelRepeatAudio()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }

    //  Pass in a scene to be played at the beginning of the level
    public void LevelStart() {
        if (!this.hasPrologue)
        {
            return;
        }

        DialogueManager dm = this.gameObject.GetComponent<DialogueManager>();

        Debug.Log(dm);
        if(this.isTutorial) {
            dm.PlayPrologue(this._prologueDialogue);
            this.tutorialEnded.AddListener(this.OnTutorialEnded);
            return;
        }

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
        LoadAScene(gameObject.scene.buildIndex + 1);
    }

    // restarts current level
    private void LoadCurrentLevel()
    {
        LoadAScene(gameObject.scene.buildIndex);
    }

    public void LoadAScene(int scene) {

        if (this.fadeToBlack != null)
        {
            StartCoroutine(IncreaseNumberAndLoadScene(scene));
        }
        else {
            SceneManager.LoadScene(scene);
        }
    }

    private float transparent = 0f;

    IEnumerator IncreaseNumberAndLoadScene(int scene)
    {
        while (true)
        {
            // Increase the current number
            transparent = (transparent + 0.01f);

            if (this.fadeToBlack.color.a > 0.99f)
            {
                Debug.Log("Please get here");
                SceneManager.LoadScene(scene);
                break;
            }

            this.fadeToBlack.color = new Color(this.fadeToBlack.color.r, this.fadeToBlack.color.g, this.fadeToBlack.color.b, transparent);

            yield return null;
        }
    }

    public void IncrementCrystalClicks() {
        this.tutorialCount++;
        if(this.tutorialCount >= 2) {
            this.tutorialEnded.Invoke();
        }
    }

    public void OnTutorialEnded() {
        DialogueManager dm = this.gameObject.GetComponent<DialogueManager>();

        dm.PlayPrologue(_tutorialFinishedDialogue);
        dm.SetPrologueEndListener(this.LoadNextScene);
    }

}
