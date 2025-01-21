using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [Serializable]
    public class CameraRotateData
    {
        [Header("Mobile")]
        [SerializeField] private float _rotateSpeedMobileX;
        [SerializeField] private float _rotateSpeedMobileY;
        [Header("Keyboard")]
        [SerializeField] public float _rotateSpeedKeyboardX;
        [SerializeField] public float _rotateSpeedKeyboardY;
        [SerializeField] public float _minZoomDistanceMidle;
        [SerializeField] public float _maxZoomDistanceMidle;
        [SerializeField] public float _minZoomDistanceBottom;
        [SerializeField] public float _maxZoomDistanceBottom;
        [SerializeField] public float _zoomStep;

        public float RotateSpeedMobileX => _rotateSpeedMobileX;
        public float RotateSpeedMobileY => _rotateSpeedMobileY;
        public float RotateSpeedKeyboardX => _rotateSpeedKeyboardX;
        public float RotateSpeedKeyboardY => _rotateSpeedKeyboardY;
        public float MinZoomDistanceMidle => _minZoomDistanceMidle;
        public float MaxZoomDistanceMidle => _maxZoomDistanceMidle;
        public float MinZoomDistanceBottom => _minZoomDistanceBottom;
        public float MaxZoomDistanceBottom => _maxZoomDistanceBottom;
        public float ZoomStep => _zoomStep;
    }
}
