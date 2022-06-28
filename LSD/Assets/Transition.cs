using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator animator;
    public bool triggerNextLevel = false;
    public bool triggerMainMenu = false;
    public float transitionDelayTime = 1.0f;

    void Awake()
    {
        animator = GameObject.Find("Transition").GetComponent<Animator>();
    }

    void Update()
    {
        if(triggerNextLevel)
        {
            LoadNextLevel();
            triggerNextLevel = false;
        }
        if(triggerMainMenu)
        {
            LoadMainMenu();
            triggerMainMenu = false;
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(DelayLoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(DelayLoadLevel(0));
    }

    IEnumerator DelayLoadLevel(int index)
    {
        animator.SetTrigger("TriggerTransition");
        yield return new WaitForSeconds(transitionDelayTime);
        SceneManager.LoadScene(index);
    }
}
