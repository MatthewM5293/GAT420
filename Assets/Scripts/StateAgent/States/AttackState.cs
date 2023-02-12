using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackState : State
{
    //private float timer;
    public AttackState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.movement.Stop();        

        var colliders = Physics.OverlapSphere(owner.transform.position, 2);
        foreach (var collider in colliders)
        {
            //avoid friendly fire
            if (collider.gameObject == owner.gameObject || collider.gameObject.CompareTag(owner.gameObject.tag)) continue;

            if (collider.gameObject.TryGetComponent<StateAgent>(out var component))
            {
                if (component.health.value > 0)
                {
                    owner.animator.SetTrigger("Attack");

                    AnimationClip[] clips = owner.animator.runtimeAnimatorController.animationClips;
                    AnimationClip clip = clips.FirstOrDefault<AnimationClip>(clip => clip.name == "Punch");
                    
                    component.health.value -= Random.Range(10, 25);
                }
            }
        }
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        
    }
}
