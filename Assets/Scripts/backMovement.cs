using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backMovement : MonoBehaviour
{

    public float speed;

    [SerializeField]
    private Renderer bgRenderer;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
