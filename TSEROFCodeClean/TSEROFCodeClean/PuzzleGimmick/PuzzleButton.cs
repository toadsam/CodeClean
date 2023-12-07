
using System.Numerics;
using UnityEngine;

public enum ButtonType  //버튼기믹종류들 
{
    reset,
    widthTop,
    widthMiddle,
    widthBottom,
    lengthRight,
    lengthMiddle,
    lengthLeft
}
public class PuzzleButton : MonoBehaviour 
{
    [SerializeField] private ButtonType _buttonType;

    private int[] _reset =  {0,0,0,
                             0,0,0,
                             0,0,0};

    private bool _isMove;
    private int _movecount;
    private Vector3 _moveTransform;
    private Vector3 _startposition;

    private void OnCollisionEnter(Collision collision)
    {
        int[] curPattern = PuzzleManager.instance.puzzleObjects.QuestionPattern();
        if (collision.gameObject.CompareTag("Player"))
        {
            _isMove = true;
            switch (_buttonType)
            {
                case ButtonType.reset:
                    PuzzleManager.instance.puzzleObjects.SettingObject(_reset);
                    break;
                case ButtonType.widthTop:
                    ThreeChangePuzzle(curPattern, 0, 1, 2);
                    PuzzleManager.instance.puzzleObjects.SettingObject(curPattern);
                    break;
                case ButtonType.widthMiddle:
                    ThreeChangePuzzle(curPattern, 3, 4, 5);
                    PuzzleManager.instance.puzzleObjects.SettingObject(curPattern);
                    break;
                case ButtonType.widthBottom:
                    ThreeChangePuzzle(curPattern, 6, 7, 8);
                    PuzzleManager.instance.puzzleObjects.SettingObject(curPattern);
                    break;
                case ButtonType.lengthRight:
                    ThreeChangePuzzle(curPattern, 0, 3, 6);
                    PuzzleManager.instance.puzzleObjects.SettingObject(curPattern);
                    break;
                case ButtonType.lengthMiddle:
                    ThreeChangePuzzle(curPattern, 1, 4, 7);
                    PuzzleManager.instance.puzzleObjects.SettingObject(curPattern);
                    break;
                case ButtonType.lengthLeft:
                    ThreeChangePuzzle(curPattern, 2, 5, 8);
                    PuzzleManager.instance.puzzleObjects.SettingObject(curPattern);
                    break;

            }
        }
    }
    private void Start()
    {
        _moveTransform = transform.position + new Vector3(0, 1f, 0);
        _startposition = transform.position;
    }


    private void ThreeChangePuzzle(int[] cur, int one, int two, int three)  //그냥 편하게 일반 메서드로 묶었다
    {
        cur[one] += 1;
        cur[two] += 1;
        cur[three] += 1;
        if (cur[one] == 3)
            cur[one] = 0;
        if (cur[two] == 3)
            cur[two] = 0;
        if (cur[three] == 3)
            cur[three] = 0;

    }


}
