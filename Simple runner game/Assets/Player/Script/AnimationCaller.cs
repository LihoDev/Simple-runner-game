using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationCaller : MonoBehaviour
    {
        [SerializeField] private string _parameterStartRun = "StartRun";
        [SerializeField] private string _parameterDrink = "drink";
        [SerializeField] private string _parameterBendOverStart = "BendOver_Start";
        [SerializeField] private string _parameterColiision = "Coliision";
        [SerializeField] private string _parameterCollisionLeft = "Collision_Left";
        [SerializeField] private string _parameterCollisionRight = "Collision_Right";
        [SerializeField] private string _parameterStopAction = "StopAction";
        [SerializeField] private string _parameterMoveLeft = "Move_Left";
        [SerializeField] private string _parameterMoveRight = "Move_Right";
        [SerializeField] private string _parameterJump = "Jump";
        [SerializeField] private string _parameterDown = "Down";
        private Animator _animator;

        public void CallStartRun() => _animator.SetTrigger(_parameterStartRun);

        public void CallDrink() => _animator.SetTrigger(_parameterDrink);

        public void CallBendOverStart() => _animator.SetTrigger(_parameterBendOverStart);

        public void CallColiision() => _animator.SetTrigger(_parameterColiision);

        public void CallCollisionLeft() => _animator.SetTrigger(_parameterCollisionLeft);

        public void CallCollisionRight() => _animator.SetTrigger(_parameterCollisionRight);

        public void CallStopAction() => _animator.SetTrigger(_parameterStopAction);

        public void ResetStopAction() => _animator.ResetTrigger(_parameterStopAction);

        public void CallMoveLeft() => _animator.SetTrigger(_parameterMoveLeft);

        public void CallMoveRight() => _animator.SetTrigger(_parameterMoveRight);

        public void CallJump() => _animator.SetTrigger(_parameterJump);

        public void CallDown() => _animator.SetTrigger(_parameterDown);

        private void Awake() => _animator = GetComponent<Animator>();
    }
}
