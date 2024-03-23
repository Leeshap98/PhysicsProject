using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance;

    [SerializeField] WindController _windController;
    public bool Gravity = true;
    public bool IsWind = true;

    private void Awake()
    {
        Instance = this;
    }


}
