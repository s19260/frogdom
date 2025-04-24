using UnityEngine;

public class SetBoolBehavior : StateMachineBehaviour
{
    public string booleanName;
    public bool setOnStateMachineEnterAndExit;
    public bool trueWhileInMachine = true;

    public bool setOnStateEnter = false;
    public bool setOnStateExit  = false;
    public bool trueWhileInState = true;
    public bool updateOnState = false;
    public bool updateOnStateMachine = false;
    

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (setOnStateEnter)
            animator.SetBool(booleanName, trueWhileInState);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (setOnStateExit)
            animator.SetBool(booleanName, !trueWhileInState);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (updateOnState)
            animator.SetBool(booleanName, trueWhileInState);
    }

    public void OnStateMachineUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnStateMachine)
            animator.SetBool(booleanName, trueWhileInState);
    }
    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (setOnStateMachineEnterAndExit)
            animator.SetBool(booleanName, trueWhileInMachine);
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(setOnStateMachineEnterAndExit)
            animator.SetBool(booleanName, !trueWhileInMachine);
    }
}
