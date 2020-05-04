using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP = 3;
    public float moveSpeed = 2;
    Animator anim;
    Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(rig.velocity.x));
    }

    public void TakeDamage(int damage){
        enemyHP -= damage;
        if(enemyHP <= 0){
            anim.SetTrigger("dead");  
        }
    }

    public void DestroyEnemy(){
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Limiter"){
            moveSpeed *= -1;
            if(moveSpeed > 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else{
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
