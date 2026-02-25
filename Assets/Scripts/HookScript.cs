using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HookScript : MonoBehaviour
{
    public float speed =200f;
    public float lifetime = 1.5f;
    public float throwforce = 20;
    public Transform firePoint;
    public GrapplingHook spawner;


    private Vector2 direction=new Vector2(0,0);

    public void SetDirection(Vector2 dir)
    {
        //Debug.Log("setdirection dir"+dir);
        this.direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {

        this.GetComponent<Rigidbody2D>().linearVelocity = this.direction * 20;
        Debug.Log("hook: " + this.direction+" "+this.direction*this.speed);
        //transform.Translate(this.direction *  this.speed * Time.deltaTime);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HIT2");

        Vector3 direction = (transform.position - spawner.transform.position).normalized;
        Vector2 force = (direction * throwforce);
        force.y *= 2;
        spawner.GetComponent<Rigidbody2D>().linearVelocity = force;
        //spawner.GetComponent<Rigidbody2D>().AddForce(direction * 500f);
        Debug.Log(direction);
 
        Destroy(gameObject);
    }

}