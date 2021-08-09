using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyController : MonoBehaviour {
  void Start() {

  }

  void Update() {
    if (gameObject.transform.parent != null) {
      gameObject.transform.position = gameObject.transform.parent.transform.position + new Vector3(0, 15, 10);
      gameObject.transform.rotation = gameObject.transform.parent.transform.rotation;
      GetComponent<Rigidbody>().useGravity = false;
      GetComponent<Rigidbody>().freezeRotation = true;
    } else {
      GetComponent<Rigidbody>().useGravity = true;
      GetComponent<Rigidbody>().freezeRotation = false;
    }
  }

  void FixedUpdate() {
    // if (gameObject.transform.parent != null) {
    //   gameObject.transform.position = gameObject.transform.parent.transform.position + new Vector3(0, 20, -5);
    // }
  }
}
