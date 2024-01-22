using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class spawnManager : MonoBehaviour
{
  public Vector3 spawnPercentages;
  public Vector3 lateGameWeight;

  public Transform cam;

  public int lateGameWave;

  public float secondsInBetweenWaves;
  public float curSecInBetween;

  public float waveNum;
  public int spawnNum;
  public int curSpawnNum;
  public float waveThreashold;
  public float lateGameThreashold;
  public int baseSpawnNum;

  public GameObject g_enemy;
  public GameObject b_enemy;
  public GameObject p_enemy;

  private GameObject curSpawn;
  public List<GameObject> spawns = new List<GameObject>();
  public int dead;

  public List<GameObject> spawnPoints;
  public Vector4 spawnFavourability;
  public float baseSpawnInbetween;

  public GameObject green_par;
  public GameObject blue_par;
  public GameObject boss_par;

  //when (percentage wise) will it spawn more enemies
  [Range(0,100)]
  public float spawnReduction;
  [Range(0,100)]
  public float endSpawnReduction;
  void Start() {
    curSecInBetween = secondsInBetweenWaves;
    Wave();
  }
  public void Update() {
    if (curSecInBetween >= 0 && spawns.Count == dead) {
      curSecInBetween -= 1;
    }
      if (curSecInBetween <= 0) {
        StopCoroutine(SpawnInertvals(spawns, 0));
        spawns = new List<GameObject>();
        dead = 0;
        curSpawnNum = 0;
        Wave();
        curSecInBetween = secondsInBetweenWaves;
    }
  }

  public void Wave() {
  waveNum += 1f;

  spawnNum = baseSpawnNum + Mathf.RoundToInt(waveNum * (waveThreashold + (lateGameThreashold * (waveNum / lateGameWave))));

  Vector3 newSpawnPercentages = spawnPercentages + ((lateGameWeight-spawnPercentages)/ lateGameWave);
  Debug.Log(newSpawnPercentages);

  int centralSpawn = Random.Range(0, spawnPoints.Count + 1);

  for(int i = 0; i < spawnNum; i++) {
    int spawnId = Random.Range(0, 101);
    Debug.Log(spawnId);
    if (spawnId >= 100f - newSpawnPercentages.z) {
      curSpawn = p_enemy;
      }
    if (spawnId > newSpawnPercentages.x && spawnId < 100f - newSpawnPercentages.z) {
      curSpawn = b_enemy;
      }
    if (spawnId <= newSpawnPercentages.x) {
      curSpawn = g_enemy;
      }
    spawns.Add(curSpawn);
    }
    if(curSpawnNum != spawnNum - 1) {
      StartCoroutine(SpawnInertvals(spawns, centralSpawn));
    }
  }

  public IEnumerator SpawnInertvals(List<GameObject> e_spawns, int centralSpawn) {
    yield return new WaitForSeconds(FindInBetween(spawnNum/2));

    int i = Random.Range(1,101);
    int spawnPos = 0;
    if (i <= spawnFavourability.w && i <= spawnFavourability.w + spawnFavourability.x && i <= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y && i <= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y + spawnFavourability.z) {
      spawnPos = centralSpawn;
      }
    if (i >= spawnFavourability.w && i <= spawnFavourability.w + spawnFavourability.x && i <= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y && i <= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y + spawnFavourability.z) {
      spawnPos = centralSpawn + 1;
      if (spawnPos > spawnPoints.Count) {
        spawnPos = 0;
        }
      }
    if (i >= spawnFavourability.w && i >= spawnFavourability.w + spawnFavourability.x && i <= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y && i <= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y + spawnFavourability.z) {
      spawnPos = centralSpawn - 1;
      if (spawnPos < spawnPoints.Count) {
        spawnPos = 3;
        }
      }
    if (i >= spawnFavourability.w && i >= spawnFavourability.w + spawnFavourability.x && i >= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y && i <= spawnFavourability.w + spawnFavourability.x + spawnFavourability.y + spawnFavourability.z) {
      spawnPos = centralSpawn - 2;
      if (spawnPos == -1) {
        spawnPos = 3;
        }
      if (spawnPos == -2) {
        spawnPos = 2;
        }
      }
    GameObject enemy = Instantiate(e_spawns[curSpawnNum], spawnPoints[spawnPos].transform.position, Quaternion.identity);
    e_spawns[curSpawnNum] = enemy;

    curSpawnNum ++;

    if(curSpawnNum != spawnNum) {
      StartCoroutine(SpawnInertvals(spawns, centralSpawn));
    }

    if (curSpawnNum == spawnNum) {
      StopCoroutine(SpawnInertvals(e_spawns, 0));
    }

    //float y = FindInBetween(curSpawnNum/2);

  }

  public float FindInBetween(float h) {
      float spawnRed = spawnReduction + (Mathf.RoundToInt(waveNum * (endSpawnReduction * (waveNum / lateGameWave)))/10);

      float y = (-1/((h/spawnRed)*h)) * (Mathf.Pow(curSpawnNum - h, 2)) + spawnRed;
      float aby = (-1/((h/spawnRed))) * ((curSpawnNum - h) * Mathf.Sign(curSpawnNum - h)) + spawnRed;

      return(baseSpawnInbetween - (baseSpawnInbetween * (((y + aby)/2) / 100)));
    }
}
