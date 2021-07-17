using UnityEngine;

public class InteractControl : MonoBehaviour
{
    private RaycastHit _hit;

    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_camera.position, _camera.forward * 10, out _hit, 10))
            {
                if (_hit.collider.GetComponent<FloorButton>())
                {
                    InteractWithElevatorButton(_hit.collider.GetComponent<FloorButton>());
                }
            }
        }
    }

    private void InteractWithElevatorButton(FloorButton floorButton)
    {
        floorButton.OnButtonClick();
    }
}
