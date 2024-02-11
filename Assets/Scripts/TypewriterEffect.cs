using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public float typewriterSpeed = 50f; //how fast it goes

    private void Start()
    {
        Debug.Log("Current speed: " + typewriterSpeed);
    }
    public Coroutine Run(string typingText, TextMeshProUGUI myText)
    {
        Debug.Log(typingText);
        return StartCoroutine(TypeText(typingText, myText));
    }

    private IEnumerator TypeText(string typingText, TextMeshProUGUI myText)
    {
        myText.text = string.Empty; //clear box

        float t = 0;
        int charindex = 0;

        float currentSpeed = typewriterSpeed; // this cannot be changed mid typeing.

        while (charindex < typingText.Length)
        {
            // if at any time space is pressed just break the while loop.
            if (Input.GetKeyDown(KeyCode.F))
            {
                break;
            }
            // char at a time given the speed
            t += Time.deltaTime * currentSpeed;
            charindex = Mathf.FloorToInt(t);
            charindex = Mathf.Clamp(charindex, 0, typingText.Length);
            myText.text = typingText.Substring(0, charindex);
            yield return null;
        }

        // precaution 
        myText.text = typingText;
    }

    public void setSpeed(float newSpeed)
    {
        typewriterSpeed = newSpeed;
    }

}