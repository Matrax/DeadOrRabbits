using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsScript : MonoBehaviour
{

    //Player controls attributes
    public float moveSpeed;
    public float sensitivity;
    public GameObject view;
    public GameObject spot;
    public Vector3 look;
    public bool dead;
    public bool paused;

    //KeyCode
    public KeyCode forwardKey;
    public KeyCode behindKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode sprintKey;

    //Private attributes
    private Transform cameraTransform;
    private Transform spotTransform;
    private Rigidbody body;

    void Start()
    {
        this.dead = false;
        this.paused = false;

        //Private attributes
        this.body = this.GetComponent<Rigidbody>();
        this.cameraTransform = this.view.GetComponent<Transform>();
        this.spotTransform = this.spot.GetComponent<Transform>();

        //Cursor mode
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //If paused, dont move
        if (this.paused == false && this.dead == false)
        {
            //Mouse controls
            this.look.x = Mathf.Clamp(this.look.x + -Input.GetAxis("Mouse Y") * this.sensitivity, -80f, 80f);
            this.look.y += Input.GetAxis("Mouse X") * this.sensitivity;
            this.cameraTransform.eulerAngles = this.look;
            this.spotTransform.eulerAngles = this.look;
        }
    }

    void FixedUpdate()
    {
        //If paused, dont move
        if (this.paused == false && this.dead == false)
        {
            bool keyPressed = false;
            float directionCount = 0;
            Vector3 direction = new Vector3(0, 0, 0);

            if (Input.GetKey(this.behindKey))
            {
                direction.x += -this.cameraTransform.forward.x;
                direction.z += -this.cameraTransform.forward.z;
                directionCount++;
                keyPressed = true;
            }

            if (Input.GetKey(this.forwardKey))
            {
                direction.x += this.cameraTransform.forward.x;
                direction.z += this.cameraTransform.forward.z;
                directionCount++;
                keyPressed = true;
            }

            if (Input.GetKey(this.rightKey))
            {
                direction.x += this.cameraTransform.right.x;
                direction.z += this.cameraTransform.right.z;
                directionCount++;
                keyPressed = true;
            }

            if (Input.GetKey(this.leftKey))
            {
                direction.x += -this.cameraTransform.right.x;
                direction.z += -this.cameraTransform.right.z;
                directionCount++;
                keyPressed = true;
            }

            if (keyPressed == true)
            {
                this.body.MovePosition(this.transform.position + direction * (this.moveSpeed / directionCount) * Time.smoothDeltaTime);
            }
        }
    }
}
