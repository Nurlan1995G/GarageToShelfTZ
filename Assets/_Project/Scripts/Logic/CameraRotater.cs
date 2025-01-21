using Assets._Project.Config;
using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.CodeBase
{
    public class CameraRotater : MonoBehaviour
    {
        [SerializeField] private VariableJoystick _variableJoystick;
        [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

        private CameraRotateData _cameraRotateData;
        private CharacterInput _input;

        private float _currentXRotation;
        private float _currentYRotation;
        private bool _isMobilePlatform;

        private Vector3 _currentMousePosition;

        private Action _rotationCameraAction;

        public void Construct(CameraRotateData cameraRotateData, CharacterInput input)
        {
            _cameraRotateData = cameraRotateData;
            _input = input;

            _input.Enable();
            _input.Mouse.MouseScrollWheel.performed += OnTouchMouseScrollWheel;
        }

        private void Update()
        {
            if (!_isMobilePlatform)
            {
                if (_input.Mouse.RightButton.IsPressed())
                    _rotationCameraAction.Invoke();
                else
                    StopCameraRotate();
            }
            else
                _rotationCameraAction.Invoke();
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Mouse.MouseScrollWheel.performed += OnTouchMouseScrollWheel;
        }

        public void InitializeMobile()
        {
            _isMobilePlatform = true;
            _currentXRotation = _cameraRotateData.RotateSpeedMobileX;
            _currentYRotation = _cameraRotateData.RotateSpeedMobileY;
            _rotationCameraAction = HandleTouchInput;

            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = _currentXRotation;
            _cinemachineFreeLook.m_YAxis.m_MaxSpeed = _currentYRotation;
        }

        public void InitializeKeyboard()
        {
            _isMobilePlatform = false;
            _currentXRotation = _cameraRotateData.RotateSpeedKeyboardX;
            _currentYRotation = _cameraRotateData.RotateSpeedKeyboardY;
            _rotationCameraAction = ControlRotation;

            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = _currentXRotation;
            _cinemachineFreeLook.m_YAxis.m_MaxSpeed = _currentYRotation;
        }

        private void ControlRotation()
        {
            if (_variableJoystick.enabled && _currentMousePosition != Input.mousePosition)
            {
                _cinemachineFreeLook.m_XAxis.m_InputAxisValue = _variableJoystick.Horizontal;
                _cinemachineFreeLook.m_YAxis.m_InputAxisValue = _variableJoystick.Vertical;
                _currentMousePosition = Input.mousePosition;
            }
            else
            {
                StopCameraRotate();
            }
        }

        private void StopCameraRotate()
        {
            _cinemachineFreeLook.m_XAxis.m_InputAxisValue = 0;
            _cinemachineFreeLook.m_YAxis.m_InputAxisValue = 0;
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                if (_variableJoystick.enabled)
                {
                    if (IsTouchWithinJoystick(touch1.position) && IsTouchWithinJoystick(touch2.position))
                    {
                        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                        Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                        float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                        float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    }
                }
            }
            else if (Input.touchCount == 1)
            {
                ControlRotation();
            }
        }

        private bool IsTouchWithinJoystick(Vector2 touchPosition)
        {
            RectTransform joystickRect = _variableJoystick.GetComponent<RectTransform>();
            return RectTransformUtility.RectangleContainsScreenPoint(joystickRect, touchPosition, null);
        }

        private void OnTouchMouseScrollWheel(InputAction.CallbackContext context)
        {
            float scrollDelta = context.ReadValue<float>();

            OnTouchZoom(scrollDelta);
        }

        private void OnTouchZoom(float deltaMagnitudeDiff)
        {
            for (int i = 0; i < _cinemachineFreeLook.m_Orbits.Length; i++)
            {
                CinemachineFreeLook.Orbit orbit = _cinemachineFreeLook.m_Orbits[i];

                orbit.m_Radius = Mathf.Clamp(orbit.m_Radius - deltaMagnitudeDiff * _cameraRotateData.ZoomStep, _cameraRotateData.MinZoomDistanceBottom, _cameraRotateData.MaxZoomDistanceBottom);

                if (i == 1)
                {
                    orbit.m_Height = Mathf.Clamp(orbit.m_Height - deltaMagnitudeDiff * 0.005f, _cameraRotateData.MinZoomDistanceMidle, _cameraRotateData.MaxZoomDistanceMidle);
                }

                if (i == 2)
                {
                    orbit.m_Height = Mathf.Clamp(orbit.m_Height - deltaMagnitudeDiff * _cameraRotateData.ZoomStep, _cameraRotateData.MinZoomDistanceBottom, _cameraRotateData.MaxZoomDistanceBottom);
                }

                _cinemachineFreeLook.m_Orbits[i] = orbit;
            }
        }
    }
}
