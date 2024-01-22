using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawn : MonoBehaviour
{
  public List<GameObject> possibleSpawns;
  public float minTime;
  public float maxTime;
  private float currentTime;

  public void Update() {
    if (currentTime > 0) {
      currentTime --;
    }
    else {
      int i = Random.Range(0,possibleSpawns.Count+1);
      GameObject bossEnemy = Instantiate(possibleSpawns[i], new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
      currentTime = Random.Range(minTime, maxTime);
    }
  }
}
