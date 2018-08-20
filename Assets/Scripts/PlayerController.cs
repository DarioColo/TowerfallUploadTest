using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public float myLife;
    public CharacterController controller;

    #region Movement Variables
    public Vector3 moveVector;
    public float moveSpeed;
    public float slowSpeedCharge;
    public float maxSpeedChargeTimer;
    public float verticalVelocity;

    public float gravity;

    public float jumpForce = 20;
    public bool isJumping;
    public bool isFalling;
    public bool canJump;
    public bool delayJump;

    public bool isFallingOff;
    public float fallOffSpeed;

    public bool coyoteBool;
    public float coyoteTime;
    #endregion

    #region Attack Variables
    public float weaponExtends;

    public float impactVelocityNormal;
    public float defaultAttackNormal;
    public float normalAttackCoolDown;
    public float influenceOfMovementNormal;


    public float impactVelocityUp;
    public float defaultAttackUp;
    public float upAttackCoolDown;
    public float influenceOfMovementUp;

    public float impactVelocityDown;
    public float defaultAttackDown;
    public float downAttackCoolDown;
    public float influenceOfMovementDown;
    #endregion

    #region Dash Variables
    public float dashSpeed = 50;
    public float dashDistance = 7;
    public float dashCoolDown;
    public bool isDashing;
    public bool canDash = true;
    #endregion

    #region ImpactVariables
    public Vector3 impactVelocity;

    public float impactSpeed = 20;
    public float maxImpactToInfinitStun;
    public float impactStunMaxTimer;
    public float currentImpactStunTimer;
    public float residualStunImpact;

    public float hitHeadReject;
    public float maxNoStunVelocityLimit;
    public float maxStunVelocityLimit;

    bool canAttack;

    public ParticleSystem hitParticles;
    #endregion

    private IMove _iMove;

    public Collider attackColliders;

    #region Dictionarys
    private Dictionary<string, IMove> myMoves = new Dictionary<string, IMove>();
    private Dictionary<string, IAttack> attacks = new Dictionary<string, IAttack>();
    private Dictionary<string, IHability> hability = new Dictionary<string, IHability>();
    #endregion

    public PlayerController whoHitedMe;
    public PlayerController whoIHited;
    public bool isDead;
    public bool stunned;

    public bool stunnedByGhost;
    private float maxStunnedGhost;
    private float currentStunnedGhost;

    public float chargedEffect;
    public bool isCharged;
    public bool hitCharged;

    public LifeUI myLifeUI;

    public ParticleSystem PS_Stunned; //Podría ser unos signos de pregunta o estrellitas arriba de la cabeza.
    public ParticleSystem PS_Marked;
    public ParticleSystem PS_Charged;
    public ParticleSystem PS_Fall;
    public Animator myAnim;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        myAnim = GetComponent<Animator>();
    }

    void Start()
    {
        coyoteTime = 0.1f;
        SetMovements();
        SetAttacks();
        SetHabilities();
        myLifeUI.maxLife = myLife;
        maxStunnedGhost = 5f;
        StartCoroutine(CanAttack(0.1f));
    }

    void Update()
    {
        PhysicsOptions();
        if (!GameManager.Instance.finishedGame && !GameManager.Instance.startingGame)
            Move();
        UpdateHabilities();
        Attack();
        StunAndMark();

        if (!stunnedByGhost)
            controller.Move(moveVector * Time.deltaTime);
    }

    #region Moves & Jump
    public void Move()
    {
        if (!stunnedByGhost)
        {
            if (stunned)
            {
                moveVector.x = impactVelocity.x;
                moveVector.y = impactVelocity.y;
            }
            else
            {
                foreach (var m in myMoves.Values)
                    m.Update();
                if ((canJump && controller.isGrounded && !isFallingOff) || (delayJump && controller.isGrounded) || coyoteBool && canJump)
                {
                    myMoves["Jump"].Move();
                    canJump = false;
                    delayJump = false;
                    coyoteBool = false;
                }
                else if (!isDashing && !isFallingOff)
                    myMoves["HorizontalMovement"].Move();
            }
        }
        else
        {
            if (!PS_Stunned.isPlaying)
                PS_Stunned.Play();
        }
    }

    public void Jump()
    {
        canJump = true;
        if (isFalling && IsCloseToGround())
            delayJump = true;
    }
    #endregion

    #region PhysicsOptions

    #region PhysicsChecks
    void PhysicsOptions()
    {
        PhysicsChecks();
        if (controller.isGrounded)
            StartCoroutine(CoyoteTime(coyoteTime));
    }

    void PhysicsChecks()
    {
        if (verticalVelocity < 0 && !controller.isGrounded)
            isFalling = true;
        else
            isFalling = false;
    }

    bool IsCloseToGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.5f);
    }

    IEnumerator CoyoteTime(float timer)
    {
        while (true)
        {
            bool grounded = controller.isGrounded;
            yield return new WaitForSeconds(timer);
            if (grounded != controller.isGrounded && !isJumping)
            {
                coyoteBool = true;
                yield return new WaitForSeconds(timer * 2);
                coyoteBool = false;
            }
            break;
        }
    }

    #endregion


    public void SmoothHitRefleject()
    {
        moveVector = Vector3.zero;
        //verticalVelocity -= gravity * Time.deltaTime * 2;
        //impactVelocity.x = Mathf.Sign(moveVector.x) * 3f;
        //impactVelocity.y = verticalVelocity;
        myAnim.Play("HitOnWall");
    }
    #endregion

    #region Stun & Mark
    void StunAndMark()
    {
        if (stunned)
            StunUpdate();
        if (stunnedByGhost)
            StunGhostUpdate();
    }

    void StunUpdate()
    {
        currentImpactStunTimer += Time.deltaTime;
        if (currentImpactStunTimer > impactStunMaxTimer)
        {
            whoHitedMe = null;
            stunned = false;
            myAnim.SetBool("Stunned", false);
            currentImpactStunTimer = 0;
            impactVelocity = Vector3.zero;
        }
    }

    void StunGhostUpdate()
    {
        currentStunnedGhost += Time.deltaTime;
        if (currentStunnedGhost > maxStunnedGhost)
        {
            stunnedByGhost = false;
            currentStunnedGhost = 0;
            PS_Stunned.Stop();
        }
    }

    public void WhoHitedMe(PlayerController pl)
    {
        whoHitedMe = pl;
    }
    #endregion

    #region Attacks
    void Attack()
    {
        if (isCharged)
            Charged();
        foreach (var a in attacks.Values)
            a.Update();
    }

    public void AttackNormal(string state)
    {
        if (!stunned && canAttack)
        {
            if (state == "Realese")
            {
                GetComponent<ParticlePuños>().PuñoAActivar("recto");
                attacks["NormalAttack"].Attack(attackColliders);
                StartCoroutine(CanAttack(0.1f));
            }
            else
            {
                attacks["NormalAttack"].Pressed();
            }
        }
    }

    public void AttackDown(string state)
    {
        if (!stunned && canAttack)
        {
            if (state == "Realese")
            {
                GetComponent<ParticlePuños>().PuñoAActivar("abajo");
                attacks["DownAttack"].Attack(attackColliders);
                StartCoroutine(CanAttack(0.1f));
            }
            else
            {
                attacks["DownAttack"].Pressed();
            }
        }
    }

    public void AttackUp(string state)
    {
        if (!stunned && canAttack)
        {
            if (state == "Realese")
            {
                GetComponent<ParticlePuños>().PuñoAActivar("arriba");
                attacks["UpAttack"].Attack(attackColliders);
                StartCoroutine(CanAttack(0.1f));
            }
            else
            {
                attacks["UpAttack"].Pressed();
            }
        }
    }

    IEnumerator CanAttack(float x)
    {
        while (true)
        {
            canAttack = false;
            yield return new WaitForSeconds(x);
            canAttack = true;
            break;
        }
    }

    void UpdateHabilities()
    {
        foreach (var h in hability.Values)
            h.Update();
    }
    #endregion

    #region Habilities

    public void Dash()
    {
        if (canDash)
            hability["Dash"].Hability();
    }

    public void FallOff()
    {
        if (!controller.isGrounded && !stunned && !isDashing)
            myMoves["FallOff"].Move();
    }

    void Charged()
    {
        if (!PS_Charged.isPlaying)
            PS_Charged.Play();
    }

    private void LateUpdate()
    {
        if (hitCharged)
        {
            isCharged = false;
            hitCharged = false;
            PS_Charged.Stop();
        }
    }
    #endregion

    #region ReceiveDamage
    public void ReceiveDamage(Vector3 impact)
    {
        Vector3 impactRelax = Vector3.zero;

        if (stunned)
        {
            UpdateMyLife(50);
        }
        else
        {
            UpdateMyLife(10);
        }
        if (stunned && currentImpactStunTimer > 0.1f)
        {
            impactRelax = (impactVelocity.magnitude / residualStunImpact) * impact;
            impactRelax = new Vector3(Mathf.Abs(impactRelax.x) > maxStunVelocityLimit ? Mathf.Sign(impactRelax.x) * maxStunVelocityLimit : impactRelax.x, Mathf.Abs(impactRelax.y) > maxStunVelocityLimit ? Mathf.Sign(impactRelax.y) * maxStunVelocityLimit : impactRelax.y, 0);
            impactVelocity = impactRelax;
            SetImpacts();
        }
        else
        {
            impactRelax = impact;
            if (impact.magnitude >= maxNoStunVelocityLimit)
                impactRelax = new Vector3(Mathf.Abs(impactRelax.x) != 0 ? Mathf.Sign(impactRelax.x) * maxNoStunVelocityLimit : 0, Mathf.Abs(impactRelax.y) != 0 ? Mathf.Sign(impactRelax.y) * maxNoStunVelocityLimit : 0, 0);
            impactVelocity = impactRelax;
            SetImpacts();
            stunned = true;
            myAnim.SetBool("Stunned", true);
        }
        if (myLife > 0)
        {
            if (impact.x != 0)
                myAnim.Play("GetHit");
            else if (impact.y < 0 && !myAnim.GetBool("Grounded"))
                myAnim.Play("GetHitDown");
            else if (impact.y > 0)
                myAnim.Play("GetHitUp");
        }
        currentImpactStunTimer = 0;
    }


    #endregion

    #region Collisions, Colliders, Triggers
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        var dir = Vector3.Dot(transform.up, hit.normal);
        if (!controller.isGrounded && dir == -1)
        {
            if (!stunned)
            {
                verticalVelocity = -hitHeadReject;
                moveVector.y = verticalVelocity;
            }
            else
            {
                verticalVelocity = -hitHeadReject;
                moveVector.y = verticalVelocity;
                stunned = false;
                myAnim.SetBool("Stunned", false);
            }
        }
        else if (Vector3.Angle(transform.up, hit.normal) >= 90)
        {
            if (stunned)
            {
                SmoothHitRefleject();
                stunned = false;
                myAnim.SetBool("Stunned", false);
            }
        }
        else
        {
            canJump = false;
            if (stunned && impactVelocity.y < 0)
            {
                impactVelocity = Vector3.zero;
                moveVector = Vector3.zero;
                stunned = false;
                myAnim.SetBool("Stunned", false);
            }
            else if (!stunned)
                impactVelocity = Vector3.zero;
            if (isFallingOff)
            {
                //PS_Fall.Play();
                isFallingOff = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PowerUp"))
        {
            chargedEffect = other.GetComponent<IPowerUp>().PowerUp();
            isCharged = true;
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region UpdateLife
    public void UpdateMyLife(float damage)
    {
        myLife -= Mathf.RoundToInt(damage);
        myLifeUI.TakeDamage(Mathf.RoundToInt(damage));
        if (myLife <= 0)
        {
            myAnim.Play("Death");
            StartCoroutine(Death(3f));
            Destroy(gameObject, 3f);
        }
    }

    IEnumerator Death(float x)
    {
        while (true)
        {
            yield return new WaitForSeconds(x - 0.1f);
            isDead = true;
        }
    }

    #endregion

    #region Sets()
    private void SetMovements()
    {
        myMoves.Add(typeof(HorizontalMovement).ToString(), new HorizontalMovement(this));
        myMoves.Add(typeof(Jump).ToString(), new Jump(this));
        myMoves.Add(typeof(FallOff).ToString(), new FallOff(this));
    }

    private void SetAttacks()
    {
        attacks.Add(typeof(NormalAttack).ToString(), new NormalAttack(this, normalAttackCoolDown));
        attacks.Add(typeof(UpAttack).ToString(), new UpAttack(this, upAttackCoolDown));
        attacks.Add(typeof(DownAttack).ToString(), new DownAttack(this, downAttackCoolDown));
        CanAttack(0.1f);
    }

    private void SetHabilities()
    {
        hability.Add(typeof(Dash).ToString(), new Dash(this, dashCoolDown));
    }

    private void SetImpacts()
    {
        impactSpeed = Mathf.Abs(impactVelocity.magnitude);
        if (impactSpeed >= maxImpactToInfinitStun)
            impactStunMaxTimer = 100;
        else
            impactStunMaxTimer = impactSpeed / maxImpactToInfinitStun;
    }
    #endregion
}
