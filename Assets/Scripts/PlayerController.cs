using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : FighterController {
  public void OnMove(InputAction.CallbackContext context) {
    Move(context.ReadValue<Vector2>());
  }

  public void OnJump(InputAction.CallbackContext context) {
    if (context.started) {
      Jump();
    }
  }

  public void OnAttack(InputAction.CallbackContext context) {
    if (context.started) {
      Attack();
    }
  }
}
