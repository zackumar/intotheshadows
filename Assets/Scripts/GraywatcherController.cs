using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GraywatcherController : MonoBehaviour
{
    public GameObject player;
    public float radius = 1f;

    public UnityEvent onEnter;
    public UnityEvent onExit;
    public bool entered = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius && !entered)
        {
            entered = true;
            Movement.LookAt(transform, player.transform);
            onEnter?.Invoke();
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > radius && entered)
        {
            entered = false;
            onExit?.Invoke();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
