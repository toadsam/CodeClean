using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Lever")]
    private GameObject _Lever;
    private bool _isMove;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        StartSetting();
    }

    private void Update()
    {
        LeverMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveStart());
        }
    }

    private IEnumerator MoveStart()
    {
        _isMove = true;
        yield return new WaitForSeconds(2f);
        _isMove = false;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f); // + 조건
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f); // + 조건
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f); // + 조건
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        transform.GetChild(5).gameObject.SetActive(true);
        Stage2Manager.instance.Stage2Start();

    }
    private void StartSetting()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);

        _Lever = transform.GetChild(0).gameObject;
    }

    private void LeverMove()
    {
        if (_isMove)
            _animator.SetBool("LeverUp", true);
    }
}
