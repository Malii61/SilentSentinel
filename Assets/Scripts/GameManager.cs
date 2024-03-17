using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Initialize,
        
        GameStarted,
        FirstRuinCleaned,
        ElectricShutDown,
        FirsAidKitTaken,
        FirstAidKitGiven,
        EnteredFoodPlace,
        FireStopped,
        GameFinished,
    }
    public static GameManager Instance { get; private set; }
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    private State _state = State.Initialize;
    public class OnStateChangedEventArgs
    {
        public State CurrentState;
    }
    private void Awake()
    {
        Instance = this;
    }
    public void ChangeState(State state)
    {
        Debug.Log("change state");
        if (_state == state || _state == State.GameFinished) { return; } 

        _state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { CurrentState = state });
    }
}
