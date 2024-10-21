using UnityEngine;

namespace Assets._Project.CodeBase
{
    public class CharacterAnimation
    {
        private const string IsIdling = "IsIdling";
        private const string IsWalking = "Walking";

        private Animator _animator;

        public CharacterAnimation(Animator anim) =>
            _animator = anim;

        public void StartIdle() => _animator.SetBool(IsIdling, true);
        public void StopIdle() => _animator.SetBool(IsIdling, false);

        public void StartRunning() => _animator.SetBool(IsWalking, true);
        public void StopRunning() => _animator.SetBool(IsWalking, false);

        public void HandleAnimations(Vector2 moveDirection)
        {
            if (moveDirection != Vector2.zero)
            {
                StartRunning();
                StopIdle();
            }
            else
            {
                StopRunning();
                StartIdle();
            }
        }
    }
}
