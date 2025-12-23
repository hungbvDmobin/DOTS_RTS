using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

  
public class HealthBarCanvasAuthoring : MonoBehaviour
{
    public GameObject healthBarCanvasPrefab;
    public class Baker : Baker<HealthBarCanvasAuthoring>
    {
        public override void Bake(HealthBarCanvasAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HealthBarCanvas
            {
                prefabs = authoring.healthBarCanvasPrefab
            });
        }
    }
}

public struct HealthBarCanvas: IComponentData
{
    public UnityObjectRef<GameObject> prefabs;
}

public struct HealthBarCanvasUI : ICleanupComponentData
{
    public UnityObjectRef<Transform> CanvasTransform;
    public UnityObjectRef<Slider> HealthBarSlider;
    public UnityObjectRef<TextMeshProUGUI> TextMesh;
}