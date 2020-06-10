using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    public GameObject bulletPrefab;
    public float coolDownTime = 1.0f;
    private float coolDownTimer;
    private bool canShoot = true;
    public float bulletSpeed = 20;
    public Transform shootingPosition;
    private float direction = 1;

    new void Update(){
        distance = Vector2.Distance(transform.position, playerTransform.position);
        if(distance <= attackDistance){
            if(transform.position.x > playerTransform.position.x){
                moveSpeed = -2;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else{
                moveSpeed = 2;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if(distance <= attackDistance && canShoot){
            Attack();
            canShoot = false;
            coolDownTimer = coolDownTime;
        }

        if(!canShoot){
            coolDownTimer -= Time.deltaTime;
            if(coolDownTimer <= 0){
                canShoot = true;
            }
        }
    }

    new public void Attack(){
        anim.SetTrigger("attack");
        GameObject bullet = Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        if(moveSpeed>0)
            rig.velocity = new Vector2(bulletSpeed, rig.velocity.y);
        else if(moveSpeed<0)
            rig.velocity = new Vector2(-bulletSpeed, rig.velocity.y);
    }
}
