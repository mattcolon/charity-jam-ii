using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCUU.Events;

public class CollectDepositColliderController : MonoBehaviour {
  void Start() {

  }

  void Update() {

  }

  void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.name == "Canister") {
      EventHandler.GetSingleton().NotifyEventListeners("Near Canister", "true");
    } else if (collider.gameObject.name == "Truck") {
      EventHandler.GetSingleton().NotifyEventListeners("Near Truck", "true");
    }
  }

  void OnTriggerExit(Collider collider) {
    if (collider.gameObject.name == "Canister") {
      EventHandler.GetSingleton().NotifyEventListeners("Near Canister", "false");
    } else if (collider.gameObject.name == "Truck") {
      EventHandler.GetSingleton().NotifyEventListeners("Near Truck", "false");
    }
  }
}
