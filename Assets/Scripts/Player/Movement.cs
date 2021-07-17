using UnityEngine;

public class Movement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private const string Vertical = "Vertical";

    [SerializeField] private float _maxSpeed;

    private float _horizontalMove;

    private float _verticalMove;

    private void FixedUpdate()
    {
        _horizontalMove = Input.GetAxis(Horizontal) * _maxSpeed;

        _verticalMove = Input.GetAxis(Vertical) * _maxSpeed;

        transform.localPosition += transform.forward * _verticalMove;

        transform.localPosition += transform.right * _horizontalMove;
    }
}
