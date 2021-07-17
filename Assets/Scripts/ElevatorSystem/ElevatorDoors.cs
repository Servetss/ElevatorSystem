using System;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorDoors : MonoBehaviour
{
    private const string OpenTheDoorsTrigger = "OpenTheDoors";

    private const string CloseTheDoorsTrigger = "CloseTheDoors";

    private const int OpenDoorDelay = 6;

    [SerializeField] private ElevatorPhotocell _elevatorPhotocell;

    [SerializeField] private UnityEvent DoorsOpening;

    [SerializeField] private UnityEvent DoorsClosing;

    private Animator _doorAnimator;

    private StringFormator _stringFormator;

    private event Action DoorsOpened;

    private event Action DoorsClosed;


    private void Awake()
    {
        if (_elevatorPhotocell == null)
            throw new ArgumentNullException();

        _doorAnimator = GetComponent<Animator>();

        _elevatorPhotocell.SubscribeOnStayInDangerZone(OnPlayerStayInDangerZone);

        _elevatorPhotocell.SubscribeOnGoFromDangerZone(OnPlayerGoFromDangerZone);

        _stringFormator = new StringFormator();
    }

    public bool IsDoorOpen { get; private set; }

    public bool IsOpening { get; private set; }

    public bool IsClosing { get; private set; }

    private string ClosingTheDoorsMethod => _stringFormator.GetMethodName(ClosingTheDoors);

    #region Subscribe on doors events
    public void SubscribeOnDoorOpened(Action method)
    {
        DoorsOpened += method;
    }

    public void SubscribeOnDoorClosed(Action method)
    {
        DoorsClosed += method;
    }
    #endregion

    public void OpeningTheDoors()
    {
        if (IsOpening == false)
        {
            IsOpening = true;

            _doorAnimator.SetTrigger(OpenTheDoorsTrigger);

            StopTimerToCloseTheDoors();

            StartTimerToCloseTheDoors();

            DoorsOpening?.Invoke();
        }
    }

    public void ClosingTheDoors()
    {
        IsClosing = true;

        StopTimerToCloseTheDoors();

        _doorAnimator.SetTrigger(CloseTheDoorsTrigger);

        DoorsClosing?.Invoke();
    }

    #region Timer
    public void StartTimerToCloseTheDoors()
    {
        Invoke(ClosingTheDoorsMethod, OpenDoorDelay);
    }

    public void StopTimerToCloseTheDoors()
    {
        CancelInvoke(ClosingTheDoorsMethod);
    }
    #endregion

    #region Animation events
    private void OnPlayerStayInDangerZone()
    {
        StopTimerToCloseTheDoors();
    }

    private void OnPlayerGoFromDangerZone()
    {
        StartTimerToCloseTheDoors();
    }

    private void OnDoorsOpened()
    {
        IsOpening = false;

        IsDoorOpen = true;

        DoorsOpened?.Invoke();
    }

    private void OnDoorsClosed()
    {
        IsClosing = false;

        IsDoorOpen = false;

        DoorsClosed?.Invoke();
    }
    #endregion
}
