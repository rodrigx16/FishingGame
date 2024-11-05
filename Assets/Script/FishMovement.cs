using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 2f;  // Velocidade do peixe
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Define a direção do movimento baseado na escala do peixe
        float direction = transform.localScale.x > 0 ? 1 : -1;
        rb.velocity = new Vector2(speed * direction, 0);
    }

    // Destrói o peixe ao sair dos limites da tela
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
