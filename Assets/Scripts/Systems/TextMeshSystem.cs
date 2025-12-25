using System;
using TMPro;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct TextMeshSystem : ISystem
{


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
        foreach (var (textMeshPrefab, entity) in SystemAPI.Query<TextMesh>().WithNone<TextMeshControl>().WithEntityAccess())
        {
            var texmeshObject = UnityEngine.Object.Instantiate(textMeshPrefab.prefabs.Value);
            ecb.AddComponent(entity, new TextMeshControl
            {
                TextMeshTransform = texmeshObject.transform,
                TextMeshText = texmeshObject.GetComponent<TextMeshPro>()
            });
        }



        foreach (var (playerPostion, texmeshObject) in SystemAPI.Query<LocalToWorld, TextMeshControl>())
        {
            texmeshObject.TextMeshTransform.Value.position = playerPostion.Position + new float3(0, 3, 0);
            texmeshObject.TextMeshTransform.Value.rotation = playerPostion.Rotation;
            texmeshObject.TextMeshText.Value.text = MouseWorldPosition.Instance.GetIndex.ToString();
        }

        ecb.Playback(state.EntityManager);
    }


}
