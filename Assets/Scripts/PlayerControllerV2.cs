using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    public Vector3 Tester;
    public Camera TPSCamera;
    public Camera FPSCamera;
    public float MovementSpeed = 10f;
    public Animator anim;
    private Vector2 KeyboardMovement;
    private bool CamTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        AnimationSet();
        if (CamTrigger)
        {
            FPSCamera.enabled = !FPSCamera.enabled;
            TPSCamera.enabled = !TPSCamera.enabled;
            CamTrigger = !CamTrigger;
        }
    }

    private void FixedUpdate()
    {
        TPSCam();
        PlayerMove();
    }

    void GetInput()
    {
        KeyboardMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.F5))
        {
            CamTrigger = !CamTrigger;
        }
    }

    void TPSCam()
    {

    }

    void PlayerMove()
    {
        
        transform.rotation = Quaternion.LookRotation(new Vector3(TPSCamera.transform.forward.x, 0, TPSCamera.transform.forward.z), TPSCamera.transform.up);
        Vector3 velocity = transform.forward * KeyboardMovement.y * MovementSpeed + transform.right * KeyboardMovement.x * MovementSpeed;
        velocity.y = GetComponent<Rigidbody>().velocity.y;
        GetComponent<Rigidbody>().velocity = velocity;
    }

    void AnimationSet()
    {
        if (TPSCamera.enabled) anim.SetFloat("MoveDir", (KeyboardMovement.y + 1) / 2);
    }

}
