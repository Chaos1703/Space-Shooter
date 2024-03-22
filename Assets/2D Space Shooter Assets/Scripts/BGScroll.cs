using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private float scrollSpeed = 0.1f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float x = Time.time * scrollSpeed;
        Vector2 offset = new Vector2(x, 0f);
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
