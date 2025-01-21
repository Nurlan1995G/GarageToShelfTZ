using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public CharacterData CharacterData { get; private set; }
        [field: SerializeField] public CameraRotateData CameraRotateData { get; private set; }
        [field: SerializeField] public RaySelectorItemData RaySelectorItemData { get; private set; }
    }
}
