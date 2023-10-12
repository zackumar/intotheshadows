using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float speed = 1;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currWaypoint;
    public bool endOfPath = false;

    private Seeker seeker;
    private Movement movement;

    public FieldOfView fov;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        movement = GetComponent<Movement>();
        fov = GetComponent<FieldOfView>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        float dist = Vector2.Distance(transform.position, target.position);
        if (seeker.IsDone() && dist > nextWaypointDistance)
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currWaypoint = 1;
        }
    }


    void FixedUpdate()
    {
        if (path == null) return;


        if (currWaypoint >= path.vectorPath.Count)
        {
            movement.SetVelocity(Vector2.zero);
            endOfPath = true;
            return;
        }
        else
        {
            endOfPath = false;
        }



        Vector2 direction = ((Vector2)path.vectorPath[currWaypoint] - movement.rb.position).normalized;
        Vector2 vel = direction * speed * Time.deltaTime;
        Debug.DrawRay(transform.position, direction);

        movement.SetVelocity(vel);

        float distance = Vector2.Distance(movement.rb.position, path.vectorPath[currWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currWaypoint++;
        }

        if (fov != null)
        {
            float angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angleDeg < 0)
            {
                angleDeg += 360f;
            }

            angleDeg = 360f - angleDeg;

            fov.viewOffset = angleDeg;
        }

    }
}
