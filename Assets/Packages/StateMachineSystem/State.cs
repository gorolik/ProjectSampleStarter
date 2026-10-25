using UnityEngine;

namespace StateMachineSystem
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private Transition[] _transitions;

        public Transition[] Transitions => _transitions;

        public virtual void Tick(float deltaTime) { }
        
        public virtual void FixedTick(float fixedDeltaTime) { }

        public virtual void Enter()
        {
            gameObject.SetActive(true);

            foreach (Transition transition in _transitions) 
                transition.gameObject.SetActive(true);
        }
        
        public virtual void Exit()
        {
            gameObject.SetActive(false);

            foreach (Transition transition in _transitions) 
                transition.gameObject.SetActive(false);
        }
    }
}
