using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _sensX;
    [SerializeField] private float _sensY;

    private float _xRotation;
    private float _yRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensY * Time.deltaTime;

        _xRotation -= mouseY;
        _yRotation += mouseX;

        _xRotation = Mathf.Clamp(_xRotation, -67.5f, 67.5f);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}
