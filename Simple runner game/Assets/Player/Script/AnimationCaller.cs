using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationCaller : MonoBehaviour
    {
        [SerializeField] private string _startRun = "StartRun";
        [SerializeField] private string _drink = "Drink";
        [SerializeField] private string _coliision = "Coliision";
        [SerializeField] private string _collisionLeft = "Collision_Left";
        [SerializeField] private string _collisionRight = "Collision_Right";
        [Space]
        [SerializeField] private string _bendOverStart = "BendOver_Start";
        [SerializeField] private string _bendOverStop = "BendOver_Stop";
        [Space]
        [SerializeField] private string _moveLeft = "Move_Left";
        [SerializeField] private string _moveLeftStop = "Move_Left_Stop";
        [Space]
        [SerializeField] private string _moveRight = "Move_Right";
        [SerializeField] private string _moveRightStop = "Move_Right_Stop";
        [Space]
        [SerializeField] private string _jump = "Jump";
        [Space]
        [SerializeField] private string _down = "Down";
        [SerializeField] private string _downStop = "Down_Stop";
        private Animator _animator;

        public void CallStartRun() => _animator.SetTrigger(_startRun);

        public void CallDrink() => _animator.SetTrigger(_drink);

        public void CallColiision() => _animator.SetTrigger(_coliision);

        public void CallCollisionLeft() => _animator.SetTrigger(_collisionLeft);

        public void CallCollisionRight() => _animator.SetTrigger(_collisionRight);

        public void CallBendOverStart() => _animator.SetTrigger(_bendOverStart);

        public void CallBendOverStop() => _animator.SetTrigger(_bendOverStop);

        public void CallMoveLeft() => _animator.SetTrigger(_moveLeft);

        public void CallMoveLeftStop() => _animator.SetTrigger(_moveLeftStop);

        public void CallMoveRight() => _animator.SetTrigger(_moveRight);

        public void CallMoveRightStop() => _animator.SetTrigger(_moveRightStop);

        public void CallJump() => _animator.SetTrigger(_jump);

        public void CallDown() => _animator.SetTrigger(_down);

        public void CallDownStop() => _animator.SetTrigger(_downStop);
        public void ResetDownStop() => _animator.ResetTrigger(_downStop);

        private void Awake() => _animator = GetComponent<Animator>();
    }
}
