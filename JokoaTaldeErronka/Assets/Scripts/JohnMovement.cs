using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class JohnMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;
    public AudioClip sonidoFin;

    public GameObject panel;

    private bool tocandoSuelo;
    public DBDemo dbDemo;
    
    private Rigidbody2D Rigidbody2D;
    private Animator animator;

    private int vida = 10;
    public Image[] corazones;

    private float ultimoDisparo;
    private float Horizontal; 
    void Start()
    {
        // meter el componente Ridigbody2D a jhon movement
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // valores en funcion de lo que teclea el jugador,saber si va a la izquierda o a la derecha
        Horizontal = Input.GetAxisRaw("Horizontal");

        //si clicka a le metemos el valor -1 para poner inversa la imagen
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // si el jugador esta clickando a o d metemos la animacion
        animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down*0.1f,Color.red);
        // hacemos que si esta tocando el suelo pueda saltar
        if (Physics2D.Raycast(transform.position,Vector3.down,0.1f ))
        {
            tocandoSuelo = true;
        }
        else
        {
            tocandoSuelo = false;
        }
        if (Input.GetKeyDown(KeyCode.W)&& tocandoSuelo)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > ultimoDisparo + 0.25f)
        { 
            Shoot();
            ultimoDisparo = Time.time;
        }
        if(transform.position.y < -4.5)
        {
            // para parar el juego
            Time.timeScale = 0;
            panel.SetActive(true);
            // para reiniciar la escena timescale = 1;
        }
    }
    
    private void FixedUpdate()
    {
        //Vector2(2 elementos del mundo, X e Y)
        Rigidbody2D.velocity = new Vector2(Horizontal*Speed, Rigidbody2D.velocity.y);
    }
    
    private void Jump()
    {
        //a�adimos una fuerza hacia arriba
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        //saber hacia que lado tiramos la bala
        if (transform.localScale.x == 1.0f)direction = Vector3.right;
        else direction = Vector3.left;
        
        // coge la bala y la duplica en la posicion que queramos sin rotacion(para sacar la bala desde donde queramos)
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction *0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
    public void Tocado()
    {
        vida = vida - 1;
        corazones[vida].color = Color.cyan;
        if(vida  == 0) { 
            Camera.main.GetComponent<AudioSource>().PlayOneShot(sonidoFin);
            Destroy(gameObject);
            Time.timeScale = 0;
            panel.SetActive(true);
            dbDemo = new DBDemo("MarkelUNI", 100.0f);
            
        }
    }
}
