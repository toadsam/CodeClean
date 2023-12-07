using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Numerics;

public class Stage2Manager : MonoBehaviour
{
    [Header("Stage")]
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;

    [Header("CurStage")]
    public bool isStage1;
    public bool isStage2;
    public bool isStage3;

    public bool isStage2Clear;

    [Header("RespwamPosition")]
    public GameObject stage1RespawnPosition;
    public GameObject stage2RespawnPosition;
    public GameObject stage3RespawnPosition;
    public Vector3 curRespawnPosition;

    public float stage2CameraY;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera _stage1Camera;
    [SerializeField] private CinemachineVirtualCamera _stage2Camera;
    [SerializeField] private CinemachineVirtualCamera _stage3Camera;


    [Header("StageCharactor")]
    [SerializeField] private GameObject BasePlayer;
    [SerializeField] private GameObject Stage3Player;

    public static Stage2Manager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        Stage1Start();
    }

    public void Stage1Start()
    {
        ;
        _stage1.SetActive(true);
        curRespawnPosition = stage1RespawnPosition.transform.position;
        _stage1Camera.enabled = true;
        _stage2Camera.enabled = false;
        _stage3Camera.enabled = false;
        isStage1 = true;
        isStage2 = false;
        isStage3 = false;
    }
    public void Stage2Start()
    {
        _stage2.SetActive(true);
        curRespawnPosition = stage2RespawnPosition.transform.position;
        _stage1Camera.enabled = false;
        _stage2Camera.enabled = true;
        _stage3Camera.enabled = false;
        isStage1 = false;
        isStage2 = true;
        isStage3 = false;
    }
    public void Stage3Start()
    {
        _stage3.SetActive(true);
        curRespawnPosition = stage3RespawnPosition.transform.position;
        _stage1Camera.enabled = false;
        _stage2Camera.enabled = false;
        _stage3Camera.enabled = true;
        isStage1 = false;
        isStage2 = false;
        isStage3 = true;
        BasePlayer.SetActive(false);
        Stage3Player.SetActive(true);
    }

    public void ClearStage2()
    {
        GameManager.instance.isSecondStageClear = true;
        GameManager.instance.isSpawn = true;
        SceneManager.LoadScene("StageSelect");
    }
}
