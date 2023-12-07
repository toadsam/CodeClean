using System.Collections;
using System.Numerics;
using UnityEngine;

public enum Cube  //타입을 나눠서 하나의 스크립트로 큐브를 관리
{
    Magma,
    Luva,
    Ice,
    Water,
    IceWall
}
public class CubeType : MonoBehaviour
{
    [SerializeField] private Cube _cube;

    [Header("IceCube")]
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Vector3 _startRotation;
    [SerializeField] private bool _isMelt;
    [SerializeField] private bool _isRightFalling;


    [Header("Magma")]
    private int _magmaJumpPower;

    [Header("임시입니다")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _respwanPos;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = new Vector3(transform.localRotation.x, 16.976f, transform.localRotation.z);
        StartCoroutine(MagmaCubeMove());
    }
    private IEnumerator IceMoveStart() 
    {
        float fallingsecond = Random.Range(1, 2);
        if (_isRightFalling)
        {
            fallingsecond = 0.3f;
        }
        yield return new WaitForSeconds(fallingsecond);
        _rigidbody.isKinematic = false;
        yield return new WaitForSeconds(3f);
        if (_isMelt)
        {
            int melt = Random.Range(1, 6);
            if (melt < 4)
            {
                gameObject.SetActive(false);
            }
        }
        _rigidbody.isKinematic = true;
        transform.position = _startPosition;
        transform.localRotation = Quaternion.Euler(_startRotation);

    }
    private IEnumerator IceWallMove() 
    {


        yield return new WaitForSeconds(6f);
        _rigidbody.isKinematic = true;
        transform.position = _startPosition;
        transform.localRotation = Quaternion.Euler(_startRotation);
        yield return new WaitForSeconds(2f);
        _rigidbody.isKinematic = false;
    }

    private IEnumerator MagmaCubeMove() 
    {
        _magmaJumpPower = 10;
        yield return new WaitForSeconds(1.49f);
        var deleyTime = new WaitForSeconds(3.146f);
        //무한 코루틴이기 때문에 가비지를 줄이기 위해서 변수를 초기화 시켜서 사용
        while (true)
        {
            _magmaJumpPower = 3;
            yield return deleyTime;
            _magmaJumpPower = 10;
            yield return deleyTime;
        }
    }
    private void SwichPos()  
    {
        _player.transform.position = _respwanPos.transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ForceReceiver forceReceiver = collision.gameObject.GetComponent<ForceReceiver>();
            switch (_cube)
            {
                case Cube.Magma:
                    forceReceiver.StartGimmick(Gimmicks.AddVelocity, collision.gameObject.GetComponent<Rigidbody>(), 0, _magmaJumpPower, 0, 30f);
                    break;
                case Cube.Luva:
                    SwichPos();
                    break;
                case Cube.Ice:
                    StartCoroutine(IceMoveStart());
                    break;
                case Cube.Water:
                    SwichPos();
                    break;
                case Cube.IceWall:
                    StartCoroutine(IceWallMove());
                    break;
                default:
                    break;
            }
        }
    }
}
