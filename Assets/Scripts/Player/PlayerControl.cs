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
    private float moveSpeed = 6.0f;
    private PlayerInput inputActions;
    private InputAction move;
    private InputAction fire;
    private Vector2 direction;
    private Vector2 lastMoveDirection;
    private Vector2 previousLastDirection;

    private void Awake()
    {
        // Load components
        player = GetComponent<Player>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();

        // Initialize input actions
        inputActions = new PlayerInput();

    }

    private void OnEnable()
    {
        move = inputActions.Player.Move;
        fire = inputActions.Player.Fire;

        move.Enable();
        fire.Enable();

    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();

    }

    private void Update()
    {
        MovementInput();
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
                player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
            }
            else
            {
                player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, 0);
                player.attackEvent.CallAttackEvent(true);
            }
        }

        // else trigger idle event
        else
        {
            if (fire.ReadValue<float>() > 0)
            {
                player.attackEvent.CallAttackEvent(true);

            }
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
}
