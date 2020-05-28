using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(int levelId){
        SceneManager.LoadScene(levelId);
    }

    public void ExitGame(){
        Application.Quit();
        Debug.Break();
    }
}
