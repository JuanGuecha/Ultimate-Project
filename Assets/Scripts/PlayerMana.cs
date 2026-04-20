using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;

    public float regenerationRate = 20f;

    public ManaUI manaUI;

    void Start()
    {
        currentMana = maxMana;

        if (manaUI != null)
        {
            manaUI.SetMaxMana(maxMana);
            manaUI.UpdateMana(currentMana);
        }
        else
        {
            Debug.LogError("ManaUI no está asignado en PlayerMana");
        }
    }

    void Update()
    {
        RegenerateMana();
    }

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += regenerationRate * Time.deltaTime;

            if (currentMana > maxMana)
                currentMana = maxMana;

            manaUI.UpdateMana(currentMana);
        }
    }

    public bool UseMana(float amount)
    {
        Debug.Log("Mana actual: " + currentMana + " | Costo: " + amount);

        if (currentMana >= amount)
        {
            currentMana -= amount;

            if (currentMana < 0f)
                currentMana = 0f;

            manaUI.UpdateMana(currentMana);

            Debug.Log("Se gastó mana. Nuevo mana: " + currentMana);
            return true;
        }

        Debug.Log("No hay suficiente mana");
        return false;
    }
}