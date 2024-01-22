using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    public int moneyValue;

    public void OnDestroy() {
      PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + moneyValue);
    }
}
