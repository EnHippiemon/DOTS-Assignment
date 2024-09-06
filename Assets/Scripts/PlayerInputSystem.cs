using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private GameInput _inputActions;
    private Entity _player;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        _inputActions = new GameInput();
    }

    protected override void OnStartRunning()
    {
        _inputActions.Enable();
        _inputActions.GamePlay.Shoot.performed += OnShoot;
        _player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    private void OnShoot(InputAction.CallbackContext obj)
    {
        if (!SystemAPI.Exists(_player)) return;
        
        SystemAPI.SetComponentEnabled<FireProjectileTag>(_player, true);
    }

    protected override void OnUpdate()
    {
        var moveInput = _inputActions.GamePlay.Move.ReadValue<Vector2>();
        
        SystemAPI.SetSingleton(new PlayerMoveInput { Value = moveInput });
    }

    protected override void OnStopRunning()
    {
        _inputActions.Disable();
        _player = Entity.Null;
    }
}
