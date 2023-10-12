using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Movement movement;
    private int currPoint = 0;

    public bool loop = false;
    public float speed = 1;


    void Start()
    {
        movement = GetComponent<Movement>();
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currPoint].position) > 1.1)
        {
            movement.MoveTowards(patrolPoints[currPoint].position, speed);
        }
        else if (currPoint < patrolPoints.Length - 1 || loop)
        {

            currPoint = currPoint + 1;
            if (currPoint >= patrolPoints.Length)
            {
                currPoint = 0;
            }
        }
        else
        {

            movement.Move(Vector2.zero);
        }


    }
}
