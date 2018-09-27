using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCamera : MonoBehaviour
{
    public Material mat;
    void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    //void OnRenderImage(RenderTexture src, RenderTexture dest)
    //{
    //    Graphics.Blit(src, dest, mat);
    //}
}
