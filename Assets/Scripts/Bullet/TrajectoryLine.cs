using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bullet;

    [SerializeField] private int _segmentCount = 50;

    private Vector2[] _segments;
    private LineRenderer lr;

    private Bulletscript bs;

    private float _projectileSpeed;
    private float _projectileGravityFromRB;

    void Start()
    {
        _segments = new Vector2[_segmentCount];

        lr = GetComponent<LineRenderer>();
        lr.positionCount = _segmentCount;
    }
}
