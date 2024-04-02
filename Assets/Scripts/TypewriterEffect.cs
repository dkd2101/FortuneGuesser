using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class TypewriterEffect : MonoBehaviour
{
    public float typewriterSpeed = 50f; //how fast it goes

    //public AudioClip talkingNoise;
    public AudioSource _audioSource;

    private TextMeshProUGUI textBox;
    private string currentText;

    private void Start()
    {
        Debug.Log("Current speed: " + typewriterSpeed);
    }

    public bool DoneTyping()
    {
        return (textBox == null || currentText == null || textBox.text == currentText);
    }

    public void EndEffect()
    {
        textBox.text = currentText;
        StopAllCoroutines();
    }

    public Coroutine Run(string typingText, TextMeshProUGUI myText, AudioClip voice, float pitch)
    {
        Debug.Log(typingText);
        textBox = myText;
        currentText = typingText;
        return StartCoroutine(TypeText(voice, pitch));
    }

    private IEnumerator TypeText(AudioClip voice, float pitch)
    {

        //FindObjectOfType<LevelManager>().playRepeatAudio(this.talkingNoise);
        textBox.text = string.Empty; //clear box

        float t = 0;
        int charindex = 0;

        float currentSpeed = typewriterSpeed; // this cannot be changed mid typeing.

        while (charindex < currentText.Length)
        {
            // if at any time space is pressed just break the while loop.
            if (Input.GetKeyDown(KeyCode.F))
            {
                break;
            }
            // char at a time given the speed
            t += Time.deltaTime * currentSpeed;
            charindex = Mathf.FloorToInt(t);
            charindex = Mathf.Clamp(charindex, 0, currentText.Length);
            textBox.text = currentText.Substring(0, charindex);
            if (_audioSource != null && !_audioSource.isPlaying)
            {
                _audioSource.clip = voice;
                _audioSource.volume = pitch;
                _audioSource.Play();
            }
            yield return null;
        }

        // precaution 
        textBox.text = currentText;

        //FindObjectOfType<LevelManager>().cancelRepeatAudio();
    }

    public void setSpeed(float newSpeed)
    {
        typewriterSpeed = newSpeed;
    }

}
