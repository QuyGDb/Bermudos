using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[DisallowMultipleComponent]
public class PlayerControl : MonoBehaviour
{
    private Player player;
    private MovementByVelocityEvent movementByVelocityEvent;
    private InputSystem_Actions inputSystem_Actions;
    private InputAction move;
    private InputAction fire;
    private InputAction rightClick;
    private InputAction spaceClick;
    private InputAction beam;
    private InputAction openInventory;
    private InputAction useItemAction;
    private InputAction restAction;
    private BeamByPlayer beamByPlayer;
    private InventoryManager inventoryManager;
    private ItemConsumer useItem;
    private Vector2 direction;
    private Vector2 lastMoveDirection;
    private Vector2 previousLastDirection;
    private float dashCooldownTimer;
    private float bashCooldownTimer;

    private void Awake()
    {
        // Load components
        player = GetComponent<Player>();
        beamByPlayer = GetComponent<BeamByPlayer>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();

        inventoryManager = GetComponent<InventoryManager>();
        useItem = GetComponent<ItemConsumer>();
        // Initialize input actions
        inputSystem_Actions = new InputSystem_Actions();

    }
    private void OnEnable()
    {
        move = inputSystem_Actions.Player.Move;
        fire = inputSystem_Actions.Player.Fire;
        beam = inputSystem_Actions.Player.Beam;
        rightClick = inputSystem_Actions.Player.RightClick;
        spaceClick = inputSystem_Actions.Player.SpaceClick;
        openInventory = inputSystem_Actions.UI.OpenInventory;
        useItemAction = inputSystem_Actions.UI.UseItem;
        restAction = inputSystem_Actions.Player.Rest;
        move.Enable();
        fire.Enable();
        beam.Enable();
        rightClick.Enable();
        spaceClick.Enable();
        openInventory.Enable();
        useItemAction.Enable();
        restAction.Enable();
        fire.performed += OnFireClick;
        beam.performed += ctx => beamByPlayer.Beam();
        rightClick.performed += OnRightMouseClick;
        rightClick.canceled += ctx => player.bashEvent.CallOnBashEvent(BashState.ReleaseBash);
        spaceClick.performed += OnSpaceClick;
        openInventory.performed += ctx => inventoryManager.ToggleInventory();
        useItemAction.performed += ctx => useItem.UseItemFromHotBar();
        restAction.performed += ctx => StaticEventHandler.CallPressRestEvent();

    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        beam.Disable();
        rightClick.Disable();
        spaceClick.Disable();
        openInventory.Disable();
        useItemAction.Disable();
        restAction.Disable();
        fire.performed -= OnFireClick;
        beam.performed -= ctx => beamByPlayer.Beam();
        rightClick.performed -= OnRightMouseClick;
        rightClick.canceled -= ctx => player.bashEvent.CallOnBashEvent(BashState.ReleaseBash);
        spaceClick.performed -= OnSpaceClick;
        openInventory.performed -= ctx => inventoryManager.ToggleInventory();
        useItemAction.performed -= ctx => useItem.UseItemFromHotBar();
        restAction.performed -= ctx => StaticEventHandler.CallPressRestEvent();
    }


    private void OnRightMouseClick(InputAction.CallbackContext ctx)
    {
        if (Time.unscaledTime > bashCooldownTimer)
        {
            bashCooldownTimer = Time.unscaledTime + Settings.bashCooldown;
            player.bashEvent.CallOnBashEvent(BashState.ActiveBash);
            StaticEventHandler.CallTriggerBashEvent();
        }
    }

    private void OnSpaceClick(InputAction.CallbackContext ctx)
    {
        // Play dash animation based on direction
        if (direction != Vector2.zero && Time.time > dashCooldownTimer)
        {
            dashCooldownTimer = Time.time + Settings.dashCooldown;
            player.dashEvent.CallDashEvent(direction);
            StaticEventHandler.CallTriggerDashEvent();
        }
    }

    private void OnFireClick(InputAction.CallbackContext ctx)
    {
        player.attackEvent.
            CallAttackEvent();
    }

    private void Update()
    {
        MovementInput();

        if (rightClick.ReadValue<float>() > 0)
        {
            player.bashEvent.CallOnBashEvent(BashState.DuringBash);
        }

    }

    /// <summary>
    /// Player movement input
    /// </summary>
    private void MovementInput()
    {

        Vector2 currentMoveInput = move.ReadValue<Vector2>();
        if ((currentMoveInput.x == 0 && currentMoveInput.y != 0 || currentMoveInput.x != 0 && currentMoveInput.y == 0) && (direction.x != 0 && direction.y != 0))
        {
            lastMoveDirection = direction; //ko thể lưu được tiếp direction cuối trước khi thả phím vào currentMoveInput được vì ngay frame sau nó sẽ đọc giá trị mới nên là cần một biến phụ lastdir để lưu
        }
        // Create a direction vector based on the input
        direction = move.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            if (fire.ReadValue<float>() == 0)
            {
                if (direction == Vector2.up)
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.Up);
                }
                else if (direction == Vector2.down)
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.Down);
                }
                else if (direction == Vector2.left)
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.Left);
                }
                else if (direction == Vector2.right)
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.Right);
                }
                else if (HelperUtilities.ApproximatelyEqual(direction, new Vector2(-0.71f, -0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.DownLeft);
                }
                else if (HelperUtilities.ApproximatelyEqual(direction, new Vector2(0.71f, -0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.DownRight);
                }
                else if (HelperUtilities.ApproximatelyEqual(direction, new Vector2(-0.71f, 0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.UpLeft);
                }
                else if (HelperUtilities.ApproximatelyEqual(direction, new Vector2(0.71f, 0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.UpRight);
                }
                // trigger movement event
                player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, player.movementDetails.GetMoveSpeed());
            }

        }

        // else trigger idle event
        else
        {

            player.idleEvent.CallIdleEvent();
            // nếu nhân vật di chuyển đường thằng, lastmovedirection sẽ không được cập nhật, previouslastDir sẽ bằng lastmovedir hiện tại ngăn animateEvent được gọi ngoài mong đợi
            if (previousLastDirection != lastMoveDirection)
            {
                previousLastDirection = lastMoveDirection;

                if (HelperUtilities.ApproximatelyEqual(lastMoveDirection, new Vector2(-0.71f, -0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.DownLeft);
                }
                else if (HelperUtilities.ApproximatelyEqual(lastMoveDirection, new Vector2(0.71f, -0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.DownRight);
                }
                else if (HelperUtilities.ApproximatelyEqual(lastMoveDirection, new Vector2(-0.71f, 0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.UpLeft);
                }
                else if (HelperUtilities.ApproximatelyEqual(lastMoveDirection, new Vector2(0.71f, 0.71f), Settings.epsilon))
                {
                    player.animateEvent.CallAnimateEvent(AimDirection.UpRight);
                }

            }
        }

    }
}
