using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    GameInputActions _input;
    
    protected override void OnCreate()
    {
        _input = new GameInputActions();
        RequireForUpdate<InputData>();
    }

    protected override void OnStartRunning()
    {
        _input.Enable();
    }

    protected override void OnStopRunning()
    {
        _input.Disable();
    }

    protected override void OnUpdate()
    {
        var inputResult = _input.Player.Move.ReadValue<Vector2>();
        float3 direction = new float3(inputResult.x, 0f, inputResult.y);
        
        SystemAPI.SetSingleton(new InputData()
        {
            
            Direction = direction
        });
    }
}