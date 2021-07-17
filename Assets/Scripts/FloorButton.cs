using System;
using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    private const string ButtonClickTrigger = "ButtonClick";

    private event Action<int> ElevatorCall;

    [SerializeField] private int _floorLevel;

    [SerializeField] private UnityEvent ButtonClick;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public int FloorLevel { get => _floorLevel; }

    public void SubscribeOnElevatorCall(Action<int> callMethod)
    {
        ElevatorCall += callMethod;
    }

    public void OnButtonClick()
    {
        _animator.SetTrigger(ButtonClickTrigger);

        ElevatorCall?.Invoke(_floorLevel);

        ButtonClick?.Invoke();
    }
}
