using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform _camera;

    private float MouseX;

    private float MouseY;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        MouseY = Input.GetAxis("Mouse Y");

        MouseX = Input.GetAxis("Mouse X");
        
        _camera.localEulerAngles += new Vector3(-MouseY, 0, 0);

        transform.localEulerAngles += new Vector3(0, MouseX, 0);
    }
}
