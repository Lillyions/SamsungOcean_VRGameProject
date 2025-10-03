using System;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    public event Action<Collider> OneHit;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        OneHit?.Invoke(other);
    }
}
