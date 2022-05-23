using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    public Animator animator;
    public int minSpecialWaitCount;
    public float minAttackTime;
    public float maxAttackTime;
    private float newAttackTime;
    private float currentAttackTime;
    public bool isAttacking;
    public int currentCyclone = 2;
    public bool StopRotation = false;
    public float rotationSpeed;
    public BossDamage hitBox;
    public bool dead;

    private void Start()
    {
        playerTransform = ActionCharacter.Instance.transform;
        newAttackTime = 10;
    }

    void Update()
    {
        if(dead) return;
        if(!StopRotation)
        {
            LookAtPlayerSmooth();
        }

        if(!isAttacking)
        {
            
            currentAttackTime += Time.deltaTime;

            if(currentAttackTime >= newAttackTime)
            {
                currentAttackTime = 0;
                isAttacking = true;
                if(BossDamage.life <= currentCyclone*33)
                {
                    Cyclone();
                }
                else
                {
                    Attack();
                }
            }
        }
    }

    private void Cyclone()
    {
        //currentCyclone--;

        StopRotation = true;

        //Round
        var vec = transform.eulerAngles;
        vec.y = (Mathf.Round(vec.y / 72) * 72) -38;

        transform.DORotate(vec, 0.3f);

        animator.SetTrigger("cyclone");

        StartCoroutine(Reset(18));
    }

    private void Attack()
    {
        //Random attack selector
        var rand = Random.value;
        var rand2 = Random.value;
        string attack;
        float delay = 1;

        if(rand <= .5f)
        {
            if(rand2 <= .4f) {attack = "attack_hand";}
            else if(rand2 <= .8f) {attack = "attack_tentacle";}
            else {attack = "attack_merge"; delay = 3;}
        }
        else
        {
            if(rand2 <= .5f) {attack = "sweep_hand";}
            else {attack = "sweep_tentacle";}
        }

        animator.SetTrigger(attack);

        StartCoroutine(Reset(delay));
    }

    IEnumerator Reset(float duration)
    {
        yield return new WaitForSeconds(duration);
        newAttackTime = Random.Range(minAttackTime, maxAttackTime+1);
        isAttacking = false;
    }

    public void BlockRotation()
    {
        StopRotation = !StopRotation;
    }

    public void ConstantDamage()
    {
        hitBox.constantDamage = true;
        Camera.main.GetComponent<ActionCamera>().Shake(2, 2);
    }

    public void EndConstantDamage()
    {
        hitBox.constantDamage = false;
    }

    public void DealDamage() 
    {
        hitBox.DoDamage();
        Camera.main.GetComponent<ActionCamera>().Shake(7, 1);
    }

    void LookAtPlayerSmooth()
    {
        var rot = Quaternion.LookRotation(playerTransform.position - transform.position, Vector3.up).eulerAngles;
        rot.x = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), rotationSpeed * Time.deltaTime);
    }
}
