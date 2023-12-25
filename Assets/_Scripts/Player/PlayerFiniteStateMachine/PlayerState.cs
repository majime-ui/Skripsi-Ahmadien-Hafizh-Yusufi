using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*PlayerState is base class of all player states, so any state that created will inherit from PlayerState*/
public class PlayerState
{
    // protected means this variable can be access from their children only
    protected PlayerContext player; // Reference of PlayerContext
    protected PlayerFSM playerFSM; // Reference of PlayerFSM
    protected PlayerData playerData; // Reference of PlayerData

    protected bool isAnimationFinished; // bool to check animation is finished
    protected bool isExitingState; // checking if any state exit

    protected float startTime; // Get set everytime enter state

    private string animBoolName; // Telling animator what state that playing

    // Constructor
    public PlayerState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName)
    {
        // this refers to variable name in class
        this.player = player;
        this.playerFSM = playerFSM;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    // virtual means this function can get overwritten by class that inherit from this class
    // Enter() get called when enter specific state
    public virtual void Enter()
    { 
        DoCheck();
        player.Anim.SetBool(animBoolName, true); // transition in of animation
        startTime = Time.time; // save time whenever entering state
        Debug.Log(animBoolName);
        isAnimationFinished = false; //this variable default is false
        isExitingState = false; // this variable default is false
    }

    // Exit() get called when leaving the state
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false); // transition out of animation
        isExitingState = true; // when a state change this become true
    }

    // Update() so its called every frame
    public virtual void LogicUpdate() { }

    // FixedUpdate() so its called every second
    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    // Get called form PhysicsUpdate() and Enter()
    public virtual void DoCheck() { }

    // Get called when Animation playing
    public virtual void AnimationTrigger() { }

    // Get called after animation done playing
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

}
