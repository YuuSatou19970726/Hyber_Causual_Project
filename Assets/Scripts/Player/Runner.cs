using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [Header("Setting")]
    private bool isTarget;

    public void SetTarget() { isTarget = true; }

    public bool IsTarget() { return isTarget; }
}
