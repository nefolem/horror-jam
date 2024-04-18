using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(StartMenu.Instance.IsGameStarted)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float inputX = Input.GetAxis("Mouse X") * _mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
        }
    }
}
