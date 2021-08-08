using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : FighterController {
  private enum State {
    Moving,
    Jumping,
    Attacking,
  }

  private State _currentState;

  private Vector2 _vectorToPlayer;

  private bool _isAttackingFromGround;

  private int _numGroundAttacks;

  private bool _allowedToAttack;

  protected override void OnStart() {
    _currentState = State.Moving;
    _vectorToPlayer = new Vector2();
    _isAttackingFromGround = false;
    _numGroundAttacks = 0;
    _allowedToAttack = true;
  }

  protected override void OnUpdate() {
    GameObject player = GameObject.Find("Player");
    Vector3 vectorToPlayer3D = (player.transform.position - gameObject.transform.position);
    _vectorToPlayer = new Vector2(vectorToPlayer3D.x, vectorToPlayer3D.z);
    
    if (_allowedToAttack && !_isAttackingFromGround && _vectorToPlayer.magnitude < 10) {
      _isAttackingFromGround = true;
      _numGroundAttacks += 1;
      if (_numGroundAttacks <= 3) {
        StartCoroutine(AttackWithDelay());
      } else {
        _numGroundAttacks = 0;
        _allowedToAttack = false;
        StartCoroutine(PauseAttacking());
      }
    } else if (!_isAttackingFromGround && _vectorToPlayer.magnitude < 20 && _currentState != State.Jumping && _currentState != State.Attacking) {
      _currentState = State.Jumping;
      Jump();
      Attack();
    } else {
      _currentState = State.Moving;
      Move(_vectorToPlayer.normalized);
    }
  }

  IEnumerator AttackWithDelay() {
    Attack();
    yield return new WaitForSeconds(0.3f);
    _isAttackingFromGround = false;
  }

  IEnumerator PauseAttacking() {
    yield return new WaitForSeconds(2f);
    _allowedToAttack = true;
    _isAttackingFromGround = false;
  }
}
