using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft(); // resetting jump counter
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection); // setting x velocity and y velocity
        player.CheckIfShouldFlip(wallJumpDirection); // player should flip if it facing wall
        player.JumpState.DecreaseAmountOfJumpsLeft(); // when wall jump happened jump counter is decrease
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        if(Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
    
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if(isTouchingWall)
        {
            wallJumpDirection = -player.FacingDirection; // This will make the jump direction the opposite of the facing direction
        }
        else
        {
            wallJumpDirection = player.FacingDirection; // This will make the jump direction the opposite of the facing direction
        }
    }
}
