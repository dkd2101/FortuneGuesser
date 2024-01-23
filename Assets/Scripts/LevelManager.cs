using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // The relationship score
    float relationshipScore = 0;

    // Audio clips for this level
    public AudioClip levelFailedSFX;
    public AudioClip levelPassedSFX;

    // If the game is over
    public static bool isGameOver = false;

    // Player object, this might actually not be that neccessary
    public GameObject player;

    // The never level title, could also do this with a number and the scene thing i forget what its called but like when you build it and the scenes have #s
    public string nextLevel;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver) { 
            // So long as the level is not over please update
        }
    }

    private void OnGUI()
    {
        // fun GUI stuff in here
    }

    // A method to change the relationship score for the given level
    public void addRelationshipScore(float amount)
    {
        // This ofc needs to get flushed out more, like does it go below 0 and such
        this.relationshipScore += amount;
    }

    // A method to play audio directly to the player. This can be elaborated on if we want to use pitch as a param
    public void playAudio(AudioClip clip) {
        Camera.main.GetComponent<AudioSource>().pitch = 1;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    // Simply casues the level to repeat on call
    public void LevelLost()
    {
        isGameOver = true;
        playAudio(levelFailedSFX);
        LoadCurrentLevel(); // Could also envoke it for a certain amount of time
    }

    // Simply causes the level to progress to the next
    public void LevelBeat()
    {
        isGameOver = true;
        playAudio(levelPassedSFX);
        // check that there is a next scene to go to
        if (!string.IsNullOrEmpty(nextLevel))
        {
            LoadNextLevel(); // Could also envoke for certain amount of time
        }
        else { 
            // do something here!
        }
    }

    // loads the next level
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    // restatrts current level
    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
