using UnityEngine;

namespace StateMachineSystem
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;
        
        [HideInInspector] public bool ShouldTransit = false;
        
        public State TargetState => _targetState;

        public virtual void Tick(float deltaTime) { }
        
        public virtual void FixedTick(float fixedDeltaTime) { }
    }
}
