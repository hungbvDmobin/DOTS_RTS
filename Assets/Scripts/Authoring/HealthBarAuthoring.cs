using UnityEngine;
using Unity.Entities;

public class HealthBarAuthoring : MonoBehaviour
{
    public GameObject barVisualGameObject;
    public GameObject unitGameObject;
    public class Baker : Baker<HealthBarAuthoring>
    {
        public override void Bake(HealthBarAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HealthBar
            {
                barVisualEntity = GetEntity(authoring.barVisualGameObject, TransformUsageFlags.Dynamic),
                unitEntity = GetEntity(authoring.unitGameObject, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct HealthBar : IComponentData
{
    public Entity barVisualEntity;
    public Entity unitEntity;
}
