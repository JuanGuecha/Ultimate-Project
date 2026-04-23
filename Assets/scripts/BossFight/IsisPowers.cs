using System.Collections;
using UnityEngine;

public class IsisPowers : MonoBehaviour
{
    public bool casting;

    public Transform boltsParent; // contenedor de los hijos

    private GameObject[] bolts;

    void Start()
    {

        int count = boltsParent.childCount;
        bolts = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            bolts[i] = boltsParent.GetChild(i).gameObject;
        }
    }
    public void Melee()
    {
        Debug.Log("Melee");
    }

    public IEnumerator Castbolts()
    {

        if (casting)
        {
            for (int i = 0; i < bolts.Length; i += 2)
            {
                // activar de 2 en 2
                bolts[i].SetActive(true);

                if (i + 1 < bolts.Length)
                    bolts[i + 1].SetActive(true);

                yield return new WaitForSeconds(2f);
            }
        }
    }
}
