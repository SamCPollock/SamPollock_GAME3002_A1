using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWithArc : MonoBehaviour
{
    public float speed = 10f;
    public int predictionStepsPerFrame = 6;
    public Vector3 projectileVelocity;

    // Start is called before the first frame update
    void Start()
    {
        projectileVelocity = this.transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point1 = this.transform.position;
        float stepSize = 1.0f / predictionStepsPerFrame;

        for (float step = 0; step < 1; step += stepSize)
        {
            projectileVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + projectileVelocity * stepSize  * Time.deltaTime;

            Ray ray = new Ray(point1, point2 - point1);

            if (Physics.Raycast(ray, (point2 - point1).magnitude))
            {
                Debug.Log("HIT");
            }
            point1 = point2;
            
        }
        this.transform.position = point1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 point1 = this.transform.position;
        float stepSize = 0.01f;
        Vector3 predictedProjectileVelocity = projectileVelocity;

        for (float step = 0; step < 1; step += stepSize)
        {
            predictedProjectileVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + predictedProjectileVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
               
    }
}
