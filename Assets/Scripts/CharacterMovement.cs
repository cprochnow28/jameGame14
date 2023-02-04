using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public bool hasLanded = true;
    public bool isSquating = false;

    public float velocityScale = 1f;

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
            new Vector3 (Input.mousePosition.x, Input.mousePosition.x, this.transform.position.z)
            );

        var aimMagnitude = worldMousePosition - this.transform.position;
        Debug.Log(aimMagnitude);
        this.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(aimMagnitude * velocityScale);
        //hasLanded = false;
    }

    void Aim(){
        // Implement aim visual
    }
}
