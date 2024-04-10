using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;
    private bool _forwardPress;
    private int _isWalkingHash;
    private bool _isWalking;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _forwardPress = Input.GetKey(KeyCode.W);
        _isWalking = _animator.GetBool(_isWalkingHash);
        if (!_isWalking && _forwardPress)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if (_isWalking && !_forwardPress)
        {
            _animator.SetBool(_isWalkingHash, false);
            
        }
    }
}
