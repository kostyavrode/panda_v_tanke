using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleIncreaser : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void FixedUpdate()
    {
        Time.timeScale += 0.0001f;
    }
}
