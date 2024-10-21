using Assets._Project.Config;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Character _character;
    [SerializeField] private CharacterMove _characterMove;
    [SerializeField] private CameraRotater _cameraRotater;

    private void Awake()
    {
        CharacterInput input = new();

        _character.Construct(_gameConfig.CharacterData, _characterMove, input);
        _cameraRotater.Construct(_gameConfig.CameraRotateData, input);
    }
}
