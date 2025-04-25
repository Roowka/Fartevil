using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    int progressAmount;
    public Slider progressSlider;
    
    public bool toiletUnlocked = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        PQ.OnPQCollect += IncreaseProgressAmount;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        Debug.Log(progressAmount);
        progressSlider.value = progressAmount;
        if (progressAmount >= 100)
        {
            // Chiottes dévérouillées
            toiletUnlocked = true;
            Debug.Log("BIEN JOUE BG LAA " + progressAmount + " "+ toiletUnlocked);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
