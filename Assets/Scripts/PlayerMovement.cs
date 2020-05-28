using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4;
    public float jumpForce = 2;
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spr;
    public Transform groundCheck;
    public bool grounded;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    public bool isJumping;
    private float jumpTimeCounter;
    public float jumpTime;

    public bool attacking;
    public int playerHP = 3;
    public Slider playerHPBar;
    public bool isAlive = true;

    public AudioClip[] sounds;
    
    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAlive){
            playerHPBar.value = playerHP;
            //float h = Input.GetAxis("Horizontal");
            float h = SimpleInput.GetAxis("Horizontal");

            rig.velocity = new Vector2(movementSpeed*h, rig.velocity.y);
            anim.SetFloat("speed", Mathf.Abs(h));

            grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            anim.SetBool("jump", !grounded);
            
            // if(Input.GetButtonDown("Jump") && grounded){
            //     rig.AddForce(Vector2.up * jumpForce);
            // }

            //Input.GetButtonDown
            if(SimpleInput.GetAxis("Vertical") > 0 && grounded){
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rig.velocity = Vector2.up * jumpForce;
                //aud.PlayOneShot(sounds[0]);
            }
            //Input.GetButton
            if(SimpleInput.GetAxis("Vertical") > 0 && isJumping){
                if(jumpTimeCounter >0){
                    rig.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else{
                    isJumping = false;
                }
            }
            //Input.GetButtonUp
            if(SimpleInput.GetAxis("Vertical") == 0 ){
                isJumping = false;

            }

            if(h > 0)
                Flip(true);
            else if(h < 0)
                Flip(false);


            if(Input.GetKeyDown(KeyCode.E)){
                anim.SetTrigger("attack");
                //aud.PlayOneShot(sounds[1]);
            }
        }
    }

    void Flip(bool faceRight){
        if(faceRight)
            spr.flipX = false;
        else
            spr.flipX = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Heal"){
            TakeDamage(-1);
            Destroy(other.gameObject);
        }
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

    public void AttackAnimation(){
        anim.SetTrigger("attack");
    }

    public void TakeDamage(int damage){
        playerHP -= damage;
        //aud.PlayOneShot(sounds[2]);

        if(playerHP > 3)
            playerHP = 3;

        if(playerHP <= 0){
            playerHPBar.value = playerHP;
            Destroy(playerHPBar.transform.GetChild(1).gameObject);
            anim.SetTrigger("die");  
            isAlive = false;
            Invoke("Respawn", 4);
        }
    }

    public void DestroyPlayer(){
        //aud.PlayOneShot(sounds[3]);
        Destroy(this.gameObject);
    }

    void Respawn(){
        //aud.PlayOneShot(sounds[4]);
        transform.position = GameObject.Find("Respawn").transform.position;
        anim.SetTrigger("respawn");
        playerHP = 3;
        isAlive = true;
    }
}
