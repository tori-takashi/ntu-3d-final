using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    void Awake() {
        if (soundManager == null) {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Start(){
    }

    void Update()
    {
       modifyMusic(); 
    }

    void modifyMusic(){
        if(SceneManager.GetActiveScene().name == "MultiMode" || SceneManager.GetActiveScene().name == "SingleMode"){
            GetComponent<AudioSource>().volume = 0.1f;
        } else {
            GetComponent<AudioSource>().volume = 0.4f;
        } 
    }
}
