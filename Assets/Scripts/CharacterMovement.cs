using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public bool hasLanded = false;
    public bool isSquating = false;
    public float velocityScale = 1f;
    public float bounciness = 1f;
    public float reverseWorldXLocation = 0f;
    public bool isReversed = false;
    public GameObject mainCamera;

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

        if(Input.GetKeyDown(KeyCode.R)){
            Warp();
        }

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
            isSquating = true;
            GetComponent<Animator>().SetBool("isSquat", true);
    }

    void Jump() {
        isSquating = false;

        var worldMousePosition = Camera.main.ScreenToWorldPoint(
            new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z)
        );


        var aimMagnitude = worldMousePosition - this.transform.position;

        Debug.Log(aimMagnitude);

        aimMagnitude.x = aimMagnitude.x > 300 ? 300 : aimMagnitude.x;
        aimMagnitude.y = aimMagnitude.y > 600 ? 600 : aimMagnitude.y;

        aimMagnitude.x = aimMagnitude.x < -300 ? -300 : aimMagnitude.x;
        aimMagnitude.y = aimMagnitude.y < -600 ? -600 : aimMagnitude.y;

        Debug.Log(aimMagnitude);

        var force = aimMagnitude * velocityScale;

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(isReversed ? force * -1 : force);
        GetComponent<Animator>().SetBool("isSquat", false);
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

    void Warp() {
        if(isReversed){
            transform.position = new Vector3(
                transform.position.x - reverseWorldXLocation, transform.position.y, 0
            );
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x - reverseWorldXLocation, mainCamera.transform.position.y, mainCamera.transform.position.z
            );
            isReversed = false;
        }
        else{
            transform.position = new Vector3(
                transform.position.x + reverseWorldXLocation, transform.position.y, 0
            );
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x + reverseWorldXLocation, mainCamera.transform.position.y, mainCamera.transform.position.z
            );
            isReversed = true;
        }
    }
}
