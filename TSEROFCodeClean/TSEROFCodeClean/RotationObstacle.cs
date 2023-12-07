using System.Numerics;
using System;
using UnityEngine;

public class RotationObstacle : MonoBehaviour, IRotatable //회전 관련 기믹인 것을 알리기 위해서 인터페이스 상속
{
    public enum Type
    {
        Cogwheel,
        IronMace
    }

    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private float _speed;

    public Type obstacles;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    public int rotateSpeed;

    public GameObject RotationObject;
    
    private void Start()
    {
        TargetNextWaypoint();
    }

    void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);//한게에 스무딩이 추가된다
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }
    private void Update()
    {
        Rotation();
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    public void Rotation()
    {
        switch (obstacles)
        {
            case Type.Cogwheel:
                RotationObject.transform.Rotate(Vector3.right * (Time.deltaTime * rotateSpeed));
                break;
            case Type.IronMace:
                RotationObject.transform.Rotate(Vector3.up * (Time.deltaTime * rotateSpeed));
                break;
            default:
                break;

        }
    }
}
