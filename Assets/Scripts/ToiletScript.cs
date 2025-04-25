using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToiletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameController.Instance != null && GameController.Instance.toiletUnlocked)
            {
                Debug.Log("TOILETTE ATTEINTE, VICTOIRE !");
                Time.timeScale = 0f;
                // Tu peux ici lancer un menu Win ou charger la scène :
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                Debug.Log("Pas encore assez de PQ mon reuf...");
            }
        }
    }
}
