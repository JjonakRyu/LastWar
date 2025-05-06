using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float range = 5f; // Initial range

    public void IncreaseRange(float amount)
    {
        range += amount;
    }
}
