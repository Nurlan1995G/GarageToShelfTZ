using Assets._Project.Config;
using Cinemachine;
using System;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    [SerializeField] private VariableJoystick _variableJoystick;
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

    private CameraRotateData _cameraRotateData;
    private CharacterInput _input;

    private float _currentXRotation;
    private float _currentYRotation;
    private bool _isMobilePlatform;

    private Vector2 _lastDirection;
    private Vector3 _currentMousePosition;

    private Action _rotationCameraAction;

    public void Construct(CameraRotateData cameraRotateData, CharacterInput input)
    {
        _cameraRotateData = cameraRotateData;
        _input = input;

        _input.Enable();
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
        if (_variableJoystick.enabled && _currentMousePosition != UnityEngine.Input.mousePosition)
        {
            _cinemachineFreeLook.m_XAxis.m_InputAxisValue = _variableJoystick.Horizontal;
            _cinemachineFreeLook.m_YAxis.m_InputAxisValue = _variableJoystick.Vertical;
            _currentMousePosition = UnityEngine.Input.mousePosition;
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
}
