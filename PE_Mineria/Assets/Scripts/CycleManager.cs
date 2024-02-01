using UnityEngine;

public class CycleManager : MonoBehaviour
{
    [Tooltip("Current day since we started playing")]
    [SerializeField] private int _dayCounter = 1;
    [SerializeField] private DayCycle _currentDayCycle = DayCycle.Morning;
    
    
    public delegate void CycleChanged(DayCycle dayCycle);
    public CycleChanged dayCycleChangedEvent;

    public delegate void DayChanged(int currentDay);
    public DayChanged dayChangedEvent;

    private void ChangeCurrentDayCycle()
    {
        if (_currentDayCycle == DayCycle.Night)
        {
            _currentDayCycle = DayCycle.Morning;
        }
        else
        {
            int day = (int)_currentDayCycle;
            DayCycle newCurrentDayCycle = (DayCycle)(day + 1);
            _currentDayCycle = newCurrentDayCycle;
        }
    }
    
    /// <summary>
    /// Button call to advance on the day
    /// </summary>
    public void ChangeCycle()
    {
        ChangeCurrentDayCycle();
        if (dayCycleChangedEvent != null)
            dayCycleChangedEvent.Invoke(_currentDayCycle);

        if (_currentDayCycle != DayCycle.Morning) return;
        if (dayChangedEvent == null) return;
        
        _dayCounter += 1;
        dayChangedEvent.Invoke(_dayCounter);

    }

    public DayCycle CurrentDayCycle => _currentDayCycle;
    public int DayCounter => _dayCounter;
}
