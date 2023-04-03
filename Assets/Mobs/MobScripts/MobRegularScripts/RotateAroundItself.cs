using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundItself : MonoBehaviour
{
    private Rigidbody2D rb; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (0,0,-1)  * 15 * rb.velocity.x * Time.deltaTime, Space.World);
    }
}
