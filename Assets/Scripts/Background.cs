using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Material bgMaterial;
    [SerializeField] float scrollSpeed = 0.2f;

    void Update(){
        Vector2 direction = Vector2.up;

        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
