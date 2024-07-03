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

    void FixedUpdate()
    {
        if(!target) return;

        Vector2 direction = target.position - transform.position;
        float directionNorm = Mathf.Sqrt( MathF.Pow(direction.x, 2) + Mathf.Pow(direction.y,2) );
        direction.x /= directionNorm;
        direction.y /= directionNorm;

        rb.velocity = direction * bulletSpeed;
    }

    public void SetTarget(Transform _target){
        target = _target;
    }

    void OnCollisionEnter2D(Collision2D other){

        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);


    }
    
}
