using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip sonidoDisp;
    // Start is called before the first frame update
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sonidoDisp);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }
    
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        //gameobject es la referencia al objeto bala
        //destruimos la bala 
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnMovement john = collision.GetComponent<JohnMovement>();
        EnemigoScript enemigo = collision.GetComponent<EnemigoScript>();
        if (john != null)
        {
            john.Tocado();
        }
        if (enemigo != null)
        {
            enemigo.Tocado();
        }
        DestroyBullet();
    }

}
