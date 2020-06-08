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
    new void Update(){
        distance = Vector2.Distance(transform.position, playerTransform.position);
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
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(bulletSpeed, rig.velocity.y);
    }
}
