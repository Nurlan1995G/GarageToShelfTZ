using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [Serializable]
    public class CharacterData
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        public float MoveSpeed => _moveSpeed;
        public float RotateSpeed => _rotateSpeed;
    }
}
