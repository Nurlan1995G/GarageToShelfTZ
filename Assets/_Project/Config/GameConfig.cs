using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config")]
    public class GameConfig : ScriptableObject
    {
        public CharacterData CharacterData;
        public CameraRotateData CameraRotateData;
        public RaySelectorItemData RaySelectorItemData;
    }

    [Serializable]
    public class RaySelectorItemData
    {
        public float RayDistance = 10f;
        public LayerMask InteractableLayerMask;
    }

    [Serializable]
    public class CharacterData
    {
        public float MoveSpeed;
        public float RotateSpeed;
    }

    [Serializable]
    public class CameraRotateData
    {
        [Header("Mobile")]
        public float RotateSpeedMobileX;
        public float RotateSpeedMobileY;
        [Header("Keyboard")]
        public float RotateSpeedKeyboardX;
        public float RotateSpeedKeyboardY;
        public float MinZoomDistanceMidle;
        public float MaxZoomDistanceMidle;
        public float MinZoomDistanceBottom;
        public float MaxZoomDistanceBottom;
    }
}
