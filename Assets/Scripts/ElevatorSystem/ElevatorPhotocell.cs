using System;
using UnityEngine;

public class ElevatorPhotocell : MonoBehaviour
{
    public event Action PlayerStayInDangerZone;

    public event Action PlayerGoFromDangerZone;

    private RaycastHit _hit;

    private bool _savedIsPlayerOnDangerZone;

    public bool IsPlayerInDangerZone { get; private set; }

    #region Subscribe on events
    public void SubscribeOnStayInDangerZone(Action method)
    {
        PlayerStayInDangerZone += method;
    }

    public void SubscribeOnGoFromDangerZone(Action method)
    {
        PlayerGoFromDangerZone += method;
    }
    #endregion


    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward * 5, out _hit, 5))
        {
            IsPlayerInDangerZone = _hit.collider.GetComponent<Movement>();

            if (_savedIsPlayerOnDangerZone != IsPlayerInDangerZone)
            {
                _savedIsPlayerOnDangerZone = IsPlayerInDangerZone;

                if (IsPlayerInDangerZone) PlayerStayInDangerZone?.Invoke();
                else PlayerGoFromDangerZone?.Invoke();
            }
        }
    }
}
