using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

    public float amplitude;
    public float speed;

    private Vector3 origPos;

    // Update is called once per frame
    void Start()
    {
        origPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, origPos.y + Mathf.Sin(speed * Time.time) * amplitude, transform.position.z);
    }
}
