using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if(xInput != 0)
            {
                playerFSM.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                playerFSM.ChangeState(player.IdleState);
            }
        }
    }
}
