using UnityEngine;

public class PQ : MonoBehaviour, IItem
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
