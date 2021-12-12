using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ActionCharacter : MonoBehaviour
{

    #region Variables

    [Space(20)]
    [Header("Object References")]
    [Space(20)]

    //Variables: referencias
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

    [Space(20)]
    [Header("Movement")]
    [Space(20)]

    //GOD
    public bool isGodMode = false;

    //Variables: movimiento
    public float walkSpeed = 15.0f;
    public float rotationSpeed = 15.0f;
    public float tiltSpeed = 15.0f;
    public float runMultiplier = 3.0f;

    //Variables: input del jugador
    Vector2 currentMovementInput = Vector2.zero;
    Vector2 lastMovementInput = new Vector2(0,1);
    private Vector2 smoothMovement = Vector2.zero;
    Vector3 currentMovement;
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
    //public EnemyBase enemyTarget;

    [Space(20)]
    [Header("FX")]
    [Space(20)]

    //Particles
    public Transform hitFX;
    public float hitFxOffest;
    public Transform fistFX;
    public ParticlePosition[] comboParticles;

    [Space(20)]
    [Header("Ledge Climb")]
    [Space(20)]

    //Wall detection
    public Vector3 wallDetectionBoxExtents;
    public Vector3 wallDetectionBoxPosition;
    public Vector3 ledgeStartHeight;
    public Vector3 roofCheckPos;
    public float roofCheckLenght;
    public float ledgeMaxHeight;
    public float ledgeSpread;

    [Space(20)]
    [Header("Sounds")]
    [Space(20)]

    public AudioClip dashSound;

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

        //Levels (Need to move to manager)
        playerInput.CharacterControls.GoToLevel1.performed += (x) => {SceneManager.LoadScene("level1_prototype");};
        playerInput.CharacterControls.GoToLevel2.performed += (x) => {SceneManager.LoadScene("level2_prototype");};
        playerInput.CharacterControls.GoToLevel3.performed += (x) => {SceneManager.LoadScene("level3_prototype");};
        playerInput.UIControls.Pause.performed += (x) => {GameManager.Instance.SwitchPause();};

        setupJumpVariables();
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

    //Ataque
    private void OnAttack(InputAction.CallbackContext context)
    {
        if(!isFloorBelow)
            return;

        CheckNewAttack();

        if (canAttack == true)
        {
            //If combo ended, stop
            if (currentCombo >= comboMax)
                return;

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
        //TODO: If enemy
        if (true)
        {
            //Camera Shake
            //actionCamera.Shake(1.8f, 1);
            actionCamera.Zoom(0.5f, 48);

            //Hit particle
            //var hitFx = Instantiate(hitFX, transform.position + transform.forward * hitFxOffest + transform.up * .2f, Quaternion.identity);
            //Destroy(hitFx.gameObject, 1);
        }
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
        }
    }

    //Regular combat dash
    public void CombatDash()
    {
        ExecuteDash(comboDashes[currentCombo-1]);

        StartCoroutine(lookTarget.Freeze(0.1f));
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

        targetTilt = 0;
    }

    //Called when "Dash"
    private void OnRoll(InputAction.CallbackContext context)
    {
        if (isDashing)
            return;

        //Trigger dash animation
        animator.SetTrigger("roll");

        //Execute the dash
        ExecuteDash(sprintDash);

        playerAudioManager.AudioPlay(dashSound,.45f,false);

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
        if(isGodMode)
        {
            if(isJumpPressed)
                currentMovement.y = 4;
            else 
                currentMovement.y = 0;

            return;
        }

        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
        } else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    //Character Rotation
    void handleRotation()
    {
        if (!isDashing)
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
        if(isGodMode)
            return;

        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 1.5f;

        //Apply gravity
        //Euler integration (may have to change it to Velocity Verlet. Btw, not causing significant problems rn)
        if (characterController.isGrounded)
        {          
            currentMovement.y = groundedGravity;
        } else if (isFalling) {
            currentMovement.y += gravity * Time.deltaTime * fallMultiplier;
        } else {
            currentMovement.y += gravity * Time.deltaTime;
        }

        //Dashing stops falling
        if (isDashing)
        {
            float Yvelocity = currentMovement.y;

            if (Yvelocity < 0)
                Yvelocity = 0;

            currentMovement.y = Yvelocity;
        }
    }

    private void handlePhysics()
    {
        if (isDashing)
        {
            return;
        }

        Vector2 targetMovement = currentMovementInput;

        //Sprint
        if (isRunPressed)
        {
            targetMovement *= runMultiplier;
        }

        //Interpolation
        smoothMovement = Vector2.SmoothDamp(smoothMovement, targetMovement, ref velocity, easeFactor);

        //Add walk speed
        currentMovement.x = smoothMovement.x * walkSpeed;
        currentMovement.z = smoothMovement.y * walkSpeed;
    }

    private void handleAnimation()
    {
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
        //Handle input
        onMovementInput(playerInput.CharacterControls.Move.ReadValue<Vector2>());
        //Apply rotacion
        handleRotation();
        //Handle Animation
        handleAnimation();
        //Apply Physics
        handlePhysics();

        //Move the character
        characterController.Move(currentMovement * Time.deltaTime);
        
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
    public void ExecuteDash(DashSetting setting)
    {
        if (dashReset != null)
            StopCoroutine(dashReset);

        Vector3 dir;

        dir = new Vector3(lastMovementInput.x, 0, lastMovementInput.y);
        dashReset = Dash(setting.dashTime, setting.dashForce, setting.dashCurve, dir);
        StartCoroutine(dashReset);
        isDashing = true;
        ResetMovement(setting.dashTime);
    }

    //Dash
    public IEnumerator Dash (float duration, float force, AnimationCurve curve, Vector3 direction)
    {
        QuickRotation(new Vector3(lastMovementInput.x, 0, lastMovementInput.y));

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

    //Gizmos
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position, -Vector3.up.normalized * distanceToGround);
        Gizmos.DrawWireSphere(transform.position + distanceToGround, groundedRadius);

        //Wall Detection collider
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + wallDetectionBoxPosition, wallDetectionBoxExtents);

        Gizmos.matrix = transform.localToWorldMatrix;

        //Ledge Height check
        Vector3 startPos = ledgeStartHeight - (Vector3.right * ledgeSpread);
        
        for (int i = 0; i < 3; i++)
        {
            Vector3 linePos = startPos + (Vector3.right * ledgeSpread * i);
            Gizmos.DrawLine(linePos, linePos + Vector3.down * ledgeMaxHeight);
        }

        //Roof Check
        Vector3 roofPos = roofCheckPos;
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