using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;
    
    private bool _forwardPress;
    private bool _backwardPress;
    private bool _rightPress;
    private bool _leftPress;
    private bool _ctrlPress;
    private bool _shiftPress;
    
    private bool _isWalking;
    private bool _isRunning;
    private bool _isCrouching;
    private bool _isCrouched;

    private bool _animState;
    
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isCrouchingHash;
    private int _isCrouchedHash;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isCrouchingHash = Animator.StringToHash("isCrouching");
        _isCrouchedHash = Animator.StringToHash("isCrouched");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputKey();
        GetAnimationState();
        SwitchWalkAnimation();
        SwitchRunAnimation();
        SwitchCrouchAnimation();
    }

    private void GetAnimationState()
    {
        _isWalking = _animator.GetBool(_isWalkingHash);
        _isRunning = _animator.GetBool(_isRunningHash);
        _isCrouching = _animator.GetBool(_isCrouchingHash);
        _isCrouched = _animator.GetBool(_isCrouchedHash);
    }
    
    private void GetInputKey()
    {
        _forwardPress = Input.GetKey(KeyCode.W);
        _backwardPress = Input.GetKey(KeyCode.S);
        _rightPress = Input.GetKey(KeyCode.D);
        _leftPress = Input.GetKey(KeyCode.A);
        _ctrlPress = Input.GetKeyDown(KeyCode.LeftControl);
        _shiftPress = Input.GetKey(KeyCode.LeftShift);
    }

    private void SwitchWalkAnimation()
    {
        if (!_isCrouched && !_isWalking && _forwardPress)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if (_isWalking && !_forwardPress)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
    }

    private void SwitchRunAnimation()
    {
        if (!_isCrouched && !_isRunning && _forwardPress && _shiftPress)
        {
            _animator.SetBool(_isRunningHash, true);
        }
        else if (_isRunning && (!_shiftPress || !_forwardPress))
        {
            _animator.SetBool(_isRunningHash, false);
        }
    }

    private void SwitchCrouchAnimation()
    {
        if (!_isCrouched && _ctrlPress)
        {
            _animator.SetBool(_isCrouchedHash, true);
        }
        else if ((_isCrouched || _isCrouching) && _ctrlPress)
        {
            _animator.SetBool(_isCrouchedHash, false);
        }
        
        if (_isCrouched && !_isCrouching && _forwardPress)
        {
            _animator.SetBool(_isCrouchingHash, true);
        }
        else if ((_isCrouched || _isCrouching) && (!_forwardPress || _ctrlPress))
        {
            _animator.SetBool(_isCrouchingHash, false);
        }
    }

    private void SwitchAnimation()
    {
        
    }
}
