using UnityEngine;
using System;

public class Elevator : MonoBehaviour
{
    [SerializeField] private ElevatorDoors _elevatorDoor;

    private ElevatorEngine _elevatorEngine;
    
    private void Awake()
    {
        if (_elevatorDoor == null)
            throw new ArgumentNullException();

        _elevatorDoor.SubscribeOnDoorOpened(OnDoorsOpened);

        _elevatorDoor.SubscribeOnDoorClosed(OnDoorsClosed);
    }

    public bool IsDoorOpened => _elevatorDoor.IsDoorOpen;

    public bool IsDoorClosed => _elevatorDoor.IsDoorOpen == false;

    public bool IsDoorProcessing => _elevatorDoor.IsOpening || _elevatorDoor.IsClosing;

    public void Init(ElevatorEngine elevatorEngine)
    {
        _elevatorEngine = elevatorEngine;
    }

    public void OpenTheDoor()
    {
        _elevatorDoor.OpeningTheDoors();
    }

    public void CloseTheDoorsBeforeMove()
    {
        _elevatorDoor.ClosingTheDoors();
    }

    #region Events
    private void OnDoorsOpened()
    {
        _elevatorDoor.StartTimerToCloseTheDoors();
    }

    private void OnDoorsClosed()
    {
        _elevatorEngine.CallForNextFloor();
    }
    #endregion
}
