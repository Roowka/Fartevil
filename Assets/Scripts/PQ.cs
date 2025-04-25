using System;
using UnityEngine;

public class PQ : MonoBehaviour, IItem
{
    public static event Action<int> OnPQCollect;
    public int worth = 10;
    public void Collect()
    {
        OnPQCollect?.Invoke(worth);
        Destroy(gameObject);
    }
}
