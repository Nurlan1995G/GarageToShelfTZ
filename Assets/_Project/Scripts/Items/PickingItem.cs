using UnityEngine;

namespace Assets._Project.CodeBase
{
    public abstract class PickingItem : MonoBehaviour
    {
        [field: SerializeField] protected Rigidbody Rigidbody { get; private set; }

        public void SetKinematicState(bool isKinematic)
        {
            Rigidbody.isKinematic = isKinematic;
        }
    }
}