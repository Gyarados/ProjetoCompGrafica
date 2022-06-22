using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Assigned to pressure pads that a box can be dropped on
/// </summary>
public class Pad : MonoBehaviour
{
    public int padId;
    int objectsOnPad = 0;

    /// <summary>
    /// Called whenever a collision occurs with this pad
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.gameObject.GetComponent<Rigidbody>().isKinematic == false) //If a box is placed...
        {
            Debug.Log("Pressure plate ativada!");
            Transform objectTransform = other.gameObject.GetComponent<Transform>();

            if (objectTransform.localScale.x > 1)
            {
            Debug.Log("Pressure plate ativada com o tamanho correto!");

            CursorLockMode lockMode;
            lockMode = CursorLockMode.None;
            Cursor.lockState = lockMode;
            SceneManager.LoadScene(0);

            }

        }
    }

    /// <summary>
    /// Called when a colliding objects stops colliding with the pad
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        
            if (other.gameObject.GetComponent<Rigidbody>() != null) //If it is a box that has stopped colliding...
            {
                objectsOnPad--; //...then reduce the number of colliding objects...
               
            }
        
    }
}