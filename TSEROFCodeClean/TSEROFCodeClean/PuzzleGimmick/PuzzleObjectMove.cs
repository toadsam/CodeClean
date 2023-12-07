
using System.Collections;
using System.Numerics;
using UnityEngine;

public class PuzzleObjectMove : MonoBehaviour, IMovable, IRotatable //각 기믹의 기능을 알리기 위한 인터페이스 상속
{
    [Header("MoveBool")]
    private bool _isMove;
    private int _isResolve;
    private int[] _movePattern = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Object Pos")]
    [SerializeField] private Transform _pos1;
    [SerializeField] private Transform _pos2;
    [SerializeField] private Transform _thornPos;

    [Header("Object Time")]
    [SerializeField] public float _pos1Time;
    [SerializeField] public float _pos2Time;

    [SerializeField] private ForceReceiver _forceReceiver;

    private int count;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Stage2Manager.instance.isStage2Clear)
        {
            _forceReceiver.ignorePlayerStatus = true;
            _rigidbody.useGravity = false; 
            _rigidbody.isKinematic = true;
            ClearMove();
        }
    }

    public IEnumerator MoveStart() 
    {

        yield return new WaitForSeconds(2f);      
        _isMove = true;
        yield return new WaitForSeconds(4f);
        Stage2Manager.instance.Stage3Start();
    }

    public void Rotation()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 500, Space.World);
    }



    private void ClearMove()
    {
        if (_isResolve == 0)
        {
            StartCoroutine(MoveStart());
            _isResolve = 1;
        }

        if (_isMove)
        {
            Move();
        }
    }
    public void Move()
    {
        if (count == 0)
        {
            _thornPos.position = Vector3.MoveTowards(_thornPos.position, _pos1.position, 0.03f);//0.15f_pos1Time
            if (_thornPos.position == _pos1.position)
            {
                count++;
            }

        }
        if (count == 1)
        {
            _thornPos.position = Vector3.MoveTowards(_thornPos.position, _pos2.position, 0.12f);// 0.01f_pos2Time
            if (_thornPos.position == _pos2.position)
            {
                count--;
            }

        }
    }

}
