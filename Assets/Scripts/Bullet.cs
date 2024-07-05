using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    private Transform target;
    private float aliveTime;
    public AudioClip explosionSound; // Drag your explosion sound here in the Inspector
    private AudioSource audioSource;

    private void Start(){
        aliveTime = 0f;

         // Get the AudioSource component attached to this game object
        audioSource = GetComponent<AudioSource>();
        // Ensure the explosionSound is set in the AudioSource
        audioSource.clip = explosionSound;
    }

    void FixedUpdate()
    {
        if(!target) return;

        Vector2 direction = target.position - transform.position;
        float directionNorm = Mathf.Sqrt( MathF.Pow(direction.x, 2) + Mathf.Pow(direction.y,2) );
        direction.x /= directionNorm;
        direction.y /= directionNorm;

        rb.velocity = direction * bulletSpeed;
       

    }

    private void Update(){
         aliveTime += Time.deltaTime;
        if( aliveTime >= 1f ){
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform _target){
        target = _target;
    }

    void OnCollisionEnter2D(Collision2D other){

        // Play the explosion sound
        audioSource.Play();

        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
    
}
