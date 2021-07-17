using UnityEngine;
using UnityEngine.Events;

public class ElevatorEngine : MonoBehaviour
{
    private const float FloorHeight = 6.68f;

    private const float Speed = 2;

    [Space]
    [SerializeField] private Elevator _elevator;

    [SerializeField] private FloorButton[] _buttons;

    [Header("Events")]
    [SerializeField] private UnityEvent ElevatorMove;

    [SerializeField] private UnityEvent ElevatorStope;

    private ElevatorTasks _elevatorTasks;

    private Transform _elevatorTransform;

    private int _floorsCount;

    private bool _isMove;

    private int _direction;

    private int _targetFloorLevel;

    private void Awake()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SubscribeOnElevatorCall(OnElevatorCall);

            if (_floorsCount < _buttons[i].FloorLevel)
                _floorsCount = _buttons[i].FloorLevel;
        }
        _elevatorTransform = _elevator.transform;

        _elevator.Init(this);

        _elevatorTasks = new ElevatorTasks(++_floorsCount);
    }

    private float HeightPosition => _targetFloorLevel * FloorHeight;

    public bool IsSelectedFloor => _elevatorTransform.localPosition.y >= HeightPosition - 0.01f && _elevatorTransform.localPosition.y <= HeightPosition + 0.01f;

    private void FixedUpdate()
    {
        if (_isMove)
        {
            _elevatorTransform.localPosition += Vector3.up * _direction * Speed * Time.fixedDeltaTime;

            if (IsSelectedFloor)
            {
                ElevatorStop();
            }
        }
    }

    public void CallForNextFloor()
    {
        if (_elevatorTasks.IsHasAnyCalls())
        {
            OnElevatorCall(_elevatorTasks.GetNextFloor());
        }
    }

    private void OnElevatorCall(int floorLevel)
    {
        _elevatorTasks.AddFloorCall(floorLevel);

        _targetFloorLevel = GetNextFloorTargetByElevatorDirection();

        if (_isMove == false && _elevator.IsDoorProcessing == false)
        {
            PrepareToMove();
        }
    }

    private int GetNextFloorTargetByElevatorDirection()
    {
        int target = _elevatorTasks.GetNextFloor();

        if (_isMove && ((_direction == 1 && target < _elevatorTasks.FloorIndex) || (_direction == -1 && target > _elevatorTasks.FloorIndex)))
        {
            // Do not change the floor level target
            target = _targetFloorLevel;
        }
        else if (_isMove && (target == _elevatorTasks.FloorIndex))
        {
            // Do not change the floor level target
            target = _targetFloorLevel;
        }
        else
        {
            target = _elevatorTasks.GetNextFloor();
        }

        return target;
    }

    private void PrepareToMove()
    {
        if (IsSelectedFloor && _elevator.IsDoorClosed)
        {
            _elevatorTasks.RemoveFloorCall(_targetFloorLevel);

            _elevator.OpenTheDoor();
        }
        else if (IsSelectedFloor == false && _elevator.IsDoorOpened)
        {
            _elevator.CloseTheDoorsBeforeMove();
        }
        else if (_elevator.IsDoorClosed)
        {
            StartMove();
        }
    }

    private void StartMove()
    {
        _direction = HeightPosition < _elevatorTransform.localPosition.y ? -1 : 1;

        ElevatorMove?.Invoke();

        _isMove = true;
    }

    private void ElevatorStop()
    {
        _isMove = false;

        _elevatorTasks.RemoveFloorCall(_targetFloorLevel);

        _elevatorTasks.SetActualFloorLevel(_targetFloorLevel);

        _elevator.OpenTheDoor();

        ElevatorStope?.Invoke();
    }
}
