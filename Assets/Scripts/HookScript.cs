using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HookScript : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 5f;

    private Vector2 direction=new Vector2(0,0);

    public void SetDirection(Vector2 dir)
    {
        Debug.Log("setdirection dir"+dir);
        this.direction = dir.normalized;
        //Destroy(gameObject, lifetime);
    }

    void Update()
    {

        //GetComponent<Rigidbody2D>().linearVelocity = this.direction * this.speed;
        //Debug.Log("hook: " + direction);
        transform.Translate(this.direction *  this.speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}