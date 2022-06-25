using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class ActivateLSD : MonoBehaviour
{
    [Header("Audio")]
	public AudioSource audioSource;
    [Header("Visual")]
	public Volume PPVolume;
    [Header("Estado")]
	public bool ativo = false;
    [Header("Acao")]
    public InputAction lsd = new InputAction("lsd", binding: "<Keyboard>/L");
    
    private void Awake() {
        //Se houver, pegar fonte de audio
        audioSource = GetComponent<AudioSource> ();
        //Pega o GameObject com esse script (poderia ser o gO de nome PostProcessing tambem)
        PPVolume = GetComponent<Volume> ();  
    }

    private void Start() {
        lsd.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(lsd.triggered){
            ativo = !ativo;
            if(ativo) audioSource.Play(); //Toca som quando esta mudando de inativo para ativo
            PPVolume.enabled = ativo;
            //Debug.Log("LSD trigger");
        }
    }
}
