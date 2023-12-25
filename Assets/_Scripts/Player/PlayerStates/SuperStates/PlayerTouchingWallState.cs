using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded; 
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool isTouchingLedge;
    protected int xInput;
    protected int yInput;

    public PlayerTouchingWallState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded(); // true if grounded
        isTouchingWall = player.CheckIfTouchingWall(); // true if touching wall
        isTouchingLedge = player.CheckIfTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPostion(player.transform.position);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;

        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall); // wall jump direction
            playerFSM.ChangeState(player.WallJumpState);
        }
        else if(isGrounded && !grabInput) // condition for change state to IdleState
        {
            playerFSM.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || (xInput != player.FacingDirection && !grabInput)) // condition for change state to inairstate
        {
            playerFSM.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && !isTouchingLedge)
        {
            playerFSM.ChangeState(player.LedgeClimbState);
        }
    }
}
