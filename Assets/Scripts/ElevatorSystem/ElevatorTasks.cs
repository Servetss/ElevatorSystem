using UnityEngine;

[System.Serializable]
public class ElevatorTasks
{
    [SerializeField] private bool[] _floorsCall;

    private int _loopDirection;

    public ElevatorTasks(int maxFloor)
    {
        _floorsCall = new bool[maxFloor];

        _loopDirection = 1;

        FloorIndex = 0;
    }

    public int FloorIndex { get; private set; }

    public bool IsHasAnyCalls()
    {
        for (int i = 0; i < _floorsCall.Length; i++)
        {
            if (_floorsCall[i]) return true;
        }

        return false;
    }

    public void AddFloorCall(int floorLevel)
    {
        _floorsCall[floorLevel] = true;
    }

    public void RemoveFloorCall(int floorLevel)
    {
        _floorsCall[floorLevel] = false;
    }

    public void SetActualFloorLevel(int floorLevel)
    {
        FloorIndex = floorLevel;
    }

    public int GetNextFloor()
    {
        int protectedCount = _floorsCall.Length * 3;

        int indexProtected = 0;

        for (int i = FloorIndex; i < _floorsCall.Length; i += _loopDirection)
        {
            LoopDirection(i);

            if (_floorsCall[i])
            {
                return i;
            }

            indexProtected++;

            if (indexProtected >= protectedCount) break;
        }

        return -1;
    }

    private void LoopDirection(int index)
    {
        if (index + _loopDirection == _floorsCall.Length)
            _loopDirection = -1;
        else if (index + _loopDirection == -1)
            _loopDirection = 1;
    }
}
