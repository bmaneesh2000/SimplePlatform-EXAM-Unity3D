using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerScript : MonoBehaviour
{
    [SerializeField] private float speed = 15f;

    private Vector2 facing = Vector2.left;

    private void Update()
    {
        transform.Translate(facing * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="man")
        {
            facing.x *= -1;
            transform.localScale = new Vector3(facing.x * -1f, 1f, 1f);
        }
        
    }
}
