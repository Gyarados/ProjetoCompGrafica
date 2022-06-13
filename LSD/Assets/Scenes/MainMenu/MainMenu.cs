using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame ()
    {
        CursorLockMode lockMode;
        lockMode = CursorLockMode.Locked;
        Cursor.lockState = lockMode;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
