using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_example : MonoBehaviour
{
    public AudioSource audioSource;
    private bool condition = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(condition){
            audioSource.Play();
            condition = false;
        }
    }
}
