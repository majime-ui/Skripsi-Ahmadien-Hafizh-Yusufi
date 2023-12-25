using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);

        player.SetVelocityX(playerData.movementVelocity * xInput); // moving player horizontaly

        if(!isExitingState)
        {
            if(xInput == 0) // condition to change state to IdleState
            {
                playerFSM.ChangeState(player.IdleState);
            }
            else if(yInput == -1)
            {
                playerFSM.ChangeState(player.CrouchMoveState);
            }
        }
    }
}
