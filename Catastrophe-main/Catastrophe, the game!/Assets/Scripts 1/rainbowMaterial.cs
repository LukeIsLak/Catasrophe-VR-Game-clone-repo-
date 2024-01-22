using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowMaterial : MonoBehaviour
{
    public List<MeshRenderer> mat;
    [Range(0,255)]
    public float r;
    [Range(0,255)]
    public float g;
    [Range(0,255)]
    public float b;
    [Range(0,255)]
    public float a;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (r < 255 && g <= 0 && b <= 0) {
          r+=1*speed;
        }
        if (r >= 255 && g < 255 && b <= 0) {
          g+=1*speed;
        }
        if (r > 0 && g >= 255 && b <= 0) {
          r-=1*speed;
        }
        if (r <= 0 && g >= 255 && b < 255) {
          b+=1*speed;
        }
        if (r <= 0 && g > 0 && b >= 255) {
          g-=1*speed;
        }
        if (r < 255 && g <= 0 && b >= 255) {
          r+=1*speed;
        }
        if (r >= 255 && g <= 0 && b > 0) {
          b-=1*speed;
        }
        foreach (MeshRenderer m in mat) {
        m.material.color = new Color(r/255, g/255, b/255, a/255);
      }
    }
}
