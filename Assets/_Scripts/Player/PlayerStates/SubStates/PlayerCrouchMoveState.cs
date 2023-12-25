using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(playerData.crouchColliderHeigt);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.SetVelocityX(playerData.crouchMovementVelocity * player.FacingDirection);
            player.CheckIfShouldFlip(xInput);

            if(xInput == 0)
            {
                playerFSM.ChangeState(player.CrouchIdleState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                playerFSM.ChangeState(player.MoveState);
            }
        }
    }
}
