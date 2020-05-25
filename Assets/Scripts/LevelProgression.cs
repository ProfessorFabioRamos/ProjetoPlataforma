using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    SpriteRenderer rend;
    public Sprite imgOpen;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            rend.sprite = imgOpen;
            Invoke("LevelCompleted", 2);
        }
    }

    void LevelCompleted(){
        SceneManager.LoadScene(1);
    }
}
