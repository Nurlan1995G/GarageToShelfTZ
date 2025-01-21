using Assets._Project.Config;
using UnityEngine;

namespace Assets._Project.CodeBase
{
    public class CharacterMove : MonoBehaviour
    {
        private Character _character;
        private CharacterData _playerData;
        private CharacterAnimation _characterAnimation;

        private float _currentSpeed;
        private Camera _camera;

        public void Construct(Character player, CharacterAnimation characterAnimation)
        {
            _character = player;
            _playerData = _character.CharacterData;
            _currentSpeed = _playerData.MoveSpeed;
            _characterAnimation = characterAnimation;

            _camera = Camera.main;
        }

        private void Update()
        {
            Vector2 moveDirection = _character.CharacterInput.Character.Move.ReadValue<Vector2>();

            Move(moveDirection);
            _characterAnimation.HandleAnimations(moveDirection);
        }

        private void Move(Vector2 direction)
        {
            Vector3 newDirection = new Vector3(direction.x, 0, direction.y);
            Quaternion cameraRotationY = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);

            MoveCharacter(newDirection, cameraRotationY);
            RotateCharacter(newDirection, cameraRotationY);
        }

        private void MoveCharacter(Vector3 moveDirection, Quaternion cameraRotation)
        {
            Vector3 finalDirection = (cameraRotation * moveDirection).normalized;
            _character.CharacterController.Move(finalDirection * _currentSpeed * Time.deltaTime);
        }

        private void RotateCharacter(Vector3 moveDirection, Quaternion cameraRotation)
        {
            if (Vector3.Angle(transform.forward, moveDirection) > 0)
            {
                Vector3 finalDirection = (cameraRotation * moveDirection).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(finalDirection);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _playerData.RotateSpeed * Time.deltaTime);
            }
        }
    }
}

