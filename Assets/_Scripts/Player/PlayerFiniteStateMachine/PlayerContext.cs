using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    #region State Variables
    public PlayerFSM FSM { get; private set; } // getter setter PlayerFSM

    // Creating references for all player states
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set;}
    public PlayerWallGrabState WallGrabState { get; private set;}
    public PlayerWallClimbState WallClimbState { get; private set;}
    public PlayerWallJumpState WallJumpState { get; private set;}
    public PlayerLedgeClimbState LedgeClimbState { get; private set;}
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }

    // SerializeField will allow Unity to access the variable from editor even if it private
    [SerializeField]
    private PlayerData playerData; // Referencing PlayerData
    #endregion+

    #region Components
    public Animator Anim { get; private set; } // getter setter for accessing Animator
    public PlayerInputHandler InputHandler { get; private set; } // getter setter for PlayerInputHandler. So now we can access it from here
    public Rigidbody2D RB { get; private set; } // getter setter for accessing Rigidbody2D 
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider {  get; private set; }

    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform ceilingCheck;
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; } // getter setter for Current Velocity of any Vector2 movement
    public int FacingDirection { get; private set; } // variable 1 (right) or -1 (left) for facing direction

    private Vector2 workSpace; // its purpose is to be the main Vector2 variable so we don't make another variable for the same type
    #endregion

    #region Unity Callback
    private void Awake()
    {
        FSM = new PlayerFSM(); // FSM object

        // Declaring object of states
        IdleState = new PlayerIdleState(this, FSM, playerData, "idle");
        MoveState = new PlayerMoveState(this, FSM, playerData, "move");
        JumpState = new PlayerJumpState(this, FSM, playerData, "inAir");
        InAirState = new PlayerInAirState(this, FSM, playerData, "inAir");
        LandState = new PlayerLandState(this, FSM, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, FSM, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, FSM, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, FSM, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, FSM, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, FSM, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, FSM, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, FSM, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, FSM, playerData, "crouchMove");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>(); // getting access from Animator Component from gameobject
        InputHandler = GetComponent<PlayerInputHandler>(); // getting access from PlayerInputHandler script from gameobject
        RB = GetComponent<Rigidbody2D>(); // getting access from Rigidbody2D component from gameobject
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();

        FacingDirection = 1; // initialize value (right)

        FSM.Initialize(IdleState); // It will make IdleState to be default state
    }

    // Calling LogicUpdate() for states
    private void Update()
    {
        CurrentVelocity = RB.velocity; // it will updating based on RB.velocity of player

        FSM.CurrentState.LogicUpdate(); // calling LogicUpdate(). CurrentState is variable that reference from PlayerState
    }

    private void FixedUpdate()
    {
        FSM.CurrentState.PhysicsUpdate(); // calling PhysicsUpdate(). CurrentState is variable that reference from PlayerState
    }
    #endregion

    #region Set Functions

    public void SetVelocityZero() // function to set velocity to zero
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workSpace = direction * velocity;
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction) // function to set x velocity and y velocity
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocityX(float velocity) // Function to set x velocity
    {
        workSpace.Set(velocity, CurrentVelocity.y); // now its value is gonna be this
        RB.velocity = workSpace; // and it will changing RB.velocity of player
        CurrentVelocity = workSpace; // updating current velocity
    }

    public void SetVelocityY(float velocity) // Function to set y velocity
    {
        workSpace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }


    #endregion

    #region Check Function
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection) // condition to calling Flip() so player Flipping
        {
            Flip();
        }
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    // Checking ground on the feet
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround); // make circle to detect other object
    }

    // Checking wall on facing direction
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround); // make line to detect other object
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround); // make line to detect other object
    }

    #endregion

    #region Other Function
    
    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workSpace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2 ;

        MovementCollider.size = workSpace;
        MovementCollider.offset = center;
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround); // making object RaycastHit2D
        float xDist = xHit.distance; // return distance of platform
        workSpace.Set((xDist + 0.015f) * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
        float yDist = yHit.distance;
        workSpace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);

        return workSpace;
    }
    
    // calling AnimationTrigger() from state machine so it can be used in player states
    private void AnimationTrigger() => FSM.CurrentState.AnimationTrigger();

    // calling AnimationFinishTriggerte machine so it can be used in player states
    private void AnimationFinishTrigger() => FSM.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1; // the value is changing (to left)
        transform.Rotate(0.0f, 180.0f, 0.0f); // Rotating gameobject 180 degree 
    }
    #endregion
}
