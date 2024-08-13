using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;
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

    private static bool firstTurretClicked = false; //Varaible to check for the tutorial if the first turret has been clicked to show or not the popup
    private int cost;

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

    public void SetCost(int cost){
        this.cost = cost;
    }

    public int GetCost(){
        return cost;
    }
        
    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 140f));
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
        //da modificare transform.position --> firingPoint.position
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();

        bulletScript.SetTarget(target);
    }

    void OnMouseDown()
    {
        // Perform your action here

        if(TutorialManager.instance.GetState() < 4) return;
        
        if(!TutorialManager.firstTurretClicked) {   
            TutorialManager.instance.RunTutorialStep();
            TutorialManager.firstTurretClicked = true;
        }else{
            Debug.Log("My turret cost is " + cost + ". I will sell it for " + cost/2);
            SellPanelManager.main?.SetTurret(gameObject);
            SellPanelManager.main?.Pause();
        }
            
        }

    public void Sell(){

    }

}
