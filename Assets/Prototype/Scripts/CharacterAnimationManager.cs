using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    ActionCharacter actionCharacter;
    PlayerAudioManager playerAudioManager;
    public AudioClip footstepSound;

    private void Start()
    {
        actionCharacter = gameObject.GetComponentInParent<ActionCharacter>();
        playerAudioManager = gameObject.GetComponentInParent<PlayerAudioManager>();
    }

    public void CombatDash()
    {
        actionCharacter.CombatDash();
    }

    public void AllowNewAttack()
    {
        actionCharacter.isAttacking = false;
    }

    public void ContactPoint()
    {
        actionCharacter.DoDamage();
    }

    public void PlayAudio(AnimationEvent evt)
    {  
        if (evt.animatorClipInfo.weight < 0.5 && evt.animatorStateInfo.IsName("Base movement"))
            return;

        playerAudioManager.AudioPlay(evt.stringParameter);
    }
}
