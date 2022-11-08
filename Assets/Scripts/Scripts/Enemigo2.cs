using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    public int health;
    Animator anim;
    public GameObject target;
    public float speed = 3;
    public enum Comportamiento{ LookAt, Chase}
    public Comportamiento comportamiento;
    
    public float maxDistance = 5;
   
    void Start()
    {

        health = 150;

        anim = GetComponentInChildren<Animator>();


    }


    void Update()
    {
        
        

        LookAtPlayer(comportamiento);
        target = GameObject.Find("PlayerTarget");

         if (Vector3.Distance(target.transform.position, transform.position) <= maxDistance)
        {

            GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            anim.SetBool("Walk", false);
            return;

        }


    }

    public void LookAtPlayer(Comportamiento comportamiento)
    {

        switch (comportamiento)
        {
            case Comportamiento.LookAt:

                transform.LookAt(target.transform);

                break;


            case Comportamiento.Chase:

                ChekPosition();


                break;




        }
    }
    void ChekPosition()
    {

        if (Vector3.Distance(target.transform.position, transform.position) >= maxDistance +2 )
        {

            transform.LookAt(target.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);

            anim.SetBool("Walk", true);
            return;
        }

       

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            Debug.Log("I GOT YOU");

        }
        if (collision.collider.tag == "Bullet")
        {
            Destroy(collision.collider.gameObject);
            health = health - 50;
            Debug.Log("Enemy life -50");


        }
        if (health == 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Dead");
        }


    }
}
