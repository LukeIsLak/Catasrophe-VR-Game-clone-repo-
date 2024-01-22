using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public GameObject audience;
    public bool appl = false;
    public List<Material> mat;
    public int spawnNum;
    public List<float> heightMap;
    public List<float> lengthMap;
    public List<float> offset;

    [Range(0,1)]
    public float yeildTime;

    private int count;
    void Start()
    {
      foreach (float h in heightMap) {
        for (int i = 0; i < spawnNum; i ++) {
          Vector3 spawnPos = new Vector3(0, h, 0);
          GameObject spawn = Instantiate(audience, spawnPos, Quaternion.identity);
          spawn.transform.eulerAngles = new Vector3(0,(360/spawnNum * i) + offset[count],0);
          spawn.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, -lengthMap[count]);

          int matNum = Random.Range(0, mat.Count);
          spawn.transform.GetChild(0).GetComponent<MeshRenderer>().material = mat[matNum];
          spawn.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = mat[matNum];
          spawn.transform.GetChild(0).GetChild(2).GetComponent<MeshRenderer>().material = mat[matNum];

          spawn.transform.parent = this.gameObject.transform;

        }
        count ++;
      }
    }
}
