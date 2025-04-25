using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Header("Collect")]
    public AudioSource collectAudioSource;
    public AudioClip collectClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
            PlayCollectSound();
        }
    }
    
    private void PlayCollectSound()
    {
        Debug.Log("Playing collect");
        collectAudioSource.PlayOneShot(collectClip);
    }
}
