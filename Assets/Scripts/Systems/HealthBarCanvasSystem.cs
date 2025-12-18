using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UI;

partial struct HealthBarCanvasSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
        foreach (var (uiPrefab, entity) in SystemAPI.Query<HealthBarCanvas>().WithNone<HealthBarCanvasUI>().WithEntityAccess())
        {
            var healtBarObject = Object.Instantiate(uiPrefab.prefabs.Value);
            ecb.AddComponent(entity, new HealthBarCanvasUI
            {
                CanvasTransform = healtBarObject.transform,
                HealthBarSlider = healtBarObject.GetComponentInChildren<Slider>()
            });
        }

        foreach (var (playerPostion, healthBarCanvas) in SystemAPI.Query<LocalToWorld, HealthBarCanvasUI>())
        {
            healthBarCanvas.CanvasTransform.Value.position = playerPostion.Position;
            healthBarCanvas.HealthBarSlider.Value.value = 0.5f;
        }

        ecb.Playback(state.EntityManager);
    }
}
