using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*PlayerFSM is script that contain current state that play in player*/
public class PlayerFSM
{
    /*PlayerFSM contain variables that hold reference of the current state, function that initialize current state, and function that change state*/

    // {  get; private set; } is called getter setter meaning any other script that have reference to the variable can get the variable and read what it is but can only set from this script
    public PlayerState CurrentState {  get; private set; }
    
    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState; // its initialize the first or default state
        CurrentState.Enter(); // its calling Enter() from PlayerState so player will entering this default state
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit(); // its calling Exit() from PlayerState so player will exiting the current state
        CurrentState = newState; // its changing state to newState
        CurrentState.Enter(); // its calling Enter() from PlayerState so player will entering this new state
    }
}
