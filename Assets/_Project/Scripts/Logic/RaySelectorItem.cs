using Assets._Project.Config;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets._Project.CodeBase
{
    public class RaySelectorItem
    {
        private Camera _mainCamera;
        private Image _descriptionToPickUp;
        private CharacterInput _input;
        private RaySelectorItemData _selectorItemData;

        private List<PickingItem> _movableObjects;
        private PickingItem _selectedObject;

        private bool _isObjectBeingHeld;
        private float _objectDistanceFromCamera;

        public RaySelectorItem(RaySelectorItemData selectorItemData, List<PickingItem> movableObjects, Image descriptionToPickUp, CharacterInput input)
        {
            _selectorItemData = selectorItemData;
            _movableObjects = movableObjects;
            _mainCamera = Camera.main;
            _descriptionToPickUp = descriptionToPickUp;
            _input = input;

            _input.PickUp.PickUp.performed += ToggleObjectState;
        }

        public void Update()
        {
            if (_isObjectBeingHeld)
                MoveObjectWithMouse();
            else
                UpdateRaycast();
        }

        private void UpdateRaycast()
        {
            if (TryGetRaycastHit(out PickingItem hitObject))
                SetSelectedObject(hitObject);
            else
                ClearSelection();
        }

        private bool TryGetRaycastHit(out PickingItem hitObject)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, _selectorItemData.RayDistance, _selectorItemData.InteractableLayerMask))
            {
                hitObject = hit.collider.GetComponent<PickingItem>();
                return _movableObjects.Contains(hitObject);
            }

            hitObject = null;
            return false;
        }

        private void SetSelectedObject(PickingItem hitObject)
        {
            _selectedObject = hitObject;
            SetActiveDescription(true);
        }

        private void ClearSelection()
        {
            _selectedObject = null;
            SetActiveDescription(false);
        }

        private void ToggleObjectState(InputAction.CallbackContext context)
        {
            _isObjectBeingHeld = !_isObjectBeingHeld;

            if (_isObjectBeingHeld && _selectedObject != null)
                StartHoldingObject();
            else
                StopHoldingObject();
        }

        private void StartHoldingObject()
        {
            _selectedObject.SetKinematicState(true);
            _objectDistanceFromCamera = Vector3.Distance(_mainCamera.transform.position, _selectedObject.transform.position);
        }

        private void StopHoldingObject()
        {
            if (_selectedObject != null)
            {
                _selectedObject.SetKinematicState(false);
                _selectedObject = null;
            }
        }

        private void MoveObjectWithMouse()
        {
            if (_selectedObject == null)
                return;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            _selectedObject.transform.position = ray.GetPoint(_objectDistanceFromCamera);
        }

        private void SetActiveDescription(bool isActive) =>
            _descriptionToPickUp.gameObject.SetActive(isActive);
    }
}
