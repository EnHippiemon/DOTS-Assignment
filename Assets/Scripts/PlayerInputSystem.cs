using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
[BurstCompile]
public partial class PlayerInputSystem : SystemBase
{
    private GameInput _inputActions;
    private Entity _player;

    [BurstCompile]
    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        _inputActions = new GameInput();
    }

    [BurstCompile]
    protected override void OnStartRunning()
    {
        _inputActions.Enable();
        _inputActions.GamePlay.Shoot.performed += OnShoot;
        _player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    [BurstCompile]
    private void OnShoot(InputAction.CallbackContext obj)
    {
        if (!SystemAPI.Exists(_player)) return;
        
        SystemAPI.SetComponentEnabled<FireProjectileTag>(_player, true);
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        var moveInput = _inputActions.GamePlay.Move.ReadValue<Vector2>();
        
        SystemAPI.SetSingleton(new PlayerMoveInput { Value = moveInput });
    }

    [BurstCompile]
    protected override void OnStopRunning()
    {
        _inputActions.Disable();
        _player = Entity.Null;
    }
}