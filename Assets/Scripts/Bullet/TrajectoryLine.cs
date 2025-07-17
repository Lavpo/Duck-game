using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Gun gun;
    [SerializeField] private Transform gunTip;

    [Header("Trajectory Line smoothnewss/Length")]
    [SerializeField] private int _segmentCount = 50;
    [SerializeField] private float _curveLength = 3.5f;

    private Vector2[] _segments;
    private LineRenderer lr;

    private GrenadeBullet gb;

    private float _projectileSpeed;
    private float _projectileGravityFromRB;

    void Start()
    {
        //initialize segments
        _segments = new Vector2[_segmentCount];

        //Take line renderer and set it's amount of point
        lr = GetComponent<LineRenderer>();
        lr.positionCount = _segmentCount;

        //Will help to grab bullet's speed
        gb = gun.GetComponent<GrenadeBullet>();
        _projectileSpeed = gun.speed;
        _projectileGravityFromRB = gb.Gravity;
    }

    private void Update()
    {
        //sets spawn position for a line
        Vector2 startPos = gunTip.position;
        _segments[0] = startPos;
        lr.SetPosition(0, startPos);

    }
}
