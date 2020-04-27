using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4;
    public float jumpForce = 200;
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spr;
    public Transform groundCheck;
    public bool grounded;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movementSpeed*h, rig.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(h));

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        anim.SetBool("jump", !grounded);

        if(Input.GetButtonDown("Jump") && grounded){
            rig.AddForce(Vector2.up * jumpForce);
        }

        if(h > 0)
             Flip(true);
        else if(h < 0)
            Flip(false);


        if(Input.GetKeyDown(KeyCode.E)){
            anim.SetTrigger("attack");
        }
    }

    void Flip(bool faceRight){
        if(faceRight)
            spr.flipX = false;
        else
            spr.flipX = true;
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Enemy" && attacking){
            other.gameObject.GetComponent<Enemy>().TakeDamage(1);
            attacking = false;
        }
    }

    public void Attack(){
        attacking = true;
    }
    public void CancelAttack(){
        attacking = false;
    }

}
