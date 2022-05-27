using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour, IHitable
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
    public float stunForce;
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
    public Transform attackFX;
    public float fxOffset;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Animator animator;

    //Private variables
    private Transform player;
    private Vector3 currentTarget;
    private IEnumerator resetMovement;
    public bool playerInRange = false;
    private bool canAttack = false;
    private bool isChasing = false;
    public bool isAttacking = false;
    private float attackTimerDuration = 0;
    private float currentAttackTimer = 0;
    private float currentIdleTimer = 0;
    private float idleTimerDuration = 0;
    private IEnumerator attackCoroutine;
    private bool warning;
    private GameObject warningObject;
    private IEnumerator warningCoroutine;
    public Transform lifePivot;
    public GameObject bar;
    public GameObject lockTar;
    public SpriteRenderer lifeRenderer;
    public UnityEvent dieEvent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
                animator.SetBool("isWalking", true);
            } else {
                animator.SetBool("isWalking", false);
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
        
        //Combat Related
        if(!playerInRange) return;

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
        if(EnemyManager.immaAttack >= 2)
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

        animator.SetBool("isWalking", true);

        //Change speed
        navMeshAgent.speed = chaseSpeed;

        yield return new WaitForSeconds(chaseTime);
        
        if(playerInRange)
        {
            Attack();
        } 
        else 
        {
            navMeshAgent.speed = walkSpeed;
            isChasing = false;
            EnemyManager.EnemyStopsAttacking();
            attackTimerDuration = Random.Range(attackTimerMin, attackTimerMax);
            currentAttackTimer = 0;
        }
    }

    IEnumerator Warning() 
    {
        warning = true;
        //warningObject = Instantiate(warningFX, warningPos).gameObject;

        yield return new WaitForSeconds (.6f);

        if(playerInRange)
        {
            attackCoroutine = Chase();

            StartCoroutine(attackCoroutine);
        } 
        else 
        {
            navMeshAgent.speed = walkSpeed;
            warning = false;
            isChasing = false;
            EnemyManager.EnemyStopsAttacking();
            attackTimerDuration = Random.Range(attackTimerMin, attackTimerMax);
            currentAttackTimer = 0;
        }
    }

    void Attack() 
    {
        if(targetedByPlayer) return;

        //RandomAttack
        var rand = Random.value;
        string attack;
        float delay;

        if(rand <= .35f) {attack = "attack3"; delay = 1.4f;}
        else if(rand <= .65f) {attack = "attack2"; delay = 1f;}
        else {attack = "attack1"; delay = 0.3f;}
        
        //Animation
        animator.SetBool("isWalking", false);
        animator.SetTrigger(attack);

        //Change speed
        navMeshAgent.speed = walkSpeed;

        Debug.Log("ataco");

        isChasing = false;
        isAttacking = true;

        toPlayerBoost(1, delay);

        EnemyManager.EnemyStopsAttacking();

        //Boost
        Vector3 dir = transform.position - player.position;
        dir.y = 0;

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

    public void Heal()
    {
        life = 100;
        lifePivot.localScale = Vector3.one;
    }

    public string OnHit(int damage, Vector3 direction)
    {
        targetedByPlayer = false;

        //If is attacking, don't
        if(isAttacking) return "fail";
        
        life -= damage;

        if(life<=0)
        {
            if(isChasing) StopCoroutine(attackCoroutine);
            if(warning) StopCoroutine(warningCoroutine);
            animator.SetTrigger("die");
            navMeshAgent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            bar.SetActive(false);
            lockTar.SetActive(false);
            this.enabled = false;
            dieEvent.Invoke();
            return "die";
        }

        //LifeBar
        lifePivot.DOScaleX(life/100f, 0.3f).SetEase(Ease.InQuad);
        lifeRenderer.DOBlendableColor(Color.red, 0.1f).OnComplete(() => lifeRenderer.DOBlendableColor(Color.white, 0.3f));

        //FX
        var fx = Instantiate(hitFX);
        fx.parent = transform;
        fx.localPosition = Vector3.up * fxOffset;
        Destroy(fx.gameObject, 1f);

        //On Hit disable navagent, disable kinematic and add force
        KnockBack(direction);

        //Rotate   
        LookAtPlayer();

        //Animation
        animator.SetTrigger("hit");

        return "hit";
    }

    public void KnockBack(Vector3 direction)
    {
        Vector3 dir = transform.position - direction;
        dir.y = 0;
        dir = dir.normalized;

        directionBoost(stunForce, stunTime, dir);
    }

    void LookAtPlayer()
    {
        Vector3 dir = transform.position - player.position;
        dir.y = 0;
        dir = dir.normalized;

        //Rotate
        transform.rotation =  Quaternion.LookRotation(-dir);
    }

    public void toPlayerBoost(int force, float delay)
    {
        directionBoost(force, delay);
    }

    public void DealDamage(int damage)
    {
        Vector3 boxPos = transform.position;
        boxPos += transform.forward * damageAreaOffset.z;
        boxPos += transform.up * damageAreaExtents.y;

        //Particles
        if(damage >= 20)
        {
            Destroy(Instantiate(attackFX,transform.position + transform.forward*2, Quaternion.identity).gameObject, 2);
            Camera.main.GetComponent<ActionCamera>().Shake(12, 1);
        }


        if(Physics.CheckBox(boxPos, damageAreaExtents/2, transform.rotation, playerLayerMask))
        {
            ActionCharacter.Instance.ReceiveDamage(damage);
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
        else if (canAttack && !isChasing && !isAttacking && !warning && playerInRange)
            AttackMode();

        if(isChasing && !justHit)
        {
            //Go to player
            navMeshAgent.destination = player.position; 
            //LookAtPlayer();
            if(Vector3.Distance(transform.position, player.position) < 2)
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

        if(EnemyManager.activeEnemies.Count <= 0)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().DOFade(0, 2);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position + damageAreaOffset, damageAreaExtents);
    }
}
