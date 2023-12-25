using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition; // variable to hold position when wall climbing

    public PlayerWallGrabState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;

        HoldPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            HoldPosition();

            if(yInput > 0)
            {
                playerFSM.ChangeState(player.WallClimbState);
            }
            else if(yInput < 0 || !grabInput)
            {
                playerFSM.ChangeState(player.WallSlideState);
            }
        }
    }


    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        player.SetVelocityX(0); // keep camera center of the player
        player.SetVelocityY(0);
    }
}
