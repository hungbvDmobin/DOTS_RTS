using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

partial struct HealthBarSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Vector3 cameraForward = Vector3.zero;
        if (Camera.main != null)
            cameraForward = Camera.main.transform.forward;

        foreach ((
            RefRW<LocalTransform> localTransform,
            RefRO<HealthBar> healthBar)
            in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRO<HealthBar>>()
            ){

            LocalTransform parentLocalTransform = SystemAPI.GetComponent<LocalTransform>(healthBar.ValueRO.unitEntity);
            localTransform.ValueRW.Rotation = parentLocalTransform.InverseTransformRotation( quaternion.LookRotation(cameraForward, math.up()));


        }
    }

   
}
