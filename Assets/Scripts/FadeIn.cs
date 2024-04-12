using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float transitionLength = 3;
    private float currentTimer = 0;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentTimer >= transitionLength)
        {
            return;
        }
        else
        {
            currentTimer += Time.deltaTime;
            currentTimer = currentTimer > transitionLength ? transitionLength : currentTimer;
            sprite.color = new Color(1, 1, 1, currentTimer / transitionLength);
        }
    }
}
