using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform firingPoint;


    [Header("Attributes")]

    [SerializeField] private float targetinRange = 3f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float bulletPerSecond = 2f;


    private float timeUntilFire;
    private Transform target;

//     private void OnDrawGizmosSelected(){
//     Handles.color = Color.black;
//     Handles.DrawWireDisc(transform.position, transform.forward, targetinRange);
// }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!target){
            FindTarget();
            return;
        }else{
            timeUntilFire += Time.deltaTime;

            if( timeUntilFire >= (1 / bulletPerSecond) ){
                Shoot();
                timeUntilFire = 0f;
            }
        }
        
        // Rotate the turret
        RotateTowardsTarget();
        if(!CheckTargetIsInRange()){
            target = null;
        }
    }

        
    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation,rotationSpeed * Time.deltaTime);
    }

    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetinRange, (Vector2) transform.position, 0f, enemyMask);

        if(hits.Length > 0){
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange(){
        return Vector2.Distance(transform.position, target.position) <= targetinRange;
    }

    private void Shoot(){
        Debug.Log("Shoot porco cane");

        //da modificare transform.position --> firingPoint.position
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();

        bulletScript.SetTarget(target);

        

        
    }

}
