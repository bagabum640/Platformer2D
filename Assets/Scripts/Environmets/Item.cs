using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int Amount { get; protected set; }

    public void Collect()
    {
        Destroy(gameObject);
    }
}