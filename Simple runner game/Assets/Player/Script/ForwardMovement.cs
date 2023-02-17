using UnityEngine;

namespace Player
{
    public class ForwardMovement : MonoBehaviour
    {
        public float CurrentSpeed { get; private set; }
        [SerializeField] private float _startSpeed = 10f;
        [SerializeField] private float acceleration = 1.01f;
        [SerializeField] private AnimationCaller _animationCaller;

        private void Update()
        {
            transform.Translate(CurrentSpeed * Time.deltaTime * transform.forward);
            CurrentSpeed += acceleration;
        }

        private void Start()
        {
            _animationCaller.CallStartRun();
        }

        private void Awake()
        {
            CurrentSpeed = _startSpeed;
        }
    }
}
