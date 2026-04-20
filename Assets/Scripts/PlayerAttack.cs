using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMana playerMana;

    public float meleeCost = 15f;
    public float rangedCost = 25f;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        if (playerInput.actions["MeleeAttack"].triggered)
        {
            MeleeAttack();
        }

        if (playerInput.actions["RangedAttack"].triggered)
        {
            RangedAttack();
        }
    }

    void MeleeAttack()
    {
        if (playerMana.UseMana(meleeCost))
        {
            Debug.Log("Ataque cuerpo a cuerpo");
        }
        else
        {
            Debug.Log("No hay suficiente mana");
        }
    }

    void RangedAttack()
    {
        if (playerMana.UseMana(rangedCost))
        {
            Debug.Log("Ataque a distancia");
        }
        else
        {
            Debug.Log("No hay suficiente mana");
        }
    }
}