using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonBehavior : MonoBehaviour
{
    public string _toSceneName;
    public int _toSceneNumber;

    public void OnClick()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("LevelManager");
        if (temp != null)
        {
            Debug.Log("here");
            temp.GetComponent<LevelManager>().LoadAScene(_toSceneNumber);
        }
        else
        {
            SceneManager.LoadScene(_toSceneName);
        }
    }
}
