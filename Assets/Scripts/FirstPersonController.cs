using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // References
    public GameObject bullet;
    public Transform cameraTransform;
    public CharacterController characterController;
    public float bulletpower;

    // Player settings
    public float cameraSensitivity;
    public float moveSpeed;
    public float moveInputDeadZone;

    // Touch detection
    int leftFingerId, rightFingerId;
    float halfScreenWidth;

    // Camera control
    Vector2 lookInput;
    float cameraPitch;

    // Player movement
    Vector2 moveTouchStartPosition;
    Vector2 moveInput;
    public AudioClip shootSFX;

    Quaternion Mx;

    // Start is called before the first frame update
    void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        
        Mx.x += Input.GetAxis("Mouse Y") * 3  ;
        Mx.y += Input.GetAxis("Mouse X") * 3;
        Mx.x = Mathf.Clamp(Mx.x, -90, 90);
        Mx.y = Mathf.Clamp(Mx.y, -30, 30);
        cameraTransform.localRotation = Quaternion.Euler(Mx.x, Mx.y,Mx.z);

        print(Mx.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }
#endif
        // Handles input
        GetTouchInput();


        if (rightFingerId != -1) {
            // Ony look around if the right finger is being tracked
            Debug.Log("Rotating");
            LookAround();
        }

        if (leftFingerId != -1)
        {
            // Ony move if the left finger is being tracked
            Debug.Log("Moving");
           // Move();
        }
    }

    void GetTouchInput() {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId) {

                        // calculating the position delta from the start position
                        moveInput = t.position - moveTouchStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround() {

        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        lookInput.x = Mathf.Clamp(lookInput.x, -30, 30);
        transform.Rotate(transform.up, lookInput.x);
    }

   /* void Move() {

        // Don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        // Multiply the normalized direction by the speed
        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
        // Move relatively to the local transform's direction
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }*/
  public void shoot()
    {
        GameObject newProjectile = Instantiate(bullet,Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation) as GameObject;
        if (!newProjectile.GetComponent<Rigidbody>())
        {
            newProjectile.AddComponent<Rigidbody>();
        }
        newProjectile.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletpower, ForceMode.VelocityChange);
        AudioSource.PlayClipAtPoint(shootSFX, newProjectile.transform.position);
    }

}
