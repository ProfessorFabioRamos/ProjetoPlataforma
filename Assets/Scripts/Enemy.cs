using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP = 3;
    Animator anim;
    Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.left * Time.deltaTime*50);
        rig.AddForce(new Vector2(5000,0));
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
}
