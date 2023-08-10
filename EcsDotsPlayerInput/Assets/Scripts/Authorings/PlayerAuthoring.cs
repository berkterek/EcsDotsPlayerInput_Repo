using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed = 5f;
}

public class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent<InputData>(entity);
        AddComponent<PlayerTag>(entity);

        AddComponent(entity, new MoveData()
        {
            MoveSpeed = authoring.MoveSpeed
        });
    }
}
