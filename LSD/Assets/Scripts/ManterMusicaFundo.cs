using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManterMusicaFundo : MonoBehaviour
{
    void Awake()
    {
        // Bloqueia destruicao do objeto com a musica de fundo
        DontDestroyOnLoad(transform.gameObject);
    }
}
