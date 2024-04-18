using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Transform _startCameraPoint;
    [SerializeField] private Transform _finishCameraPoint;
    [SerializeField] private GameObject _vCam;

    private DOTween _tween;
    
    public bool IsGameStarted { get; private set; }
    public static StartMenu Instance { get; private set; }

    private void Awake()
    {
        // if (Instance != null || Instance != this)
        // {
        //     //Destroy(this);
        // }
        // else
        // {
            Instance = this;
        //}
        IsGameStarted = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = _startCameraPoint.position;
        Camera.main.transform.rotation = _startCameraPoint.rotation;
    }

    // Update is called once per frame
    public void StartGame()
    {
        IsGameStarted = true;
        Camera.main.transform.DOMove(_finishCameraPoint.position, 7f).OnComplete(() => _vCam.SetActive(true));
        Camera.main.transform.DORotate(_finishCameraPoint.eulerAngles, 7f, RotateMode.Fast);
    }
}
