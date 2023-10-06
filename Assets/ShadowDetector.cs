using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class ShadowDetector : MonoBehaviour
{

    public List<Light2D> lights;
    public LayerMask layerToIgnore;
    public Collider2D shadowCollider;
    public int points = 10;
    public bool inShadow = true;
    public UnityEvent onEvent;
    public UnityEvent offEvent;


    private PhysicsShapeGroup2D physicsShapeGroup2D = new PhysicsShapeGroup2D();
    private List<Vector2> vertices = new List<Vector2>();
    private uint shapeHash;



    private void Start()
    {
        lights.AddRange(FindObjectsByType<Light2D>(FindObjectsSortMode.None));
        lights.RemoveAll(light => !light.shadowsEnabled);
    }

    private bool prevState = false;

    private void Update()
    {
        // if (shapeHash != shadowCollider.GetShapeHash())
        // {
        shapeHash = shadowCollider.GetShapeHash();
        shadowCollider.GetShapes(physicsShapeGroup2D);
        // }

        inShadow = false;

        foreach (Light2D light in lights)
        {
            if (InShadow(light))
            {
                inShadow = true;
                break;
            }
        }

        if (inShadow != prevState)
        {
            if (inShadow)
            {
                onEvent.Invoke();
                Debug.Log("In shadow");
            }
            else
            {
                offEvent.Invoke();
                Debug.Log("Out shadow");
            }
        }

        prevState = inShadow;
    }

    private List<Vector3> castPoints = new List<Vector3>();
    private List<Vector3[]> lines = new List<Vector3[]>();

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


    }

}
