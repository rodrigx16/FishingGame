using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 2f;  
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

     
        float direction = transform.localScale.x > 0 ? 1 : -1;
        rb.velocity = new Vector2(speed * direction, 0);
    }

   
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
