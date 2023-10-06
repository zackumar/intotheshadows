using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class ShadowDetector : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Light2D> lights;
    public LayerMask layerToIgnore;

    public Collider2D shadowCollider;

    public int points = 10;

    public bool inShadow = true;

    uint shapeHash;
    private PhysicsShapeGroup2D physicsShapeGroup2D = new PhysicsShapeGroup2D();
    private List<PhysicsShape2D> shapes = new List<PhysicsShape2D>();

    public UnityEvent onEvent;
    public UnityEvent offEvent;

    private List<Vector2> vertices = new List<Vector2>();

    void Start()
    {
        Light2D[] allLights = FindObjectsByType<Light2D>(FindObjectsSortMode.None);
        foreach (Light2D light in allLights)
        {
            if (light.shadowsEnabled)
            {
                lights.Add(light);
            }
        }
    }

    private bool prevState = false;

    void Update()
    {

        // if (transform.hasChanged || shadowCollider.GetShapeHash() != shapeHash)
        // {
        // if (transform.hasChanged)
        // transform.hasChanged = false;
        // if (shadowCollider.GetShapeHash() != shapeHash)
        // {
        shapeHash = shadowCollider.GetShapeHash();
        shadowCollider.GetShapes(physicsShapeGroup2D);
        // };

        inShadow = false;

        foreach (Light2D light in lights)
        {
            if (InShadow(light))
            {
                inShadow = true;
            }
        }

        if (inShadow && !prevState)
        {
            Debug.Log("In shadow");
            onEvent.Invoke();
        }
        else if (!inShadow && prevState)
        {
            Debug.Log("Out shadow");
            offEvent.Invoke();
        }

        prevState = inShadow;
    }

    public List<Vector3> castPoints = new List<Vector3>();
    public List<Vector3[]> lines = new List<Vector3[]>();

    bool InShadow(Light2D light)
    {

        castPoints.Clear();
        lines.Clear();
        for (int i = 0; i < physicsShapeGroup2D.shapeCount; i++)
        {
            physicsShapeGroup2D.GetShapeVertices(i, vertices);
            PhysicsShape2D shape = physicsShapeGroup2D.GetShape(i);

            // TODO: Only works on circles right now
            Vector3 lightPosition = light.transform.position;

            foreach (Vector2 vertex in vertices)
            {
                Vector3 transformedVertex = physicsShapeGroup2D.localToWorldMatrix.MultiplyPoint(vertex);

                Vector3 direction = (transformedVertex - lightPosition).normalized;
                Vector3 perpendicular = new Vector3(-direction.y, direction.x, transform.position.z);

                for (int j = 0; j < points; j++)
                {
                    float angle = Mathf.PI * 2 * j / points;

                    Vector3 edgePoint = new Vector3
                    {
                        x = transformedVertex.x + shape.radius * Mathf.Cos(angle),
                        y = transformedVertex.y + shape.radius * Mathf.Sin(angle)
                    };

                    Vector3 vectorToPoint = edgePoint - transformedVertex;

                    if (Vector3.Dot(direction, vectorToPoint) <= 0)
                    {
                        castPoints.Add(edgePoint);
                    }
                }
            }

        }

        foreach (Vector3 point in castPoints)
        {
            RaycastHit2D hit = Physics2D.Raycast(light.transform.position, (point - light.transform.position).normalized, Mathf.Infinity, ~layerToIgnore);
            Debug.DrawRay(light.transform.position, (point - light.transform.position).normalized);

            if (hit.collider == shadowCollider)
            {

                Debug.DrawLine(light.transform.position, hit.point, Color.green);
                lines.Add(new Vector3[] { light.transform.position, hit.point });
            }

        }


        return lines.Count < castPoints.Count;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 p in castPoints)
        {
            Gizmos.DrawSphere(p, 0.02f);
        }

        // Gizmos.color = Color.gray;
        // foreach (Light2D light in lights)
        // {
        //     foreach (Vector3 v in lig)
        //     {

        //         Gizmos.DrawSphere(v, 0.02f);
        //     }
        // }

    }

    // void OnDrawGizmos()
    // {

    //     Gizmos.color = Color.white;
    //     if (shadowCollider.GetShapeHash() != shapeHash)
    //     {
    //         shadowCollider.GetShapes(physicsShapeGroup2D);
    //         for (int i = 0; i < physicsShapeGroup2D.shapeCount; i++)
    //         {
    //             physicsShapeGroup2D.GetShapeVertices(i, vertices);
    //             PhysicsShape2D shape = physicsShapeGroup2D.GetShape(i);

    //             foreach (Light2D light in lights)
    //             {
    //                 Vector3 lightPosition = light.transform.position;

    //                 foreach (Vector2 vertex in vertices)
    //                 {
    //                     Vector3 newVertex = physicsShapeGroup2D.localToWorldMatrix.MultiplyPoint(vertex);

    //                     Vector3 direction = (newVertex - lightPosition).normalized;
    //                     Vector3 perpendicular = new Vector3(-direction.y, direction.x, transform.position.z);


    //                     Gizmos.DrawRay(lightPosition, direction);
    //                     Gizmos.color = Color.red;
    //                     Gizmos.DrawRay(newVertex, perpendicular);

    //                     for (int j = 0; j < points; j++)
    //                     {
    //                         float angle = Mathf.PI * 2 * j / points;
    //                         float x = newVertex.x + shape.radius * Mathf.Cos(angle);
    //                         float y = newVertex.y + shape.radius * Mathf.Sin(angle);

    //                         Vector3 newPoint = new Vector3(x, y, transform.position.z);
    //                         Vector3 vectorToPoint = newPoint - newVertex;

    //                         Gizmos.color = Color.green;
    //                         Gizmos.DrawRay(newVertex, vectorToPoint);

    //                         if (Vector3.Dot(direction, vectorToPoint) <= 0)
    //                         {
    //                             Gizmos.color = Color.red;
    //                             Gizmos.DrawSphere(newPoint, 0.02f);
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //     }
    // }
}
