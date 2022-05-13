using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cinemachine;

public class ActionCharacter : MonoBehaviour
{

    #region Variables

    [Space(20)]
    [Header("Object References")]
    [Space(20)]

    //Variables: referencias
    public Animator animator;
    PlayerAudioManager playerAudioManager;
    public Transform thirdPersonCamera;
    public Transform playerModel;
    public PlayerLookAtTransform lookTarget;
    ActionCamera actionCamera;
    public PlayerInput playerInput;
    [HideInInspector]
    public CharacterController characterController;
    public ParticleSystem windParticles;
    public CinemachineVirtualCamera normalCamera;
    public CinemachineVirtualCamera lockCamera;

    [Space(20)]
    [Header("Movement")]
    [Space(20)]

    //GOD
    public bool isGodMode = false;
    public bool dead = false;

    //Variables: movimiento
    public float walkSpeed = 15.0f;
    public float jumpAirSpeed = 3.5f;
    private float currentSpeed;
    public float rotationSpeed = 15.0f;
    public float tiltSpeed = 15.0f;
    public float runMultiplier = 3.0f;

    //Variables: input del jugador
    Vector2 currentMovementInput = Vector2.zero;
    Vector2 lastMovementInput = new Vector2(0,1);
    Vector2 rawInput;
    private Vector2 smoothMovement = Vector2.zero;
    public Vector3 currentMovement;
    private float blendSmooth = 0;


    //Rotacion
    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 playerModelRotation;
    private float targetTilt;

    //Easing
    private Vector2 velocity = Vector2.zero;
    private float velocity2 = 0;
    public float easeFactor = 0.2f;

    //Estados
    public bool movementDisabled = false;
    bool isMovementPressed;
    bool isDashing;
    bool isJumpPressed = false;
    bool isJumping = false;
    bool isRolling = false;
    bool isRunPressed;
    public bool isFloorBelow;

    [Space(20)]
    [Header("Jump")]
    [Space(20)]

    //Salto
    float initialJumpVelocity;
    public float maxJumpHeight = 15.0f;
    public float maxJumpTime = 2f;

    //Gravedad
    public float gravity = -9.8f;
    public float groundedGravity = -.05f;

    //Grounded
    public Vector3 distanceToGround;
    public float groundedRadius;
    public LayerMask groundLayer;
    private Vector3 collisionPoint;

    [Space(20)]
    [Header("Dash")]
    [Space(20)]

    //Dash/Roll
    public DashSetting sprintDash = new DashSetting();
    private IEnumerator dashReset;

    [Space(20)]
    [Header("Combat")]
    [Space(20)]

    //Combate
    public bool hasSword = true;
    public int currentCombo = 0;
    public int comboMax = 3;
    private bool canAttack = true;
    [HideInInspector]
    public bool isAttacking = false;
    public DashSetting[] comboDashes;
    public LayerMask enemyLayerMask;
    private IEnumerator comboReset;
    private IEnumerator resetMovement;
    private Tween attackTween;
    public Vector3 damageBoxExtents;
    public Vector3 damageBoxOffset;
    public Transform targetLocked;
    public float targetLockRadius = 8;

    [Space(20)]
    [Header("FX")]
    [Space(20)]

    //Particles
    public Transform hitFX;
    public Vector3 hitFxOffest;
    public Transform damageFX;
    public Vector3 damageFxOffest;

    [Space(20)]
    [Header("Ledge Climb")]
    [Space(20)]

    //Wall detection
    public LayerMask ledgeLayer;
    public LayerMask hangLayer;
    public Vector3 wallDetectionBoxExtents;
    public Vector3 wallDetectionBoxPosition;
    public Vector3 ledgeStartHeight;
    public Vector3 roofCheckPos;
    public float roofCheckLenght;
    public float ledgeMaxHeight;
    public float ledgeSpread;
    private bool isCollidingWall = false;
    private bool isRoof = false;
    private bool isClimbing = false;
    private bool isHanging = false;
    private bool hang = false;
    public float maxSurfaceTilt;
    public Vector3 hangPosOffset;
    public Vector3 hangPosOffset2;

    //Sliding Slopes
    public float slopeSpeed;
    private Vector3 hitPointNormal;
    private bool isSliding
    {
        get
        {
            if(Physics.Raycast(collisionPoint + transform.position, Vector3.down, out RaycastHit slopeHit, 1f, ledgeLayer))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            } else
            {
                return false;
            }
        }
    }

    #endregion
   
   #region Singleton
   //Singleton
    public static ActionCharacter Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(ActionCharacter)) as ActionCharacter;
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private static ActionCharacter _instance;
    #endregion

    private void Awake()
    {
        Time.timeScale = 1f;
        Application.targetFrameRate = -1;
        Cursor.lockState = CursorLockMode.Locked;

        //Assign references
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        actionCamera = FindObjectOfType<ActionCamera>();
        playerAudioManager = GetComponent<PlayerAudioManager>();

        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Roll.performed += OnRoll;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;
        playerInput.CharacterControls.Attack.performed += OnAttack;
        playerInput.CharacterControls.SwitchGod.performed += OnChangeGodMode;
        playerInput.CharacterControls.Heal.performed += OnHeal;
        playerInput.CharacterControls.TargetLock.performed += context => TargetLock();

        //Levels (Need to move to manager)
        playerInput.CharacterControls.GoToLevel1.performed += (x) => {SceneManager.LoadScene("Level_1");};
        playerInput.CharacterControls.GoToLevel2.performed += (x) => {SceneManager.LoadScene("laboratorio");};
        playerInput.CharacterControls.GoToLevel3.performed += (x) => {SceneManager.LoadScene("Level_2");};
        playerInput.CharacterControls.GoToLevel4.performed += (x) => {SceneManager.LoadScene("boss_room");};
        playerInput.UIControls.Pause.performed += (x) => {GameManager.Instance.SwitchPause();};

        //Initialize speed
        currentSpeed = walkSpeed;

        setupJumpVariables();
    }

    private void OnHeal(InputAction.CallbackContext obj)
    {
        GameManager.Instance.Heal();
    }

    void CheckGrounded()
    {
        if(!isAttacking)
            isFloorBelow = Physics.CheckSphere(transform.position + distanceToGround, groundedRadius, groundLayer);

        if(!isFloorBelow)
        {
            //Check for enemy
            bool isEnemyBelow = Physics.CheckSphere(transform.position + distanceToGround, groundedRadius, enemyLayerMask);

            if(isEnemyBelow)
            {
                characterController.Move(transform.forward * 0.3f);
            }
        }
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void OnChangeGodMode(InputAction.CallbackContext context){
        isGodMode = !isGodMode;

        if(isGodMode) {
            Physics.IgnoreLayerCollision(3, 0, true);
            walkSpeed *= 5;
            currentMovement.y = 0;
        } else {
            Physics.IgnoreLayerCollision(3, 0, false);
            walkSpeed = walkSpeed/5;
        }
    }

    void onMovementInput(Vector2 context)
    {
        //Register last current input
        if (currentMovementInput != Vector2.zero)
            lastMovementInput = currentMovementInput;

        //Obtain movement from input
        currentMovementInput = context;

        rawInput = context;

        //Obtener direccion de la camara
        var cameraForward = thirdPersonCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;
        var cameraRight = thirdPersonCamera.transform.right;
        cameraRight.y = 0;
        cameraRight = cameraRight.normalized;

        //Make input relaitve to camera
        currentMovementInput = new Vector2(cameraForward.x * currentMovementInput.y + cameraRight.x * currentMovementInput.x,
            cameraForward.z * currentMovementInput.y + cameraRight.z * currentMovementInput.x);

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    //TargetLock
    public void TargetLock()
    {
        //if is locked, unlock
        if(targetLocked != null)
        {
            targetLocked = null;

            actionCamera.isLocked = false;
            
            lockCamera.m_Priority = 1;
            normalCamera.m_Priority = 10;
        }
        else
        {
            //Get nearest enemy in range
            Collider[] enemies = Physics.OverlapSphere(transform.position, targetLockRadius, enemyLayerMask);

            if(enemies.Length > 0)
            {
                float closestDist = 100;
                foreach (var enemy in enemies)
                {
                    if(!enemy.CompareTag("TargetLock")) continue;
                    
                    float dist = Vector3.Distance(transform.position, enemy.transform.position);
                    if(dist < closestDist)
                    {
                        closestDist = dist;
                        targetLocked = enemy.transform;
                    }
                }

                lockCamera.m_LookAt = targetLocked;

                actionCamera.isLocked = true;

                lockCamera.m_Priority = 10;
                normalCamera.m_Priority = 1;
            }
        }
    }

    //Ataque
    private void OnAttack(InputAction.CallbackContext context)
    {
        if(!isFloorBelow || isClimbing || isHanging || dead || isRolling || !hasSword)
            return;

        CheckNewAttack();

        if (canAttack == true)
        {
            //If combo ended, stop
            if (currentCombo >= comboMax)
                return;

            if(GameManager.Instance.currentStamina < 10)
            {
                GameManager.Instance.NoStamina();
                return;
            }
            else
            {
                GameManager.Instance.Stamina(20);
            }

            actionCamera.Zoom(0.7f, 60);

            //Seek best enemy target
            //GameObject targetEnemy = SeekEnemy();

            if ( isFloorBelow && currentCombo < comboMax)
            {
                animator.SetInteger("combo", currentCombo);
                currentCombo++;

                isAttacking = true;
                canAttack = false;

                if (comboReset != null)
                    StopCoroutine(comboReset);

                comboReset = comboStop(comboDashes[currentCombo-1].recoverTime);
                StartCoroutine(comboReset);
            }

            //Dash
            animator.SetTrigger("attack");

            actionCamera.JustDidDamge();

            smoothMovement = Vector2.zero;
        }
    }

    //Check if able to perform attack
    public void CheckNewAttack()
    {
        if (!isAttacking)
        {
            canAttack = true;
        }
    }

    public void DoDamage()
    {
        Vector3 boxPos = transform.position;
        boxPos += transform.forward * damageBoxOffset.z;
        boxPos += transform.up * damageBoxExtents.y;

        Collider[] enemies = Physics.OverlapBox(boxPos, damageBoxExtents/2, transform.rotation, enemyLayerMask);

        if(enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                if(enemy.CompareTag("TargetLock")) continue;
                var bas = enemy.GetComponent<EnemyBase>();
                if(bas.isAttacking) continue;
                bas.OnHit(10,transform.position);
                //Camera Shake
                actionCamera.Shake(9);
                //Particles
                var pos = transform.position;
                pos.y += hitFxOffest.y;
                pos += transform.forward * hitFxOffest.z;
                Destroy(Instantiate(hitFX, pos, Quaternion.identity).gameObject, 1);
                //Lock
                if(bas.life < 10 && targetLocked != null)
                    TargetLock();
            }
        }

        //Hit particle
        //var hitFx = Instantiate(hitFX, transform.position + transform.forward * hitFxOffest + transform.up * .2f, Quaternion.identity);
        //Destroy(hitFx.gameObject, 1);
    }

    //Used to reset movement after DoTween
    private void ResetMovement(float duration)
    {
        if (resetMovement != null)
            StopCoroutine(resetMovement);

        resetMovement = movementReset();
        StartCoroutine(resetMovement);

        IEnumerator movementReset()
        {
            yield return new WaitForSeconds(duration);
            isDashing = false;
            if(isRolling)
                isRolling = false;
        }
    }

    //Regular combat dash
    public void CombatDash()
    {
        ExecuteDash(comboDashes[currentCombo-1]);

        //StartCoroutine(lookTarget.Freeze(0.14f));
    }

    //Sprint
    private void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    //Jump
    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();

        if(isHanging && isJumping)
        {
            StopHang();
        }

        targetTilt = 0;
    }

    private void CheckForLedge()
    {
        //Check for wall
        Vector3 boxPos = new Vector3(transform.forward.x * wallDetectionBoxPosition.z, wallDetectionBoxPosition.y, transform.forward.z * wallDetectionBoxPosition.z);
        isCollidingWall = Physics.CheckBox(transform.position + boxPos, wallDetectionBoxExtents/2, transform.rotation, ledgeLayer);

        if(isCollidingWall)
        {
            //Check for surface
            Vector3 rayPos = new Vector3(transform.forward.x * ledgeStartHeight.z, ledgeStartHeight.y, transform.forward.z * ledgeStartHeight.z);
            Vector3 startPos = transform.position + rayPos - (transform.right * ledgeSpread);

            Collider objectHit = null;
            float surfaceDist = -1;

            Vector3 climbPos = Vector3.zero;

            for (int i = 0; i < 3; i++)
            {
                Vector3 linePos = startPos + (transform.right * ledgeSpread * i);
                RaycastHit hit;
                if(Physics.Raycast(linePos, Vector3.down, out hit, ledgeMaxHeight, ledgeLayer))
                {
                    //TODO: CHECK NORMAL
                    
                    //Check if surface is big enough
                    if(objectHit == null)
                    {
                        objectHit = hit.collider;
                    } else if (hit.collider != objectHit)
                    {
                        Debug.Log("Surface not big enough");
                        return;
                    }

                    //Check if surface is even
                    if(surfaceDist <= 0)
                    {
                        surfaceDist = hit.distance;
                    } else if (Mathf.Abs(surfaceDist - hit.distance) >= maxSurfaceTilt)
                    {
                        Debug.Log("Surface not even enough");
                        return;
                    }

                    if(i == 1)
                        climbPos = hit.point;
                } else {
                    Debug.Log("No surface found");
                    return;
                }
            }

            smoothMovement = Vector2.zero;
            currentMovement = Vector3.zero;
            isClimbing = true;
            targetTilt = 0;
            Climb(climbPos);
        }
    }

    private void Climb(Vector3 newPosition)
    {
        Vector3 hangPos = newPosition;
        hangPos.y += hangPosOffset.y;
        hangPos += transform.forward * hangPosOffset.z;
        transform.DOMove(hangPos, 0.2f).OnComplete(
            () => transform.DOMove(newPosition, 1).OnComplete(
                () => {isClimbing = false;}));
        animator.SetTrigger("climb");
    }

    //Called when "Dash"
    private void OnRoll(InputAction.CallbackContext context)
    {
        if (isDashing || isHanging || isClimbing || isAttacking || dead)
            return;

        if(GameManager.Instance.currentStamina < 18)
        {
            GameManager.Instance.NoStamina();
            return;
        }
        else
        {
            GameManager.Instance.Stamina(35);
        }

        //Trigger dash animation
        animator.SetTrigger("roll");

        //Execute the dash
        ExecuteDash(sprintDash, true);

        isRolling = true;

        //WindParticleBurst(10);
    }

    //Change character's rotation instantly
    void QuickRotation(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetTilt = 0;
        transform.rotation = targetRotation;
    }

    //Handle Jump
    void handleJump()
    {
        if(dead) return;
        if(isGodMode)
        {
            if(isJumpPressed)
                currentMovement.y = 4;
            else 
                currentMovement.y = 0;

            return;
        }

        hang = CheckForHang();

        //Ledge Climb
        if(isJumping && isJumpPressed && !characterController.isGrounded && !isClimbing && !isHanging)
        {
            if(hang) return;
            CheckForLedge();
        }

        if(!isJumping && (characterController.isGrounded || isHanging) && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            currentSpeed = jumpAirSpeed;
            animator.SetTrigger("jump");
        } else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            currentSpeed = walkSpeed;
            isJumping = false;
        } else if (isJumping && characterController.isGrounded)
        {
            currentSpeed = walkSpeed;
        }
    }

    private bool CheckForHang()
    {
        Vector3 boxPos = new Vector3(transform.forward.x * wallDetectionBoxPosition.z, wallDetectionBoxPosition.y, transform.forward.z * wallDetectionBoxPosition.z);
        Collider[] col = Physics.OverlapBox(transform.position + boxPos, wallDetectionBoxExtents/2, transform.rotation, hangLayer);

        foreach (var collision in col)
        {
            if(hangLayer == (hangLayer | (1 << collision.gameObject.layer)))
            {
                if(!isHanging && isJumpPressed) StartHang(collision.transform);
                return true;
            }
        }
        return false;
    }

    private void StartHang(Transform obj)
    {
        smoothMovement = Vector2.zero;
        currentMovement = Vector3.zero;
        targetTilt = 0;
        isHanging = true;

        Physics.IgnoreLayerCollision(3, 10, false);
        Physics.IgnoreLayerCollision(3, 6, true);

        animator.SetBool("isHanging", true);

        var plane = new Plane(-obj.forward, obj.position);

        plane.ClosestPointOnPlane(transform.position); 

        Vector3 newPos = plane.ClosestPointOnPlane(transform.position);;
        newPos.y = obj.position.y + hangPosOffset2.y;
        newPos += obj.forward * hangPosOffset2.z;
        
        transform.DOLocalMove(newPos, .2f);
        transform.DORotateQuaternion(obj.rotation, 0.1f);
    }

    public void ReceiveDamage(int damage) 
    {
        if(dead) return;
        animator.SetTrigger("hit");
        isDashing = true;
        isRolling = true;
        isAttacking = false;
        currentMovement = Vector3.zero;
        targetTilt = 0;
        GameManager.Instance.LifeBar(damage);
        actionCamera.Shake(10, 1.6f);
        ResetMovement(1);
    }

    private void StopHang()
    {
        isHanging = false;
        isJumping = false;
        currentSpeed = walkSpeed;
        animator.SetBool("isHanging", false);
        Physics.IgnoreLayerCollision(3, 10, true);
        Physics.IgnoreLayerCollision(3, 6, false);
    }
    
    //Character Rotation
    void handleRotation()
    {
        if (!isDashing && !isClimbing && !isHanging)
        {
            Vector3 positionToLookAt;

            //Input direction
            positionToLookAt.x = currentMovement.x;
            positionToLookAt.y = 0.0f;
            positionToLookAt.z = currentMovement.z;

            //Actual rotation
            Quaternion currentRotation = transform.rotation;


            if (isMovementPressed)
            {
                //New smooth rotation
                targetRotation = Quaternion.LookRotation(positionToLookAt);

                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

                playerModelRotation = playerModel.localRotation.eulerAngles;
            }
            else
            {
                targetRotation = transform.rotation;
            }

            //Tilt
            if (isRunPressed)
            {
                //Obtain Y axis rotation
                float angleA = targetRotation.eulerAngles.y;
                float angleB = currentRotation.eulerAngles.y;
                //Get angle difference
                float diff = Mathf.DeltaAngle(angleA, angleB);
                //Min and max difference
                diff = Mathf.Clamp(diff, -20, 20);

                //Normalize
                float diffNormalized = Mathf.InverseLerp(-20, 20, diff);

                targetTilt = Mathf.Lerp(targetTilt, Mathf.Lerp(-15, 15, diffNormalized), Time.deltaTime * tiltSpeed);
            }
            else
            {
                //Return to original tilt
                targetTilt = Mathf.Lerp(targetTilt, 0, Time.deltaTime * tiltSpeed);
            }
        }

        //Apply tilt to character
        playerModel.localRotation = Quaternion.Euler(playerModelRotation.x, playerModelRotation.y, targetTilt);
    }

    //Gravity control
    void handleGravity()
    {
        if(isGodMode || isClimbing || isHanging)
            return;

        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 1.5f;

        //Apply gravity
        //Euler integration (may have to change it to Velocity Verlet. Btw, not causing significant problems rn)
        if (characterController.isGrounded && !isSliding)
        {          
            currentMovement.y = groundedGravity;
        } else if (isFalling) {
            currentMovement.y += gravity * Time.deltaTime * fallMultiplier;
        } else {
            currentMovement.y += gravity * Time.deltaTime;
        }
    }

    private void handlePhysics()
    {
        if(isHanging)
        {
            return;
        }
        
        if (isDashing)
        {
            return;
        }        

        Vector2 targetMovement = currentMovementInput;

        if(isSliding) 
        {
            Debug.Log("CAEE");
            targetMovement += new Vector2(hitPointNormal.x, hitPointNormal.z) * slopeSpeed;
        }

        //Sprint
        if (isRunPressed && !isSliding)
        {
            targetMovement *= runMultiplier;
        }


        //Interpolation
        smoothMovement = Vector2.SmoothDamp(smoothMovement, targetMovement, ref velocity, easeFactor);

        //Add walk speed
        currentMovement.x = smoothMovement.x * currentSpeed;
        currentMovement.z = smoothMovement.y * currentSpeed;
    }

    private void handleAnimation()
    {
        if(isClimbing) return;
        
        if(isHanging)
        {
            animator.SetFloat("movementSpeed", rawInput.x);
            currentMovement = transform.right * rawInput.x * 1.4f;
            return;
        }

        animator.SetBool("isFalling", !isFloorBelow);

        //Change blendtree's vlaue
        if (isMovementPressed)
        {
            if (isRunPressed)
            {
                blendSmooth = Mathf.SmoothDamp(blendSmooth, 1f, ref velocity2, 0.1f);
            } else
            {
                blendSmooth = Mathf.SmoothDamp(blendSmooth, 0.3f, ref velocity2, 0.1f);
            }
        } else
        {
            blendSmooth = Mathf.SmoothDamp(blendSmooth, 0f, ref velocity2, 0.1f);
        }

        animator.SetFloat("movementSpeed", blendSmooth);
    }

    void Update()
    {
        if(!dead) {
        //Handle input
        onMovementInput(playerInput.CharacterControls.Move.ReadValue<Vector2>());
        //Apply rotacion
        handleRotation();
        //Handle Animation
        handleAnimation();
        //Apply Physics
        handlePhysics();
        }
        else
        {
            currentMovement.x = 0;
            currentMovement.z = 0;
            animator.SetFloat("movementSpeed", 0);
        }

        //Move the character
        if(!isClimbing)
        {  
            characterController.Move(currentMovement * Time.deltaTime * (isGodMode ? 6 : 1));
        }
        //Apply gravity
        handleGravity();
        handleJump();

        //Check if grounded
        CheckGrounded();
    }

    private void WindParticles()
    {
        if(isDashing)
            return;

        //Get character controller velocity minus Y axis
        Vector3 vel = characterController.velocity;
        vel.y = 0;

        float speed = vel.magnitude;

        //Speed min value, to prevent useless particles
        if(speed < 8)
            speed = 0;

        //Assign emission rate
        var particleEmission = windParticles.emission;
        particleEmission.rateOverTime = speed *.4f;
    }

    private void WindParticleBurst(int count) {
        windParticles.Emit(count);
        windParticles.Simulate(1,false,false,false);
        windParticles.Play();
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
        playerInput.UIControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
        playerInput.UIControls.Disable();
    }

    public void EnableInput(bool enable) {
        if(enable)
            playerInput.CharacterControls.Enable();
        else
            playerInput.CharacterControls.Disable();
    }


    //Be able to call dash externally
    public void ExecuteDash(DashSetting setting, bool isRoll = false)
    {
        if (dashReset != null)
            StopCoroutine(dashReset);

        Vector3 dir;

        dir = new Vector3(lastMovementInput.x, 0, lastMovementInput.y);
        dashReset = Dash(setting.dashTime, setting.dashForce, setting.dashCurve, dir, isRoll);
        StartCoroutine(dashReset);
        isDashing = true;
        ResetMovement(setting.dashTime);
    }

    //Dash
    public IEnumerator Dash (float duration, float force, AnimationCurve curve, Vector3 direction, bool isRoll)
    {
        if(targetLocked != null && Vector3.Distance(targetLocked.position, transform.position) < 4 && !isRoll)
        {
            transform.DOLookAt(targetLocked.position, 0.2f, AxisConstraint.Y);
            direction = (targetLocked.position - transform.position).normalized;
        } else
        {
            QuickRotation(new Vector3(lastMovementInput.x, 0, lastMovementInput.y));
        }
        
        //Stop Movement
        smoothMovement = new Vector2(0, 0);

        //Jump
        if (direction.y != 0)
            currentMovement.y = direction.y;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            //Generate dash vector
            Vector3 dashVector;
            dashVector = direction * force;

            //Normalize elapsed time (0 - 1)
            float elapsedRange = elapsedTime / duration;

            //Use curve to get the current value
            Vector3 dashMovement = Vector3.Lerp(Vector3.zero, dashVector, curve.Evaluate(elapsedRange));

            currentMovement = new Vector3(dashMovement.x, currentMovement.y, dashMovement.z);

            yield return null;
        }
    }


    private IEnumerator comboStop (float time)
    {
        yield return new WaitForSeconds(time);

        currentCombo = 0;
        animator.SetInteger("combo", currentCombo);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        collisionPoint = hit.point;
        collisionPoint += Vector3.up * 0.2f;
        collisionPoint = (collisionPoint - transform.position);
    }

    //Gizmos
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position, -Vector3.up.normalized * distanceToGround);
        Gizmos.DrawWireSphere(transform.position + distanceToGround, groundedRadius);
    
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(collisionPoint + transform.position, 0.2f);

        //Wall Detection collider
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = isCollidingWall? Color.green : Color.red;
        Gizmos.DrawWireCube(Vector3.zero + wallDetectionBoxPosition, wallDetectionBoxExtents);

        Gizmos.color = Color.black;

        Gizmos.DrawWireCube(Vector3.zero + damageBoxOffset, damageBoxExtents);

        //Ledge Height check
        Vector3 startPos = ledgeStartHeight - (Vector3.right * ledgeSpread);
        
        for (int i = 0; i < 3; i++)
        {
            Vector3 linePos = startPos + (Vector3.right * ledgeSpread * i);
            Gizmos.DrawLine(linePos, linePos + Vector3.down * ledgeMaxHeight);
        }

        //Roof Check
        Vector3 roofPos = roofCheckPos;
        Gizmos.color = isRoof? Color.red : Color.green;
        Gizmos.DrawLine(roofPos, roofPos + Vector3.down * roofCheckLenght);
    }
}

[Serializable]
public class DashSetting
{
    public float dashTime = 0.4f;
    public float dashForce = 3.0f;
    public AnimationCurve dashCurve;
    public float upForce = 0;
    public float recoverTime = .6f;
}

[Serializable]
public class ParticlePosition
{
    public Vector3 pos;
    public Vector3 rot;
}