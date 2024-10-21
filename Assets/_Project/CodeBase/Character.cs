using Assets._Project.Config;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterMove _playerMover;

    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    public CharacterInput CharacterInput { get; private set; }
    public CharacterData CharacterData { get; private set; }

    public void Construct( CharacterData characterData, CharacterMove playerMover, CharacterInput input)
    {
        CharacterData = characterData;
        _playerMover = playerMover;
        CharacterInput = input;

        _playerMover.Construct(this);
    }

    private void OnEnable() =>
        CharacterInput.Enable();

    private void OnDisable() =>
        CharacterInput.Disable();
}

