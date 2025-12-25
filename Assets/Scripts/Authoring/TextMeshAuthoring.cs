using TMPro;
using Unity.Entities;
using UnityEngine;

public class TextMeshAuthoring : MonoBehaviour
{
    public GameObject textMeshPrefab;

    public class Baker : Baker<TextMeshAuthoring>
    {
        public override void Bake(TextMeshAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TextMesh
            {
                prefabs = authoring.textMeshPrefab
            });
        }
    }
}

public struct TextMesh : IComponentData
{
    public UnityObjectRef<GameObject> prefabs;
}

public struct TextMeshControl : ICleanupComponentData
{
    public UnityObjectRef<Transform> TextMeshTransform;
    public UnityObjectRef<TextMeshPro> TextMeshText;

}
