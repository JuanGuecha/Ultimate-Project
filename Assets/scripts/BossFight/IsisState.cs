using System.Collections;
using UnityEngine;

public class IsisState : MonoBehaviour
{
    public Transform player;

    public IsisMovement movement;
    public IsisPowers attacks;
    public Animator animator;

    public float attackDistance = 3f;



    enum State
    {
        Follow,
        Attack,
        Teleporting

    }

    State currentState;

    float attackTimer;
    bool hasAttacked;


    void Start()
    {
        currentState = State.Follow;
        movement.canMove = true;
        StartCoroutine(CastCooldown());
    }

    void Update()
    {
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
        }
    }


    void UpdateFollow()

    {

        movement.MoveTo(player);
        animator.SetBool("IsMoving", true);

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackDistance)
        {
            ChangeState(State.Attack);

        }
    }


    void UpdateAttack()
    {

        animator.SetBool("IsMoving", false);

        attackTimer += Time.deltaTime;

        if (!hasAttacked)
        {
            animator.SetTrigger("Attacks");
            attacks.Melee();

            hasAttacked = true;
        }

        if (attackTimer > 2f && hasAttacked)
        {
            ChangeState(State.Follow);
        }
    }
    void UpdateTeleport()
    {

        if (!attacks.casting)
        {
            animator.SetTrigger("Special");
            animator.SetBool("IsMoving", false);
            attacks.casting = true;
            StartCoroutine(attacks.Castbolts());
            StartCoroutine(movement.TeleportLoop());
        }


    }

    IEnumerator CastCooldown()
    {
        yield return new WaitForSeconds(20f);
        ChangeState(State.Teleporting);
        yield return new WaitForSeconds(20f);
        attacks.casting = false;
        ChangeState(State.Follow);
        StartCoroutine(CastCooldown());

    }


    void ChangeState(State newState)//camia de estado
    {
        currentState = newState;


        switch (newState)
        {
            case State.Attack:
                attackTimer = 0;
                hasAttacked = false;
                movement.canMove = false;
                break;

            case State.Follow:
                movement.canMove = true;
                break;
            case State.Teleporting:
                movement.canMove = false;
                break;
        }
    }
}
