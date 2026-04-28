using UnityEngine;
using System.Collections;

public class IsisState : MonoBehaviour
{
    public Transform player;
    public IsisMovement movement;
    public IsisPowers powers;
    public IsisHealth isishealth;
    public Animator animator;
    bool reachedJump;
    bool hasJumped;
    public float attackDistance = 3f;
    float RayCastingCooldown = 20f;
    float RayCastingTimer;
    float attackTimer;
    bool hasAttacked;
    bool hasdied;
    bool isCastingRay = false;
    bool usedbolts = false;
    bool hasUsedPillars = false;


    // TELEPORT
    float teleportTimer;
    float teleportDuration = 20f;
    bool startedTeleport;

    // COOLDOWN
    float castCooldown = 65f;
    public float castCooldownTimer;
    bool castedRay = false;

    enum State
    {
        Follow,
        Attack,
        Teleporting,
        CastingRay,
        PreparingRay,
        Dying,


    }

    State currentState;

    // ATTACK


    void Start()
    {
        ChangeState(State.Follow);
    }

    void Update()
    {

        castCooldownTimer += Time.deltaTime;
        RayCastingTimer += Time.deltaTime;

        switch (currentState)
        {
            case State.Follow:
                UpdateFollow();
                break;

            case State.Attack:
                UpdateAttack();
                break;

            case State.Teleporting:
                UpdateTeleport();
                break;
            case State.Dying:
                UpdateDying();
                break;
            case State.PreparingRay:
                UpdatePreparingRay();
                break;
            case State.CastingRay:
                UpdateCastingRay();
                break;

        }
    }

    //  FOLLOW
    void UpdateFollow()
    {
        movement.MoveTo(player);
        animator.SetBool("IsMoving", true);

        float dist = Vector2.Distance(transform.position, player.position);
        //Mas prioridad aun, morirse
        if (isishealth.health < 0.1f)
        {
            ChangeState(State.Dying);
        }
        if (RayCastingTimer >= RayCastingCooldown && isishealth.health < 300f && castedRay == false)
        {

            ChangeState(State.PreparingRay);
            return;
        }

        // prioridad: habilidad especial
        if (castCooldownTimer >= castCooldown)
        {
            ChangeState(State.Teleporting);
            return;
        }

        if (dist <= attackDistance)
        {
            ChangeState(State.Attack);
        }
    }

    //  ATTACK
    void UpdateAttack()
    {
        if (isishealth.health < 0.1f)
        {
            ChangeState(State.Dying);
        }
        animator.SetBool("IsMoving", false);

        attackTimer += Time.deltaTime;

        if (!hasAttacked)
        {
            animator.SetTrigger("Attacks");
            StartCoroutine(powers.Melee());
            hasAttacked = true;
        }

        if (attackTimer > 2f)
        {
            ChangeState(State.Follow);
        }
    }

    // TELEPORT + BOLTS
    void UpdateTeleport()
    {

        if (isishealth.health < 0.1f)
        {
            ChangeState(State.Dying);
        }
        teleportTimer += Time.deltaTime;
        if (Vector2.Distance(transform.position, movement.CastingRayPoint.transform.position) < 0.5f)
        {
            StartCoroutine(powers.CastRay());
        }
        if (usedbolts && !hasUsedPillars)
        {
            hasUsedPillars = true;
            StartCoroutine(powers.CastPilars());
        }

        if (!startedTeleport)
        {
            startedTeleport = true;

            animator.SetTrigger("Special");
            animator.SetBool("IsMoving", false);

            movement.isTeleporting = true;

            StartCoroutine(powers.Castbolts());
            StartCoroutine(movement.TeleportLoop());
            usedbolts = true;
        }


        if (teleportTimer > teleportDuration)
        {
            movement.isTeleporting = false;
            castCooldownTimer = 0;

            ChangeState(State.Follow);
        }
    }

    void UpdateCastingRay()
    {
        if (isishealth.health < 0.1f)
        {
            ChangeState(State.Dying);
        }

        if (!isCastingRay)
        {
            isCastingRay = true;
            StartCoroutine(RayLoop());
        }
    }

    void UpdatePreparingRay()
    {
        if (isishealth.health < 0.1f)
        {
            ChangeState(State.Dying);
        }
        // IR AL PUNTO DE SALTO
        if (!reachedJump)
        {
            movement.movingToJump();

            if (Vector2.Distance(transform.position, movement.JumpPoint.transform.position) < 0.1f)
            {
                transform.position = movement.JumpPoint.transform.position;
                reachedJump = true;
            }

            return;
        }

        // SALTO
        if (!hasJumped)
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("Jump");


            transform.position = movement.CastingRayPoint.transform.position;

            hasJumped = true;

            ChangeState(State.CastingRay);
            return;
        }
    }
    void UpdateDying()
    {
        if (!hasdied)
        {
            hasdied = true;
            StartCoroutine(isishealth.IsisDies());
        }
    }
    IEnumerator RayLoop()
    {
        int shots = 5; // cuantos rayos quieres

        for (int i = 0; i < shots; i++)
        {
            animator.SetTrigger("Special");
            yield return StartCoroutine(powers.CastRay());

            yield return new WaitForSeconds(1f); // tiempo entre rayos
        }

        castedRay = true;
        isCastingRay = false;

        ChangeState(State.Follow);
    }


    // CAMBIO DE ESTADO
    void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Follow:
                movement.canMove = true;
                break;

            case State.Attack:
                movement.canMove = false;
                attackTimer = 0;
                hasAttacked = false;
                break;

            case State.Teleporting:
                movement.canMove = false;
                teleportTimer = 0;
                startedTeleport = false;
                break;
            case State.PreparingRay:
                movement.canMove = true;
                reachedJump = false;
                hasJumped = false;
                break;
            case State.CastingRay:

                break;
            case State.Dying:
                movement.canMove = false;
                break;


        }
    }
}