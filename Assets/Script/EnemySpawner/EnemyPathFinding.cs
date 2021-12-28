using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float moveSpeed = 20f;
    private GameObject[] pathPoints;
    private Vector2 targetPoint;
    private int currentPointIdx = 0;
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        // Find the first target with Tag PathPoint
        currentPointIdx = 1;
        pathPoints = GameObject.FindGameObjectsWithTag("PathPoint");

        targetPoint = FindTargetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the virus from current point to the next point
        transform.position = Vector2.MoveTowards(
            transform.position, 
            targetPoint, 
            Time.deltaTime * moveSpeed
        );

        // When near the target point
        // Find the next target point before arrive
        // If travel to the End Point (Safe Zone), destroy the object
        if (Vector2.Distance(transform.position, targetPoint) < 0.05f)
        {
            currentPointIdx++;
            if (currentPointIdx >= pathPoints.Length)
            {
                // Destroy object when finish travelling
                Destroy(gameObject);
            }
            else
            {
                // Find the next Target point
                targetPoint = FindTargetPoint();
            }
        }
    }

    // Find Next Target Point
    private Vector2 FindTargetPoint()
    {
        return new Vector2(
            pathPoints[currentPointIdx].transform.position.x,
            pathPoints[currentPointIdx].transform.position.y
        );
    }
    #endregion
}
