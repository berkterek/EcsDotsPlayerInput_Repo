using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
        state.RequireForUpdate<MoveData>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        LocalTransform localTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
        MoveData moveData = SystemAPI.GetComponent<MoveData>(playerEntity);
        InputData inputData = SystemAPI.GetComponent<InputData>(playerEntity);

        var direction = inputData.Direction;
        localTransform.Position += deltaTime * moveData.MoveSpeed * direction;

        if (!direction.Equals(float3.zero))
        {
            localTransform.Rotation = quaternion.LookRotation(inputData.Direction, new float3(0f,1f,0f));    
        } 

        SystemAPI.SetComponent(playerEntity, localTransform);
    }
}