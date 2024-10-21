using Assets._Project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.CodeBase
{
    public class RaySelectorItem
    {
        private Camera _mainCamera;
        private RaySelectorItemData _selectorItemData;

        private List<PickingItem> _movableObjects;
        private PickingItem _selectedObject;

        private bool _isObjectBeingHeld;
        private float _objectDistanceFromCamera;

        public RaySelectorItem(RaySelectorItemData selectorItemData, List<PickingItem> movableObjects)
        {
            _selectorItemData = selectorItemData;
            _movableObjects = movableObjects;
            _mainCamera = Camera.main;
        }

        public void Update()
        {
            HandleRaycast();
            HandleObjectMovement();
        }

        private void HandleRaycast()
        {
            if (_isObjectBeingHeld)
                return;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _selectorItemData.RayDistance, _selectorItemData.InteractableLayerMask))
            {
                PickingItem hitObject = hit.collider.GetComponent<PickingItem>();

                if (_movableObjects.Contains(hitObject))

                    _selectedObject = hitObject;
            }
            else
                _selectedObject = null;
        }

        private void HandleObjectMovement()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_isObjectBeingHeld)
                {
                    _isObjectBeingHeld = false;
                    _selectedObject = null;
                }
                else if (_selectedObject != null)
                {
                    _isObjectBeingHeld = true;
                    _objectDistanceFromCamera = Vector3.Distance(_mainCamera.transform.position, _selectedObject.transform.position);
                }
            }

            if (_isObjectBeingHeld && _selectedObject != null)
                MoveObjectWithMouse();
        }

        private void MoveObjectWithMouse()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 newPosition = ray.GetPoint(_objectDistanceFromCamera);
            _selectedObject.transform.position = newPosition;
        }
    }
}
