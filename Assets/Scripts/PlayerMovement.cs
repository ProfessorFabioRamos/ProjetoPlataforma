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
        if(Input.GetButtonDown("Jump")){
            rig.AddForce(Vector2.up * jumpForce);
            anim.SetBool("jump", true);
        }

        if(h > 0)
             Flip(true);
        else if(h < 0)
            Flip(false);
    }

    void Flip(bool faceRight){
        if(faceRight)
            spr.flipX = false;
        else
            spr.flipX = true;
    }

    void OnCollisionEnter2D(){
        anim.SetBool("jump", false);
    }
}
