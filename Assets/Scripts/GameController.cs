using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject character;
    public GameObject instructions;
    public AudioClip reversedMusic;
    public AudioClip regularMusic;
    public GameObject controls;


    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Y)) {
            SceneManager.LoadScene("mainScene");
        }

        if(Input.GetKeyDown(KeyCode.R)){
            if(!character.GetComponent<CharacterMovement>().isReversed) {
                GetComponent<AudioSource>().clip = reversedMusic;
                GetComponent<AudioSource>().Play();
            }
            else {
                GetComponent<AudioSource>().clip = regularMusic;
                GetComponent<AudioSource>().Play();
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("hit");            
            instructions.SetActive(false);
            controls.SetActive(true);
        }
        
    }
}
