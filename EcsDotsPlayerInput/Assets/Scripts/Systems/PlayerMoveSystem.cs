using Aspects;
using Unity.Burst;
using Unity.Entities;
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

        //Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        //LocalTransform localTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity); //Write Read
        //MoveData moveData = SystemAPI.GetComponent<MoveData>(playerEntity); //Readonly
        //InputData inputData = SystemAPI.GetComponent<InputData>(playerEntity); //Readonly
        
        new PlayerMoveJob()
        {
            DeltaTime = deltaTime
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float DeltaTime;
    
    [BurstCompile]
    private void Execute(PlayerAspect playerAspect)
    {
       playerAspect.MoveProcess(DeltaTime);
    }
}

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
[UpdateAfter(typeof(PlayerMoveSystem))]
public partial struct PlayerRotationSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new PlayerRotationJob()
        {
            DeltaTime = deltaTime
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct PlayerRotationJob : IJobEntity
{
    public float DeltaTime;
    
    [BurstCompile]
    private void Execute(PlayerAspect playerAspect)
    {
        playerAspect.RotationProcess();
    }
}

//first example
// [BurstCompile]
// public partial struct PlayerMoveJob : IJobEntity
// {
//     public float DeltaTime;
//     
//     [BurstCompile]
//     private void Execute(Entity entity, ref LocalTransform localTransform, in MoveData moveData, in InputData inputData)
//     {
//         var direction = inputData.Direction;
//         localTransform.Position += DeltaTime * moveData.MoveSpeed * direction;
//
//         if (!direction.Equals(float3.zero))
//         {
//             localTransform.Rotation = quaternion.LookRotation(inputData.Direction, new float3(0f,1f,0f));    
//         } 
//     }
// }