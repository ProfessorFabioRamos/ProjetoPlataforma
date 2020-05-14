using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP = 3;
    public float moveSpeed = 2;
    public float attackDistance = 3;
    Animator anim;
    Rigidbody2D rig;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(rig.velocity.x));
    }

    void Update(){
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if(distance <= attackDistance)
            anim.SetTrigger("attack");
    }

    public void TakeDamage(int damage){
        enemyHP -= damage;
        if(enemyHP <= 0){
            anim.SetTrigger("dead");  
        }
    }

    public void Damage(){
        try{
            playerTransform.GetComponent<PlayerMovement>().TakeDamage(1);
        }
        catch(System.Exception){
            Debug.LogWarning("O Player já foi derrotado");
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
