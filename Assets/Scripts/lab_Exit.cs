using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lab_Exit : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with sphere");
        SceneManager.LoadScene("geo2");
    }

    

}

