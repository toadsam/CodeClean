using System.Collections;
using System.Numerics;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Vector3 _startRotation;
    private Collider _collider;

    [Header("임시입니다")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _respwanPos;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = new Vector3(0, 0, 0);
        StartCoroutine(MoveStart());
    }

    public IEnumerator MoveStart() 
    {
        var deleyTime = new WaitForSeconds(2.5f); //무한 코루틴이기 때문에 캐싱을하여 가비지 최소
        while (true)
        {
            int fallingsecond = Random.Range(1, 8);
            yield return new WaitForSeconds(fallingsecond);
            _rigidbody.isKinematic = false;
            yield return deleyTime;
            _rigidbody.isKinematic = true;
            _collider.isTrigger = true;
            transform.SetPositionAndRotation(_startPosition, Quaternion.Euler(_startRotation));
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int faintingtime = Random.Range(4, 8);
            SwichPos();
        }
        if (other.CompareTag("Ice"))
        {
            _collider.isTrigger = false;
        }
    }

    

    private void SwichPos()  //임시입니다.
    {
        _player.transform.position = _respwanPos.transform.position;
    }

    
} 
