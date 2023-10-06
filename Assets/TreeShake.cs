using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShake : MonoBehaviour
{
    public float swayAmount = 1.0f; // The amount the tree sways
    public float swaySpeed = 1.0f;  // The speed of the sway

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.rotation = Quaternion.Euler(0, 0, sway);
    }
}
