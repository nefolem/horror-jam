using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;

    #region keys press

    private bool _forwardPress;
    private bool _backwardPress;
    private bool _rightPress;
    private bool _leftPress;
    private bool _ctrlPress;
    private bool _shiftPress;

    #endregion

    #region animation states

    private bool _isWalkingForward;
    private bool _isWalkingBackward;
    private bool _isWalkingRight;
    private bool _isWalkingLeft;
    
    private bool _isRunningForward;
    private bool _isRunningBackward;
    private bool _isRunningRight;
    private bool _isRunningLeft;
    
    private bool _isCrouchingForward;
    private bool _isCrouchingBackward;
    private bool _isCrouchingRight;
    private bool _isCrouchingLeft;
    
    private bool _isCrouched;
    
    private bool _isAFK;

    #endregion

    #region animation hash

    private int _isWalkingForwardHash;
    private int _isWalkingBackwardHash;
    private int _isWalkingRightHash;
    private int _isWalkingLeftHash;
    
    private int _isRunningForwardHash;
    private int _isRunningBackwardHash;
    private int _isRunningRightHash;
    private int _isRunningLeftHash;
    
    private int _isCrouchingForwardHash;
    private int _isCrouchingBackwardHash;
    private int _isCrouchingRightHash;
    private int _isCrouchingLeftHash;
    
    private int _isCrouchedHash;
    
    private int _isAFKHash;

    private int _isGameStartedHash;

    #endregion

    private bool _isGameStarted;
    private float _timer = 0.0f;
    [SerializeField] private float _timeToAFK = 10f;
    
    private void Awake()
    {
        _isGameStarted = false; 
        
        _animator = GetComponent<Animator>();
        
        _isWalkingForwardHash = Animator.StringToHash("isWalkingForward");
        _isWalkingBackwardHash = Animator.StringToHash("isWalkingBackward");
        _isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        _isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        
        _isRunningForwardHash = Animator.StringToHash("isRunningForward");
        _isRunningBackwardHash = Animator.StringToHash("isRunningBackward");
        _isRunningRightHash = Animator.StringToHash("isRunningRight");
        _isRunningLeftHash = Animator.StringToHash("isRunningLeft");
        
        _isCrouchingForwardHash = Animator.StringToHash("isCrouchingForward");
        _isCrouchingBackwardHash = Animator.StringToHash("isCrouchingBackward");
        _isCrouchingRightHash = Animator.StringToHash("isCrouchingRight");
        _isCrouchingLeftHash = Animator.StringToHash("isCrouchingLeft");
        
        _isCrouchedHash = Animator.StringToHash("isCrouched");
        
        _isAFKHash = Animator.StringToHash("isAFK");
        _isGameStartedHash = Animator.StringToHash("isGameStarted");

    }

    private void Start()
    {
        _animator.Play("Sitting");
        _animator.speed = 0f;
    }

    void Update()
    {
        if (!_isGameStarted && StartMenu.Instance.IsStartPressed)
        {
            StartCoroutine(nameof(StartGameAnimation));
        }

        if(_isGameStarted)
        {
            GetInputKey();
            GetAnimationState();
            SwitchWalkAnimation();
            SwitchRunAnimation();
            SwitchCrouchAnimation();
            SwitchAFKAnimation();
        }
    }

    IEnumerator StartGameAnimation()
    {
        _animator.speed = 1f;
        _animator.SetBool(_isGameStartedHash, true);
        
        yield return new WaitForSeconds(5f);
        _isGameStarted = true;
    }

    private void GetAnimationState()
    {
        _isWalkingForward = _animator.GetBool(_isWalkingForwardHash);
        _isWalkingBackward = _animator.GetBool(_isWalkingBackwardHash);
        _isWalkingRight = _animator.GetBool(_isWalkingRightHash);
        _isWalkingLeft = _animator.GetBool(_isWalkingLeftHash);
        
        _isRunningForward = _animator.GetBool(_isRunningForwardHash);
        _isRunningBackward = _animator.GetBool(_isRunningBackwardHash);
        _isRunningRight = _animator.GetBool(_isRunningRightHash);
        _isRunningLeft = _animator.GetBool(_isRunningLeftHash);
        
        _isCrouchingForward = _animator.GetBool(_isCrouchingForwardHash);
        _isCrouchingBackward = _animator.GetBool(_isCrouchingBackwardHash);
        _isCrouchingRight = _animator.GetBool(_isCrouchingRightHash);
        _isCrouchingLeft = _animator.GetBool(_isCrouchingLeftHash);
        
        _isCrouched = _animator.GetBool(_isCrouchedHash);
        //_isAFK = _animator.GetBool(_isAFKHash);
    }
    
    private void GetInputKey()
    {
        _isAFK = !Input.anyKey;
        _forwardPress = Input.GetKey(KeyCode.W);
        _backwardPress = Input.GetKey(KeyCode.S);
        _rightPress = Input.GetKey(KeyCode.D);
        _leftPress = Input.GetKey(KeyCode.A);
        _ctrlPress = Input.GetKeyDown(KeyCode.LeftControl);
        _shiftPress = Input.GetKey(KeyCode.LeftShift);
    }

    private void SwitchWalkAnimation()
    {
        if (!_isCrouched && !_isWalkingForward && _forwardPress)
        {
            _animator.SetBool(_isWalkingForwardHash, true);
        }
        else if (_isCrouched || (_isWalkingForward && !_forwardPress))
        {
            _animator.SetBool(_isWalkingForwardHash, false);
        }

        if (!_isCrouched && !_isWalkingBackward && _backwardPress)
        {
            _animator.SetBool(_isWalkingBackwardHash, true);
        }
        else if (_isCrouched || (_isWalkingBackward && !_backwardPress))
        {
            _animator.SetBool(_isWalkingBackwardHash, false);
        }
        
        if (!_isCrouched && !_isWalkingRight && _rightPress)
        {
            _animator.SetBool(_isWalkingRightHash, true);
        }
        else if (_isCrouched || (_isWalkingRight && !_rightPress))
        {
            _animator.SetBool(_isWalkingRightHash, false);
        }
        
        if (!_isCrouched && !_isWalkingLeft && _leftPress)
        {
            _animator.SetBool(_isWalkingLeftHash, true);
        }
        else if (_isCrouched || (_isWalkingLeft && !_leftPress))
        {
            _animator.SetBool(_isWalkingLeftHash, false);
        }
    }

    private void SwitchRunAnimation()
    {
        if (!_isCrouched && !_isRunningForward && _forwardPress && _shiftPress)
        {
            _animator.SetBool(_isRunningForwardHash, true);
        }
        else if (_isCrouched || _isRunningForward && (!_shiftPress || !_forwardPress))
        {
            _animator.SetBool(_isRunningForwardHash, false);
        }
        
        if (!_isCrouched && !_isRunningBackward && _backwardPress && _shiftPress)
        {
            _animator.SetBool(_isRunningBackwardHash, true);
        }
        else if (_isCrouched || _isRunningBackward && (!_shiftPress || !_backwardPress))
        {
            _animator.SetBool(_isRunningBackwardHash, false);
        }
        
        if (!_isCrouched && !_isRunningRight && _rightPress && _shiftPress)
        {
            _animator.SetBool(_isRunningRightHash, true);
        }
        else if (_isCrouched || _isRunningRight && (!_shiftPress || !_rightPress))
        {
            _animator.SetBool(_isRunningRightHash, false);
        }
        
        if (!_isCrouched && !_isRunningLeft && _leftPress && _shiftPress)
        {
            _animator.SetBool(_isRunningLeftHash, true);
        }
        else if (_isCrouched || _isRunningLeft && (!_shiftPress || !_leftPress))
        {
            _animator.SetBool(_isRunningLeftHash, false);
        }
    }

    private void SwitchCrouchAnimation()
    {
        if (!_isCrouched && _ctrlPress)
        {
            _animator.SetBool(_isCrouchedHash, true);
        }
        else if ((_isCrouched || _isCrouchingForward) && _ctrlPress)
        {
            _animator.SetBool(_isCrouchedHash, false);
        }
        
        
        if ((_isCrouched && !_isCrouchingForward && _forwardPress) || (!_isCrouchingForward && _ctrlPress))
        {
            _animator.SetBool(_isCrouchingForwardHash, true);
        }
        else if ((_isCrouched || _isCrouchingForward) && (!_forwardPress || _ctrlPress))
        {
            _animator.SetBool(_isCrouchingForwardHash, false);
        }
        
        if (_isCrouched && !_isCrouchingBackward && _backwardPress)
        {
            _animator.SetBool(_isCrouchingBackwardHash, true);
        }
        else if ((_isCrouched || _isCrouchingBackward) && (!_backwardPress || _ctrlPress))
        {
            _animator.SetBool(_isCrouchingBackwardHash, false);
        }
        
        if (_isCrouched && !_isCrouchingRight && _rightPress)
        {
            _animator.SetBool(_isCrouchingRightHash, true);
        }
        else if ((_isCrouched || _isCrouchingRight) && (!_rightPress || _ctrlPress))
        {
            _animator.SetBool(_isCrouchingRightHash, false);
        }
        
        if (_isCrouched && !_isCrouchingLeft && _leftPress)
        {
            _animator.SetBool(_isCrouchingLeftHash, true);
        }
        else if ((_isCrouched || _isCrouchingLeft) && (!_leftPress || _ctrlPress))
        {
            _animator.SetBool(_isCrouchingLeftHash, false);
        }
    }

    private void SwitchAFKAnimation()
    {
        if (_isAFK && _timer >= _timeToAFK)
        {
            _animator.SetTrigger(_isAFKHash);
            _timer = 0;
        }
        else if (_isAFK && _timer < _timeToAFK)
        {
            _timer += Time.deltaTime;
        }
    }
}
