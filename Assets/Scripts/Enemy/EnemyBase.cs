using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    //Status
    public enum Status { Idle, Chasing, Stunned};
    public bool justHit = false;
    public Status currentStatus;

    //TODO: Enemy Scriptable Objects

    //Public variables
    public int life = 100;
    public int walkSpeed = 4;
    public int chaseSpeed = 8;
    public float stunTime = 2;
    public float upForce = 380;
    public float attackTimerMin;
    public float attackTimerMax;
    public float minTargetDistance;
    public float maxTargetDistance;
    public float chaseTime;
    public float newTargetTimerMin;
    public float newTargetTimerMax;
    public float impulseForceMultiplier;
    public bool targetedByPlayer = false;
    public Vector3 damageAreaOffset;
    public Vector3 damageAreaExtents;
    public LayerMask playerLayerMask;

    //Component References
    public Transform hitFX;
    public Transform warningFX;
    public Vector3 fxOffset;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Animator animator;
    public Renderer _renderer;
    public Transform warningPos;

    //Private variables
    private Transform player;
    private Vector3 currentTarget;
    private IEnumerator resetMovement;
    private bool canAttack = false;
    private bool isChasing = false;
    private bool isAttacking = false;
    private float attackTimerDuration = 0;
    private float currentAttackTimer = 0;
    private float currentIdleTimer = 0;
    private float idleTimerDuration = 0;
    private IEnumerator attackCoroutine;
    private bool warning;
    private GameObject warningObject;
    private IEnumerator warningCoroutine;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        player = ActionCharacter.Instance.transform;

        navMeshAgent.speed = walkSpeed;

        //Setup Timer
        attackTimerDuration = Random.Range(attackTimerMin, attackTimerMax);
        idleTimerDuration = Random.Range(newTargetTimerMin, newTargetTimerMax);

        //Get target
        currentTarget = GetNewTargetPosition();
        navMeshAgent.destination = currentTarget;
    }

    void IdleMode()
    {
        if(!justHit)
        {
            if(navMeshAgent.remainingDistance > 2.5)
            {
                //animator.SetBool("isWalking", true);
                LookAtPlayer();
            } else {
                //animator.SetBool("isWalking", false);
            }

            currentIdleTimer += Time.deltaTime;
        
            if(currentIdleTimer >= idleTimerDuration)
            {
                currentIdleTimer = 0;
                idleTimerDuration = Random.Range(newTargetTimerMin, newTargetTimerMax);
                currentTarget = GetNewTargetPosition();
                navMeshAgent.destination = currentTarget;
                return;
            }
        }

        currentAttackTimer += Time.deltaTime;

        if(currentAttackTimer >= attackTimerDuration)
        {
            canAttack = true;
            return;
        }
    }

    void AttackMode()
    {
        canAttack = false;

        //Check if enemies are already attacking
        if(justHit || EnemyManager.immaAttack >= 2)
        {
            attackTimerDuration = Random.Range(attackTimerMin, attackTimerMax);
            currentAttackTimer = 0;
            return;
        }

        warningCoroutine = Warning();
        StartCoroutine(warningCoroutine);

        EnemyManager.EnemyAttacking();
    }

    IEnumerator Chase() {
        isChasing = true;

        //animator.SetBool("isWalking", true);

        //Change speed
        navMeshAgent.speed = chaseSpeed;

        yield return new WaitForSeconds(chaseTime);

        Attack();
    }

    IEnumerator Warning() 
    {
        warning = true;
        //warningObject = Instantiate(warningFX, warningPos).gameObject;

        yield return new WaitForSeconds (.6f);

        attackCoroutine = Chase();

        StartCoroutine(attackCoroutine);
    }

    void Attack() 
    {
        if(targetedByPlayer) return;

        //Animation
        //animator.SetBool("isWalking", false);
        //animator.SetTrigger("attack");

        //Change speed
        navMeshAgent.speed = walkSpeed;

        Debug.Log("ataco");

        isChasing = false;
        isAttacking = true;
        
        toPlayerBoost(1);

        EnemyManager.EnemyStopsAttacking();

        //Boost
        Vector3 dir = transform.position - player.position;
        dir.y = 0;
        dir = dir.normalized;

        //New Timer
        attackTimerDuration = Random.Range(attackTimerMin, attackTimerMax);
        currentAttackTimer = 0;

        //Stop Warning
        warning = false;
        Destroy(warningObject);
    }

    Vector3 GetNewTargetPosition()
    {
        //Get the player position
        Vector3 playerPos = player.position;
        //Get a random new direction
        Vector3 dir = Quaternion.Euler(0,Random.Range(0, 360), 0) * Vector3.forward;
        //Generate a new position
        Vector3 newPos = playerPos + dir * Random.Range(minTargetDistance, maxTargetDistance);
        return newPos;
    }

    public void playerTarget()
    {
        targetedByPlayer = true;


        navMeshAgent.speed = walkSpeed;

        if(isChasing)
        {
            isChasing = false;

            StopCoroutine(attackCoroutine);

            attackTimerDuration = Random.Range(attackTimerMin, attackTimerMax);
            currentAttackTimer = 0;
        }

        if(warning)
        {
            Destroy(warningObject);
            warning = false;
            StopCoroutine(warningCoroutine);

            attackTimerDuration = Random.Range(attackTimerMin, attackTimerMax);
            currentAttackTimer = 0;

            EnemyManager.EnemyStopsAttacking();
        }
    }

    public void OnHit(int damage, Vector3 direction)
    {
        targetedByPlayer = false;
        
        life -= damage;

        //FX
        //var fx = Instantiate(hitFX);
        //fx.parent = transform;
        //fx.localPosition = fxOffset;
        //Destroy(fx.gameObject, 0.6f);

        //If is attacking, don't knockback
        if(isAttacking) return;

        //On Hit disable navagent, disable kinematic and add force
        KnockBack(direction);

        //Rotate   
        LookAtPlayer();

        //Animation
        //animator.SetTrigger("damage");
    }

    public void KnockBack(Vector3 direction)
    {
        Vector3 dir = transform.position - direction;
        dir.y = 0;
        dir = dir.normalized;

        directionBoost(180, stunTime, dir);
    }

    void LookAtPlayer()
    {
        Vector3 dir = transform.position - player.position;
        dir.y = 0;
        dir = dir.normalized;

        //Rotate
        transform.rotation =  Quaternion.LookRotation(-dir);
    }

    public void toPlayerBoost(int force)
    {
        directionBoost(force, .8f);
    }

    public void DealDamage()
    {
        Vector3 boxPos = transform.position;
        boxPos += transform.forward * damageAreaOffset.z;
        boxPos += transform.up * damageAreaExtents.y;

        if(Physics.CheckBox(boxPos, damageAreaExtents/2, transform.rotation, playerLayerMask))
        {
            //ActionCharacter.Instance.ReceiveDamage(1, transform.position);
        }
    }

    void directionBoost(float force, float recoverTime, Vector3 direction = default(Vector3))
    {
        ResetMovement(recoverTime);

        if(direction == Vector3.zero)
        {

            direction = transform.position - player.position;
            direction.y = 0;
            direction = -direction.normalized;
            //Look at player
            LookAtPlayer();

            //Check if distance if enough
            if(Vector3.Distance(transform.position, player.position) < 2f) return;
        }

        navMeshAgent.enabled = false;
        rb.isKinematic = false;
        justHit = true;
        rb.AddForce(direction * force * impulseForceMultiplier, ForceMode.Impulse);
    }

    void Update()
    {
        BehaviourLoop();
    }

    private void BehaviourLoop()
    {
        if(!canAttack && !isChasing && !isAttacking && !warning)
            IdleMode();
        else if (canAttack && !isChasing && !isAttacking && !warning)
            AttackMode();

        if(isChasing && !justHit)
        {
            //Go to player
            navMeshAgent.destination = player.position; 
            LookAtPlayer();
            if(Vector3.Distance(transform.position, player.position) < 3)
            {
                StopCoroutine(attackCoroutine);
                Attack();
            }
        }
    }

    private void ResetMovement(float duration)
    {
        if (resetMovement != null)
            StopCoroutine(resetMovement);

        resetMovement = movementReset();
        StartCoroutine(resetMovement);

        IEnumerator movementReset()
        {
            yield return new WaitForSeconds(duration);
            navMeshAgent.enabled = true;
            rb.isKinematic = true;
            justHit = false;  

            if(isAttacking)
            {
                isAttacking = false;
                navMeshAgent.destination = currentTarget;
            }
        }
    }

    private void OnEnable()
    {
        EnemyManager.AddEnemy(this);
    }

    private void OnDisable()
    {
        EnemyManager.RemoveEnemy(this);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position + damageAreaOffset, damageAreaExtents);
    }
}
