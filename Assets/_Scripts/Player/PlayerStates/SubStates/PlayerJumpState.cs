using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft; // store how many jumps left

    public PlayerJumpState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity); // make player jumping bcz y value change
        player.InputHandler.UseJumpInput();
        isAbilityDone = true; // ability is done
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump() // allowing player jump
    {
        if (amountOfJumpsLeft > 0) { return true; }
        else { return false; }
    }
    
    // resetting jump counter
    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    // decrease jump counter
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
