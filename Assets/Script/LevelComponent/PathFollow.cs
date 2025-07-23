using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float minDistanceToPoint = 0.1f;
    public List<Transform> pathPoints = new List<Transform>(); // Drag points from scene

    private int _currentPointIndex = 0;

    private void Start()
    {
        if (pathPoints.Count > 0)
        {
            transform.position = pathPoints[0].position;
            _currentPointIndex = 1;
        }
    }

    private void Update()
    {
        if (pathPoints.Count == 0) return;

        Transform targetPoint = pathPoints[_currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetPoint.position);
        if (distance < minDistanceToPoint)
        {
            _currentPointIndex++;
            if (_currentPointIndex >= pathPoints.Count)
            {
                _currentPointIndex = 0; // Loop
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < pathPoints.Count; i++)
        {
            if (pathPoints[i] != null)
                Gizmos.DrawWireSphere(pathPoints[i].position, 0.3f);

            if (i < pathPoints.Count - 1 && pathPoints[i] != null && pathPoints[i + 1] != null)
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
        }

        // Connect last to first
        if (pathPoints.Count > 1 && pathPoints[0] != null && pathPoints[^1] != null)
        {
            Gizmos.DrawLine(pathPoints[^1].position, pathPoints[0].position);
        }
    }
}
