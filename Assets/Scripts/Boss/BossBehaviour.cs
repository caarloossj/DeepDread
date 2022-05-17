using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool StopRotation = false;
    public float rotationSpeed;


    private void Start()
    {
        playerTransform = ActionCharacter.Instance.transform;
        newAttackTime = Random.Range(minAttackTime, maxAttackTime+1);
    }

    void Update()
    {
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
                Attack();
            }
        }
    }

    private void Attack()
    {
        //Random attack selector

        animator.SetTrigger("attack_hand");

        StartCoroutine(Reset(1));
    }

    IEnumerator Reset(float duration)
    {
        yield return new WaitForSeconds(duration);
        newAttackTime = Random.Range(minAttackTime, maxAttackTime+1);
        isAttacking = false;
    }

    public void BlockRotation() {
        StopRotation = !StopRotation;
    }

    void LookAtPlayerSmooth()
    {
        var rot = Quaternion.LookRotation(playerTransform.position - transform.position, Vector3.up).eulerAngles;
        rot.x = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), rotationSpeed * Time.deltaTime);
    }
}
