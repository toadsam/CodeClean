
using System.Numerics;
using UnityEngine;

public class PatternTime : MonoBehaviour
{
    [Header("Time")]
    private float _maxTime = 25f;
    private float _timeLeft;
    private void Start()
    {
        _timeLeft = _maxTime;

    }

    // Update is called once per frame
    private void Update()
    {

        TimeReduction();
    }

    private void TimeReduction()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            transform.localScale = new Vector3(4 * (_timeLeft / _maxTime), 0.1f, 0.1f);
        }
        else
        {
            _timeLeft = 25f;
        }
    }
}
