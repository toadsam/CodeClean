using System.Numerics;
using UnityEngine;

public class RotationCube : MonoBehaviour, IRotatable  //회전관련 기믹인 것을 알리기 위해 인터페이스 상속
{
    void Update()
    {
        Rotation();
    }

    public void Rotation()
    {
        transform.Rotate(Vector3.right * (Time.deltaTime * 50));
    }
}
