using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    
    public int health;
    public GameObject target;
    public Animator anim;
    public AudioSource pasos;
    public float maxDistance = 3;
    float speed = 5;
    
   
    void Start()
    {
        
        health = 100;
        anim = GetComponent<Animator>();
        
   
    }

    // Update is called once per frame
    void Update()
    {
        
        ChekPosition();
        target = GameObject.Find("PlayerTarget");
        transform.LookAt(target.transform);

    }

    void ChekPosition()
    {
        
        if (Vector3.Distance(target.transform.position, transform.position) >= maxDistance)
        {
            
            GetComponent<Rigidbody>().velocity = transform.forward * speed ;
            Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);
            
            anim.SetBool("Walk", true);
            return;
        }

        else 
        {
            
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            anim.SetBool("Walk", false);
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
