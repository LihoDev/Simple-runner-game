using ObstacleGenerator;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class ForwardMovement : MonoBehaviour
    {
        public float CurrentSpeed { get; private set; }
        [SerializeField] private float _startSpeed = 10f;
        [SerializeField] private float acceleration = 1.01f;
        [SerializeField] private AnimationCaller _animationCaller;
        [SerializeField] private float _maxDistanceToReset = 900000;
        [SerializeField] private RoadReplacer _roadReplacer;
        [SerializeField] private ObstacleAppointer _obstacleAppointer;
        [SerializeField] private UnityEvent OnResetDistance;

        private void Update()
        {
            transform.Translate(CurrentSpeed * Time.deltaTime * transform.forward);
            CurrentSpeed += acceleration;
            if (transform.position.z > _maxDistanceToReset)
                ResetDistance();
        }

        private void ResetDistance()
        {
            transform.position = Vector3.Scale(transform.position, new Vector3(1, 1, 0));
            _roadReplacer.ResetDistance(_maxDistanceToReset);
            _obstacleAppointer.ResetDistance(_maxDistanceToReset);
            OnResetDistance?.Invoke();
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
