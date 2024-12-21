using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimator : ScriptAnimator
{
    private PlayerAttack attacker;
    private Runner runner;
    private Jumper jumper;
    private PlayerState playerState;
    private FacingDirection facingDirection;
    private Climb climb;
    private Vector3 initialLocalScale;
    
    protected override void Init()
    {
        #if UNITY_EDITOR
        RequireStates(
            nameof(AnimState.Idle),
            nameof(AnimState.Jump),
            nameof(AnimState.Fall),
            nameof(AnimState.Run),
            nameof(AnimState.Attack),
            nameof(AnimState.Up)
            // nameof(AnimState.Victory)
        );
        #endif
        TryGetComponent<PlayerAttack>(out attacker);
        TryGetComponent<Runner>(out runner);
        TryGetComponent<Jumper>(out jumper);
        TryGetComponent<PlayerState>(out playerState);
        TryGetComponent<FacingDirection>(out facingDirection);
        TryGetComponent<Climb>(out climb);
        initialLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // if(playerState.hasWon){
        //     PlayStateIfNotInState(AnimState.Victory);
        // }
        if(attacker.isAttacking){
            PlayStateIfNotInState(AnimState.Attack);
        }
        else if(climb.isClimbing){
            PlayStateIfNotInState(AnimState.Up);
        }
        else if(jumper.isRising){
            PlayStateIfNotInState(AnimState.Jump);
        }
        else if(jumper.isFalling){
            PlayStateIfNotInState(AnimState.Fall);
        }
        else if(runner.isRunning){
            PlayStateIfNotInState(AnimState.Run);
        }
        else{
            PlayStateIfNotInState(AnimState.Idle);
        }
        if(facingDirection.isFacingAwayFromInitialDirection) transform.localScale 
        = new Vector3(-initialLocalScale.x, initialLocalScale.y, initialLocalScale.z);
        else transform.localScale = initialLocalScale;
    }
}
