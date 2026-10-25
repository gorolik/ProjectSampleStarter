using UnityEngine;

namespace StateMachineSystem
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State _initialState;
        
        private State _currentState;

        public void Start() => 
            EnterToState(_initialState);

        private void Update()
        {
            if (_currentState == null) 
                return;

            if (TryExecuteTransitions()) 
                return;

            _currentState.Tick(Time.deltaTime);
            
            foreach (Transition currentStateTransition in _currentState.Transitions) 
                currentStateTransition.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_currentState == null) 
                return;

            if (TryExecuteTransitions()) 
                return;

            _currentState.FixedTick(Time.fixedDeltaTime);
            
            foreach (Transition currentStateTransition in _currentState.Transitions) 
                currentStateTransition.FixedTick(Time.fixedDeltaTime);
        }

        private bool TryExecuteTransitions()
        {
            foreach (Transition transition in _currentState.Transitions)
            {
                if (transition.ShouldTransit)
                {
                    transition.ShouldTransit = false;
                    EnterToState(transition.TargetState);
                    return true;
                }
            }
            
            return false;
        }

        public void EnterToState(State state)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = state;
            state.Enter();
        }
    }
}
