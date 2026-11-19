
using UnityEngine;
using UnityEngine.InputSystem;
 
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float turnSpeed = 720f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;
 
    private CharacterController controller;
    private float verticalVelocity;
 
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null) cameraTransform = Camera.main.transform;
    }
 
    private void Update()
    {
        Keyboard kb = Keyboard.current; // Keyboard input
        if (kb == null) return;
 
        float x = (kb.dKey.isPressed ? 1f : 0f) - (kb.aKey.isPressed ? 1f : 0f); // Horizontal input
        float z = (kb.wKey.isPressed ? 1f : 0f) - (kb.sKey.isPressed ? 1f : 0f);// Vertical input
 
        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 direction = Vector3.ClampMagnitude(forward * z + right * x, 1f);
 
        float speed = kb.leftShiftKey.isPressed ? sprintSpeed : walkSpeed;
 
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime);
        }
 
        if (controller.isGrounded) // Grounded check
        {
            verticalVelocity = -2f; // Small downward force to keep the player grounded and make sure no funky stuff happens when walking down slopes and such
            if (kb.spaceKey.wasPressedThisFrame) // Jump input
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        verticalVelocity += gravity * Time.deltaTime;
 
        Vector3 velocity = direction * speed + Vector3.up * verticalVelocity; // Final velocity vector
        controller.Move(velocity * Time.deltaTime);
    }
}
 