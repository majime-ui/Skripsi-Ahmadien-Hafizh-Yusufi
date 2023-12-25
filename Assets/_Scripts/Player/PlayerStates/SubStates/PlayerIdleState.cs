using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{

    // Constructor
    public PlayerIdleState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityX(0f); // it stop the movement of player

        if (!isExitingState)
        {
            if(xInput != 0) // condition for changing to MoveState
            {
                playerFSM.ChangeState(player.MoveState);
            }
            else if(yInput == -1)
            {
                playerFSM.ChangeState(player.CrouchIdleState);
            }
        }
    }
}
