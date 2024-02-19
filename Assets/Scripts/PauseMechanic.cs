using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMechanic : MonoBehaviour
{
    [HideInInspector] public static bool pauseState;
    [SerializeField] private GameObject _pauseMenu;

    private void Start()
    {
        pauseState = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseState = !pauseState;
            _pauseMenu.SetActive(pauseState);
        }
    }
}
