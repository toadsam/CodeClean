# CodeClean
프로젝트 중 나의 코드를 정리한 곳
# 최종 프로젝트 코드 정리

### PatternSign

```csharp
using System.Collections;
using UnityEngine;

public class PatternSign : MonoBehaviour
{
    private PuzzleObjects _puzzleObjects;

    [Header("patterns")]

    private int[] _pattern1 = {0, 2, 0,
                               2, 1, 2,
                               0, 2, 0 };
    private int[] _pattern2 ={ 2, 1, 2,
                               1, 0, 1,
                               2, 1, 2 };
    private int[] _pattern3 ={ 1, 2, 1,
                               0, 1, 0,
                               1, 2, 1 };
    private int[] _pattern4 ={ 0, 1, 2,
                               0, 1, 2,
                               1, 2, 0 };
    private int[] _pattern5 ={ 1, 1, 1,
                               1, 1, 1,
                               1, 1, 1 };
    private int[] _reset = {2,2,2,
                            2,2,2,
                            2,2,2};

    private int[][] _patterns = new int[5][];

    private int _random;

    private void Awake()
    {
        _puzzleObjects = GetComponent<PuzzleObjects>();
        StartSetting();
    }
    private void Start()
    {
        StartCoroutine(PatternChange());
    }
    public IEnumerator PatternChange()
    {
        var deleyTime = new WaitForSeconds(25f);  //무한 코루틴이기 때문에 가비지를 줄이기 위해서 변수를 초기화 시켜서 사용했습니다.
        while (true)
        {
            yield return deleyTime; // + 조건
            _random = Random.Range(0, 5);
            PuzzleManager.instance.settingPuzzleButton.RandomSetting();
             _puzzleObjects.SettingObject(_patterns[_random]); //이게 맞음
            //_puzzleObjects.SettingObject(_patterns[4]);
            PuzzleManager.instance.puzzleObjects.SettingObject(_reset);
            AnswerPattern();
        }
    }

    private void StartSetting()  //for문으로 한번에 넣고 싶은데 방법을 못 찾겠습니다. ㅠㅠ
    {
        _patterns[0] = _pattern1;
        _patterns[1] = _pattern2;
        _patterns[2] = _pattern3;
        _patterns[3] = _pattern4;
        _patterns[4] = _pattern5;
    }

    public int[] AnswerPattern()
    {
        return _patterns[_random]; //이게 맞음
        //return _patterns[4];
    }
}
```

### PuzzleButton

```csharp
using UnityEngine;

public enum ButtonType 
{
    reset,
    widthTop,
    widthMiddle,
    widthBottom,
    lengthRight,
    lengthMiddle,
    lengthLeft
}
public class PuzzleButton : MonoBehaviour  //PuzzleButton 
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

    private void Update()
    {
        if (_isMove)
        {
            Move();
        }

        Move();

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

    private void Move()
    {
        if (_movecount == 0)
        {
            
            this.transform.position = Vector3.MoveTowards(_startposition, _moveTransform, 0.01f);//0.15f_pos1Time
            if (transform.position == _moveTransform)
            {
                _movecount++;
            }
        }

        if (_movecount == 1)
        {
            transform.position = Vector3.MoveTowards(_startposition, _moveTransform, 0.01f);//0.15f_pos1Time
            if (transform.position == _moveTransform)
            {
                _movecount--;
                _isMove = false;
            }
        }
    }

    private void ChangeMove()
    {

    }

}
```

### PuzzleManager

```csharp
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [Header("Management target")]
    public static PuzzleManager instance = null;
    public PuzzleObjects puzzleObjects;
    public PatternSign patternSign;
    public SettingPuzzleButton settingPuzzleButton;
    public bool isMove;
    [SerializeField] private GameObject leaf; 

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        puzzleObjects = transform.GetChild(0).gameObject.GetComponent<PuzzleObjects>();
        settingPuzzleButton = transform.GetChild(1).gameObject.GetComponent<SettingPuzzleButton>();
        patternSign = transform.GetChild(2).gameObject.GetComponent<PatternSign>();
    }

    private void Update()
    {
        if (CorrectAnswer())
        {
            puzzleObjects.gameObject.SetActive(false);
            leaf.SetActive(true);
        }
    }

    public bool CorrectAnswer() //정답 패턴과 풀고있는 패턴이 맞는지 확인한 후 불 값을 출력한다.
    {
        bool isRight = puzzleObjects.QuestionPattern().SequenceEqual(patternSign.AnswerPattern());

        return isRight;
    }
}
```

### PuzzleObjectMove

```csharp
using System.Collections;
using UnityEngine;

public class PuzzleObjectMove : MonoBehaviour
{
    [Header("MoveBool")]   
    private bool _isMove;
    private int _isResolve;
    private int[] _movePattern = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    [SerializeField]private Rigidbody _rigidbody;

    [Header("Object Pos")]
    [SerializeField] private Transform _pos1;
    [SerializeField] private Transform _pos2;
    [SerializeField] private Transform _thornPos;

    [Header("Object Time")]
    [SerializeField] public float _pos1Time;
    [SerializeField] public float _pos2Time;

    [SerializeField]private ForceReceiver _forceReceiver;

    private int count;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //_forceReceiver = GetComponent<ForceReceiver>();
    }

    private void Update()
    {
        if (Stage2Manager.instance.isStage2Clear)
        {
            _forceReceiver.ignorePlayerStatus = true;
            _rigidbody.useGravity = false; //이친구만 해결하면 될 것 같다.
            _rigidbody.isKinematic = true;
            ClearMove();
        }
    }

    public IEnumerator MoveStart() //한번만 실행 시킬 수 있는 코루틴 만들어 보기
    {
       
        yield return new WaitForSeconds(2f);
       // _forceReceiver.ignorePlayerStatus = true;
        _isMove = true;
        yield return new WaitForSeconds(4f);
        Stage2Manager.instance.Stage3Start();
    }

    private void Rotate()
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
            ThornMove();
        }
    }
    public void ThornMove()
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
```

### PuzzleObjects

```csharp
using UnityEngine;

public enum GameObjectType
{
    No,
    Ice,
    Water,
    Nothing
}
public enum PuzzleType
{
    Answer,
    Question

}

public class PuzzleObjects : MonoBehaviour
{
    [SerializeField] private PuzzleType _puzzleType;

    [Header("puzzleObject")]
    private GameObject[] puzzleObjects = new GameObject[9];
    private GameObject[] _waterPuzzleObjects = new GameObject[9];
    private GameObject[] _icePuzzleObjects = new GameObject[9];

    private int[] _startQuestion = { 2, 2, 2,
                            2, 2, 2,
                            2, 2, 2 };
    int[] _curPattern = new int[9];

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        for (int i = 0; i < transform.childCount; i++)  
        {

            puzzleObjects[i] = transform.GetChild(i).gameObject;
            if (puzzleObjects[i].transform.GetChild(0) == null && puzzleObjects[i].transform.GetChild(1) == null)
                return;
            _icePuzzleObjects[i] = puzzleObjects[i].transform.GetChild(0).gameObject;
            _waterPuzzleObjects[i] = puzzleObjects[i].transform.GetChild(1).gameObject;

        }
        SettingObject(_startQuestion);
    }

    public void SettingObject(int[] pattern)
    {

        for (int i = 0; i < pattern.Length; i++)
        {
            switch (pattern[i])
            {
                case (int)GameObjectType.No:
                    _icePuzzleObjects[i].SetActive(false);
                    _waterPuzzleObjects[i].SetActive(false);
                    _curPattern[i] = (int)GameObjectType.No;
                    break;
                case (int)GameObjectType.Ice:
                    _icePuzzleObjects[i].SetActive(true);
                    _waterPuzzleObjects[i].SetActive(false);
                    _curPattern[i] = (int)GameObjectType.Ice;
                    break;
                case (int)GameObjectType.Water:
                    _icePuzzleObjects[i].SetActive(false);
                    _waterPuzzleObjects[i].SetActive(true);
                    _curPattern[i] = (int)GameObjectType.Water;
                    break;
                case (int)GameObjectType.Nothing:
                    break;
                default:
                    break;
            }
        }
        QuestionPattern();
    }

    public int[] QuestionPattern()
    {
        return _curPattern;
    }
}
```

### SettingPuzzleButton

```csharp
using System.Collections.Generic;
using UnityEngine;

public class SettingPuzzleButton : MonoBehaviour  //SettingPuzzleButton 
{
    private Transform[] _ButtonPositoin = new Transform[7];
    private GameObject[] _Button = new GameObject[7];
    private List<int> _RandomPosList = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        for (int i = 0; i < _ButtonPositoin.Length; i++)
        {
            if (transform.GetChild(i).gameObject == null && transform.GetChild(_ButtonPositoin.Length + i).gameObject == null)  //null값 방지
                return;

            _ButtonPositoin[i] = transform.GetChild(i).gameObject.transform;
            _Button[i] = transform.GetChild(_ButtonPositoin.Length + i).gameObject;
        }

        RandomSetting();
    }

    public void RandomSetting() 
    {
        List<int> _RandomPosList = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        for (int i = 0; i < _ButtonPositoin.Length; i++)
        {
          
            int rand = Random.Range(0, _RandomPosList.Count);
            _Button[i].transform.position = _ButtonPositoin[_RandomPosList[rand]].transform.position;
            _RandomPosList.RemoveAt(rand);
        }
    }

}
```
