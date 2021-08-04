using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderController : MonoBehaviour {
  public void EnableLeftPunchCollider() {
    SetColliderEnablement("Left Punch Collider", true);
  }

  public void DisableLeftPunchCollider() {
    SetColliderEnablement("Left Punch Collider", false);
  }

  public void EnableRightPunchCollider() {
    SetColliderEnablement("Right Punch Collider", true);
  }

  public void DisableRightPunchCollider() {
    SetColliderEnablement("Right Punch Collider", false);
  }

  public void EnableLeftKickCollider() {
    SetColliderEnablement("Left Kick Collider", true);
  }

  public void DisableLeftKickCollider() {
    SetColliderEnablement("Left Kick Collider", false);
  }

  public void EnableRightKickCollider() {
    SetColliderEnablement("Right Kick Collider", true);
  }

  public void DisableRightKickCollider() {
    SetColliderEnablement("Right Kick Collider", false);
  }

  public void EnableLeftStandingJumpKickCollider() {
    SetColliderEnablement("Left Standing Jump Kick Collider", true);
  }

  public void DisableLeftStandingJumpKickCollider() {
    SetColliderEnablement("Left Standing Jump Kick Collider", false);
  }

  public void EnableRightStandingJumpKickCollider() {
    SetColliderEnablement("Right Standing Jump Kick Collider", true);
  }

  public void DisableRightStandingJumpKickCollider() {
    SetColliderEnablement("Right Standing Jump Kick Collider", false);
  }

  public void EnableLeftRunningJumpKickCollider() {
    SetColliderEnablement("Left Running Jump Kick Collider", true);
  }

  public void DisableLeftRunningJumpKickCollider() {
    SetColliderEnablement("Left Running Jump Kick Collider", false);
  }

  public void EnableRightRunningJumpKickCollider() {
    SetColliderEnablement("Right Running Jump Kick Collider", true);
  }

  public void DisableRightRunningJumpKickCollider() {
    SetColliderEnablement("Right Running Jump Kick Collider", false);
  }

  private void SetColliderEnablement(string name, bool enabled) {
    gameObject.transform.Find(name).GetComponent<Collider>().enabled = enabled;
  }
}
