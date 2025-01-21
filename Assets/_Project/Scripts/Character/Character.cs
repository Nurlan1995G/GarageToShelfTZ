using Assets._Project.Config;
using UnityEngine;

namespace Assets._Project.CodeBase
{
    public class Character : MonoBehaviour
    {
        private CharacterMove _playerMover;
        private RaySelectorItem _raySelectorItem;

        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        public CharacterInput CharacterInput { get; private set; }
        public CharacterData CharacterData { get; private set; }

        public void Construct(CharacterData characterData, CharacterMove playerMover, CharacterInput input, CharacterAnimation characterAnimation, RaySelectorItem raySelectorItem)
        {
            CharacterData = characterData;
            _playerMover = playerMover;
            CharacterInput = input;
            _raySelectorItem = raySelectorItem;

            _playerMover.Construct(this, characterAnimation);
        }

        private void OnEnable() =>
            CharacterInput.Enable();

        private void Update() => 
            _raySelectorItem.Update();

        private void OnDisable() =>
            CharacterInput.Disable();
    }
}

