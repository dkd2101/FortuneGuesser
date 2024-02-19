using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonBehavior : MonoBehaviour
{
    public string _toSceneName;

    public void OnClick()
    {
        SceneManager.LoadScene(_toSceneName);
    }
}
