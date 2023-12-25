using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput; // now every PlayerGroundedState inheritance will have this variable for input
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool dashInput;

    public PlayerGroundedState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded(); // true if grounded
        isTouchingWall = player.CheckIfTouchingWall(); // true if touching wall
        isTouchingLedge = player.CheckIfTouchingLedge();
        isTouchingCeiling = player.CheckForCeiling();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft(); // reseting jump counter whenever player touch ground
        player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX; // now everytime user inputting movement x will be the value of this variable
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput; // now when player in the ground, we can call JumpInput
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if (JumpInput && player.JumpState.CanJump()) // condition of changing state to JumpState
        {
            playerFSM.ChangeState(player.JumpState);
        }
        else if (!isGrounded) // condition for chnage state to InAirState
        {
            player.InAirState.StartCoyoteTime();
            playerFSM.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            playerFSM.ChangeState(player.WallGrabState);
        }
        else if(dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            playerFSM.ChangeState(player.DashState);
        }
    }
}
