using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public bool hasLanded = false;
    public bool isSquating = false;
    public float velocityScale = 1f;
    public float bounciness = 1f;

    private enum MouseButton{
        MouseButtonLeft,
        MouseButtonRight,
        MouseButtonMiddle
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton((int) MouseButton.MouseButtonLeft) && hasLanded && !isSquating) {
            Sqaut();
        }

        if(isSquating){
            Aim();
        }

        if(isSquating && !Input.GetMouseButton((int) MouseButton.MouseButtonLeft)) {
            Jump();
        }
    }

    void Sqaut() {
        this.transform.localScale = new Vector3(1, 0.5f, 1);
            isSquating = true;
    }

    void Jump() {
        isSquating = false;

        var worldMousePosition = Camera.main.ScreenToWorldPoint(
            new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z)
        );

        var aimMagnitude = worldMousePosition - this.transform.position;
        this.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(aimMagnitude * velocityScale);
    }

    void Aim(){
        // Implement aim visual
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Platform")) {
            hasLanded = true;
        } 
        else if(collision.gameObject.CompareTag("Wall")) {
            var currentVelocity = collision.contacts[0].relativeVelocity;

            this.gameObject.GetComponent<Rigidbody2D>().AddForce(currentVelocity * bounciness);
        }
    }
    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Platform")) {
            hasLanded = false;
        }
    }
}
