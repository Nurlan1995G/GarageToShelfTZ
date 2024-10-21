using Assets._Project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.CodeBase
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private Character _character;
        [SerializeField] private CharacterMove _characterMove;
        [SerializeField] private CameraRotater _cameraRotater;
        [SerializeField] private List<PickingItem> _pickings;

        private void Awake()
        {
            CharacterInput input = new();
            CharacterAnimation characterAnimation = new(_character.Animator);
            RaySelectorItem raySelectorItem = new RaySelectorItem(_gameConfig.RaySelectorItemData, _pickings);

            _cameraRotater.Construct(_gameConfig.CameraRotateData, input);
            _character.Construct(_gameConfig.CharacterData, _characterMove, input, characterAnimation
                ,raySelectorItem);
            InitMobile();
        }

        private void InitMobile()
        {
            if (Application.isMobilePlatform)
                _cameraRotater.InitializeMobile();
            else
                _cameraRotater.InitializeKeyboard();
        }
    }
}
