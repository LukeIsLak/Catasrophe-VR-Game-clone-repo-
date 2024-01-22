using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
  public string nextScene;
  public bool condition;

  void Update() {
    if (condition) {
      SceneManager.LoadSceneAsync(nextScene);
    }
  }
}
