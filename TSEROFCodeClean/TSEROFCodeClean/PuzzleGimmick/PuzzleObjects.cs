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
