using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public CharacterController charC;
    public Camera playerCam;
    public float speed = 10f;
    public float sprintSpeed = 20f;
    public float gravity = 20f;
    public float jumpSpeed = 10f;
    public GameObject bullet;
    public Transform spawnPoint;
    private Vector3 moveDirection = Vector3.zero;

    public enum State
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public State currentState = State.MouseXandY;

    [Header("Sensitivity")]
    //Floats for x and y sensitivity
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    [Header("Y Axis Clamp")]
    //Rotation limit for Y axis
    public float minimumY = -60f;
    public float maximumY = 60f;

    float rotationY = 0f;

    void Start()
    {
        charC = GetComponent<CharacterController>();
        //playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //playerCam.transform.SetParent(target);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (charC.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection * speed);


            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = 10f;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
            }

            else
            {

            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        charC.Move(moveDirection * Time.deltaTime);

        switch (currentState)
        {
            case State.MouseXandY:
                MouseXandY();
                break;
            case State.MouseX:
                MouseX();
                break;
            case State.MouseY:
                MouseY();
                break;
            default:
                break;

        }
    }
    public void Fire()
    {
        GameObject clone = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        BulletC newBullet = clone.GetComponent<BulletC>();
        newBullet.Fire(transform.forward);

    }
    private void MouseXandY()
    {
        //The float for X axis is equal to Y axis + the mouse input on the X axis times our X sensitivity
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        //Y Rotation is += our mouse Y times Y sensitivity
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //Min and Max Y axis limit is clamped using Mathf.
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //Transform players local position to the new vector3 rotation. -y rotation on the X axis and X rotation on the Y axis
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
    private void MouseX()
    {
        //transform rotation around Y axis by mouse input. Mouse X times sensitivityX
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
    }
    private void MouseY()
    {
        //Rotation Y is += to mouse input for Mouse Y times Y sensitivity
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //Min and Max Y axis limit is clamped using Mathf.
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //Transform players local position to the new vector3 rotation. -y rotation on the X axis and X rotation on the Y axis
        transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}
