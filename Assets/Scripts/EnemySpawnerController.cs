using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour {
  public GameObject enemyPrefab;

  private bool _canSpawnEnemy;
  void Start() {
    _canSpawnEnemy = true;
  }

  void Update() {
    if (_canSpawnEnemy) {
      StartCoroutine(SpawnEnemy());
    }
  }

  IEnumerator SpawnEnemy() {
    _canSpawnEnemy = false;
    Instantiate(enemyPrefab, new Vector3(Random.Range(-75f, 75f), 50f, Random.Range(-75, 75)), Quaternion.Euler(30, 0, 0));
    yield return new WaitForSeconds(10f);
    _canSpawnEnemy = true;
  }
}
