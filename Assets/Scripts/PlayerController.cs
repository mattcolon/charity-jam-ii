using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MCUU.Events;

public class PlayerController : FighterController, IEventListener {
  public GameObject teddyPrefab;

  private bool _canPickUp;

  private bool _canDeposit;

  private bool _hasItem;

  private GameObject _teddy;

  protected override void OnStart() {
    MCUU.Events.EventHandler.GetSingleton().RegisterEventListener(this);
    _canPickUp = false;
    _canDeposit = false;
    _hasItem = false;
    _teddy = null;
  }

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

  public void OnInteract(InputAction.CallbackContext context) {
    if (context.started) {
      if (_canPickUp && !_hasItem) {
        _hasItem = true;
        Debug.Log("Picked up");
        _teddy = (GameObject) Instantiate(teddyPrefab, new Vector3(0, 0, 0), Quaternion.Euler(30, 0, 0), gameObject.transform);
      } else if (_canDeposit && _hasItem) {
        _hasItem = false;
        Debug.Log("Deposited");
        Destroy(_teddy);
      }
    }
  }

  public void OnEvent(string eventName, string eventValue) {
    if (eventName == "Near Canister") {
      _canPickUp = Boolean.Parse(eventValue);
    } else if (eventName == "Near Truck") {
      _canDeposit = Boolean.Parse(eventValue);
    }
  }

  protected override void OnHit() {
    if (_teddy != null) {
      _teddy.transform.parent = null;
      _teddy.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(10f, 30f), 10, UnityEngine.Random.Range(10f, 30f)), ForceMode.Impulse);
      _hasItem = false;
      _teddy = null;
    }
  }
}
