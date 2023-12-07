using System.Numerics;
using UnityEngine;

public class LeverMoving : MonoBehaviour, IMovable  //이동관련기능을 알리기 위한 인터페이스 상속
{
    private bool _isMove;
    private Vector3 MovePos;

    private void OnEnable()
    {
        StartSetting();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePos, 0.25f);
        }
        if (transform.position == MovePos)
        {
            _isMove = false;
        }
    }

    private void StartSetting()
    {
        MovePos = transform.position + new Vector3(-6, 0, 10);
        _isMove = true;
    }
}
