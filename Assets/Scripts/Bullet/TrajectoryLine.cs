using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Gun gun; // Make sure this Gun script provides speed and gravityScale
    [SerializeField] private Transform gunTip; // Point from where the projectile is launched

    [Header("Trajectory Line Settings")]
    [SerializeField, Range(10, 100)] private int _segmentCount = 50;
    [SerializeField, Range(0.1f, 5.0f)] private float _predictionTime = 2.0f; // Total time to predict the trajectory

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            Debug.LogError("TrajectoryLine requires a LineRenderer component!", this);
            enabled = false; // Disable the script if no LineRenderer
            return;
        }
        lr.positionCount = _segmentCount;

        Debug.Log("Gravity in TrajectoryLine script equals: " + gun.gravity);
    }

    private void Update()
    {
        // Ensure gun and gunTip are assigned
        if (gun == null || gunTip == null)
        {
            lr.enabled = false; // Hide the line if references are missing
            return;
        }
        lr.enabled = true; // Show the line if references are good

        Vector3 startPosition = gunTip.position;
        // Determine the initial velocity vector based on gunTip's forward direction and projectile speed
        // Assuming 'gunTip.right' is the forward shooting direction for a 2D side-scroller
        // If your gun sprite is oriented such that its 'up' vector points forward, use gunTip.up
        Vector3 initialVelocity = gunTip.right * gun.speed;
        // Or if your gun script provides the actual shot direction:
        // Vector3 initialVelocity = gun.GetShotDirection() * gun.speed; 

        lr.SetPosition(0, startPosition); // Set the first point to the gun tip

        float timeStep = _predictionTime / (_segmentCount - 1); // Calculate time between segments

        for (int i = 1; i < _segmentCount; i++)
        {
            float timeOffset = i * timeStep; // Total time elapsed for this segment

            // Calculate position using kinematic equation: P = P0 + V0*t + 0.5*g*t^2
            // Note: Physics2D.gravity is a Vector2, ensure consistency.
            // gun.gravity is assumed to be a gravityScale, typically positive if applied to magnitude of Physics2D.gravity
            Vector3 segmentPosition = startPosition + initialVelocity * timeOffset +
                                      (Vector3)(Physics2D.gravity * gun.gravity * 0.5f * Mathf.Pow(timeOffset, 2));

            lr.SetPosition(i, segmentPosition);
        }
    }
}