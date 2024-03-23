using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject _deathEffect;

    public static int targetCount;

    private void Start()
    {
        targetCount++;
    }

    private void OnDisable()
    {
        Destroy(Instantiate(_deathEffect, transform.position, transform.rotation), 1f);
        targetCount--;
    }
}
