using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRO<PlayerTag> _playerTagRO;
        readonly RefRO<InputData> _inputDataRO;
        readonly RefRO<MoveData> _moveDataRO;
        readonly RefRW<LocalTransform> _localTransformRW;

        public void MoveProcess(float deltaTime)
        {
            var direction = _inputDataRO.ValueRO.Direction;
            _localTransformRW.ValueRW.Position += deltaTime * _moveDataRO.ValueRO.MoveSpeed * direction;
        }

        public void RotationProcess()
        {
            var direction = _inputDataRO.ValueRO.Direction;
            if (!direction.Equals(float3.zero))
            {
                _localTransformRW.ValueRW.Rotation = quaternion.LookRotation(direction, new float3(0f,1f,0f));    
            }
        }
    }
}