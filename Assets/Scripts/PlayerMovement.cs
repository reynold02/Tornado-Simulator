using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private bool IsGrounded = false;
    [SerializeField] private float JumpForce, WalkingSpeed, CameraMovementSpeed;
    private Rigidbody rb;
    private Vector3 CameraMovement, VectorMovement;
    private Transform CameraTransform;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;
    [SerializeField] private float JumpWaitValue = 0.5f;

    private void Awake()
    {
        IsGrounded = true;
    }
    // Start is called before the first frame update
    void Start()
    {//-4
        rb = GetComponent<Rigidbody>();
        CameraTransform = VirtualCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CameraTransform.eulerAngles += (CameraMovement * Time.deltaTime * CameraMovementSpeed);
        //CameraTransform.eulerAngles = new Vector3(Mathf.Clamp(CameraTransform.eulerAngles.x, -40, 40), CameraTransform.eulerAngles.y, CameraTransform.eulerAngles.z);
        transform.eulerAngles = new Vector3(0, VirtualCamera.transform.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        
        //rb.MovePosition(rb.position + (VectorMovement * WalkingSpeed * Time.fixedDeltaTime));
        rb.MovePosition(rb.position + (transform.forward * VectorMovement.z * Time.fixedDeltaTime * WalkingSpeed));
        rb.MovePosition(rb.position + (transform.right * VectorMovement.x * Time.fixedDeltaTime * WalkingSpeed));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Work");
        if (IsGrounded && context.performed)
        {
            IsGrounded = false;
            //Debug.Log(context.phase);
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            StartCoroutine(GroundTimer());
            //CameraTransform.rot
        }
    }
    public void Movement(InputAction.CallbackContext context)
    {
        VectorMovement = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void MouseMovement(InputAction.CallbackContext context)
    {
        CameraMovement = new Vector3(-context.ReadValue<Vector2>().y, context.ReadValue<Vector2>().x, 0);
    }
    IEnumerator GroundTimer()
    {
        yield return new WaitForSeconds(JumpWaitValue);
        IsGrounded = true;
    }

}
