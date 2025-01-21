using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [Serializable]
    public class RaySelectorItemData
    {
        [SerializeField] private float _rayDistance = 10f;
        [SerializeField] private LayerMask _interactableLayerMask;

        public float RayDistance => _rayDistance;
        public LayerMask InteractableLayerMask => _interactableLayerMask;
    }
}
