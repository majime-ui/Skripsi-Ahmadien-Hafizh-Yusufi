using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMenuParallax : MonoBehaviour
{
    public MeshRenderer mesh;
    public Vector2 backGroundSpeed;

    private void Update()
    {
        mesh.material.mainTextureOffset += backGroundSpeed * Time.deltaTime;
    }
}
