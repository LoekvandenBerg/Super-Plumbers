using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int points;
    private Rigidbody2D body;
    [SerializeField]
    private float speed;
    private Vector2 movementDir;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        //movementDir = Vector2.right;
    }


    // Update is called once per frame
    void Update()
    {
        Move(movementDir);
    }

    public void Move(Vector2 direction)
    {
        movementDir = direction;
        body.velocity = new Vector2(direction.x * speed, body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            movementDir *= -1f;
        }
    }
}
