using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
  private float MAX_COMBO_GRACE_PERIOD = 0.5f;

  private Rigidbody _rigidbody;

  private Animator _animator;

  [SerializeField] private float _moveSpeed;

  [SerializeField] private float _jumpForce;

  private Vector2 _previousMove;

  private Vector2 _currentMove;

  private bool _wasMovingLeft {
    get { return _previousMove.x < 0; }
  }

  private bool _isMovingLeft {
    get { return _currentMove.x < 0; }
  }

  private bool _wasMovingRight {
    get { return _previousMove.x > 0; }
  }

  private bool _isMovingRight {
    get { return _currentMove.x > 0; }
  }

  private bool _isAscending {
    get { return _rigidbody.velocity.y > 0; }
  }

  private bool _isDescending {
    get {return _rigidbody.velocity.y < 0; }
  }

  private bool _isJumping;

  private bool _hasLanded;

  private bool _isAttacking;

  private int _comboNumber;

  private DateTime _lastAttackTimestamp;

  void Start() {
    _rigidbody = GetComponent<Rigidbody>();
    _animator = GetComponent<Animator>();
    _comboNumber = 1;
    _lastAttackTimestamp = System.DateTime.Now;
  }

  void Update() {
    if (AnimatingLanding()) {
      return;
    }

    if (!(_isAttacking && !_isJumping)) {
      MovePlayer();
    }

    AnimatePlayer();
  }

  private bool AnimatingLanding() {
    if (_hasLanded) {
      if (_isMovingLeft) {
        _animator.Play("Jump Land Left");
      } else if (_isMovingRight) {
        _animator.Play("Jump Land Right");
      } else if (_wasMovingLeft) {
        _animator.Play("Jump Land Left");
      } else if (_wasMovingRight) {
        _animator.Play("Jump Land Right");
      }
    }

    return _hasLanded;
  }

  private void MovePlayer() {
    Vector3 moveVelocity = _moveSpeed * (
      _currentMove.x * Vector3.right +
      _currentMove.y * Vector3.forward
    );

    Vector3 moveThisFrame = Time.deltaTime * moveVelocity;
    transform.position += moveThisFrame;
  }

  private void AnimatePlayer() {
    if (_isAttacking) {
      if (_isJumping) {
        if (_isMovingLeft) {
          _animator.Play("Jump Kick Run Left");
        } else if (_isMovingRight) {
          _animator.Play("Jump Kick Run Right");
        } else if (_wasMovingLeft) {
          _animator.Play("Jump Kick Stationary Left");
        } else {
          _animator.Play("Jump Kick Stationary Right");
        }
      } else if (_isMovingLeft) {
        _animator.Play(string.Format("Punch {0} Left", _comboNumber));
      } else if (_isMovingRight) {
        _animator.Play(string.Format("Punch {0} Right", _comboNumber));
      } else if (_wasMovingLeft) {
        _animator.Play(string.Format("Punch {0} Left", _comboNumber));
      } else {
        _animator.Play(string.Format("Punch {0} Right", _comboNumber));
      }
    } else if (_isJumping) {
      if (_isAscending) {
        if (_isMovingLeft) {
          _animator.Play("Jump Up Left");
        } else if (_isMovingRight) {
          _animator.Play("Jump Up Right");
        } else if (_wasMovingLeft) {
          _animator.Play("Jump Up Left");
        } else {
          _animator.Play("Jump Up Right");
        }
      } else if (_isDescending) {
        if (_isMovingLeft) {
          _animator.Play("Jump Down Left");
        } else if (_isMovingRight) {
          _animator.Play("Jump Down Right");
        } else if (_wasMovingLeft) {
          _animator.Play("Jump Down Left");
        } else {
          _animator.Play("Jump Down Right");
        }
      }
    } else if (_isMovingLeft) {
      _animator.Play("Move Left");
    } else if (_isMovingRight) {
      _animator.Play("Move Right");
    } else if (_wasMovingLeft) {
      _animator.Play("Idle Left");
    } else {
      _animator.Play("Idle Right");
    }
  }

  public void OnMove(InputAction.CallbackContext context) {
    if (CanMove()) {
      _previousMove = _currentMove;
      _currentMove = context.ReadValue<Vector2>();
    }
  }

  private bool CanMove() {
    return !_isAttacking && !_isJumping && !_hasLanded;
  }

  public void OnJump(InputAction.CallbackContext context) {
    if (!_isJumping && !_hasLanded && context.started) {
      if (IsOnLastComboAttack() || IsInFirstHalfOfAttackAnimation()) {
        return;
      }

      _isAttacking = false;
      _isJumping = true;
      _rigidbody.AddForce(Vector3.up * _jumpForce);
    }
  }

  public void OnAttack(InputAction.CallbackContext context) {
    if (!_isJumping) {
      _currentMove = new Vector2();
    }

    if (CanAttack() && context.started) {
      if (IsOnLastComboAttack() || IsInFirstHalfOfAttackAnimation()) {
        return;
      }

      _isAttacking = true;
      if ((System.DateTime.Now - _lastAttackTimestamp).TotalSeconds < MAX_COMBO_GRACE_PERIOD) {
        _comboNumber += 1;
        if (_comboNumber > 3) {
          _comboNumber = 1;
        }
      } else {
        _comboNumber = 1;
      }

      _lastAttackTimestamp = System.DateTime.Now;
    }
  }

  private bool CanAttack() {
    return !_hasLanded;
  }

  public void OnAttackEnd() {
    _isAttacking = false;
  }

  private bool IsInFirstHalfOfAttackAnimation() {
    return _isAttacking && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f;
  }

  private bool IsOnLastComboAttack() {
    return _isAttacking && _comboNumber == 3;
  }

  public void OnCollisionEnter(Collision collision) {
    _currentMove = new Vector2();
    _isJumping = false;
    _isAttacking = false;
    StartCoroutine(LandJump());
  }

  private IEnumerator LandJump() {
    _hasLanded = true;
    yield return new WaitForSeconds(0.1f);
    _hasLanded = false;
  }
}
