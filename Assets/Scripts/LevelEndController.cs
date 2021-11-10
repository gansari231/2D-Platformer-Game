using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hurray!!!! Level Completed!!!!");
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
        }
    }
}
