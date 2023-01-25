using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public GameObject John;
    public GameObject BulletPrefab;
    public AudioClip Monkey;

    private int vida = 3;
    private float ultimoDisparo;
    // Update is called once per frame
    void Update()
    {
        // si ya esta muerto que no calcule la posicion 
        if(John == null)
        {
            return;
        }
        // hacemos que mire al jugador 
        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        // el valor da siempre en positivo asique si esta a X distancia disparara
        float distancia = Mathf.Abs(John.transform.position.x - transform.position.x);
        if(distancia < 1.0f && Time.time > ultimoDisparo + 0.75f)
        {
            ultimoDisparo = Time.time;
            Shoot();
        }
        if (transform.position.y < -4.5)
        {
            ScoreScript.instance.suma();
            Destroy(gameObject);
        }
    }
    private void Shoot()
    {
        Vector3 direction;
        //saber hacia que lado tiramos la bala
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        // coge la bala y la duplica en la posicion que queramos sin rotacion(para sacar la bala desde donde queramos)
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
    public void Tocado()
    {
        vida = vida - 1;
        if (vida == 0) {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Monkey);
            ScoreScript.instance.suma();
            Destroy(gameObject);       
        }
    }
}
