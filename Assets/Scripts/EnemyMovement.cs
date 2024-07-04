using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damageDealt = 1;

    private Transform target;
    private int pathIndex = 0;



    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f){
            pathIndex++;
            

            if(pathIndex == LevelManager.main.path.Length){
                EnemySpawner.onEnemydestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.DecreaseLife(damageDealt);
                return;
            }
            else{
                target = LevelManager.main.path[pathIndex];
            }
        }

    }

    private void FixedUpdate(){

        Vector2 direction = target.position - transform.position;
        float directionNorm = Mathf.Sqrt( MathF.Pow(direction.x, 2) + Mathf.Pow(direction.y,2) );
        direction.x /= directionNorm;
        direction.y /= directionNorm;

        if (float.IsNaN(direction.x) || float.IsNaN(direction.y)) return;

        rb.velocity = direction * moveSpeed;


    }
}
