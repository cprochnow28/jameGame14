using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject character;
    public float cameraOffset = 400;
    public float camSpeed = 10f;
    
    void Start()
    {
        
    }

    void Update() {
        if(character.transform.position.y > (cameraOffset + this.transform.position.y)) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * camSpeed);
        }
        else if(character.transform.position.y < (this.transform.position.y - cameraOffset )) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * camSpeed);
        }
        else {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }
}
