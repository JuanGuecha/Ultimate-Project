using System.Collections;
using UnityEngine;

public class IsisPowers : MonoBehaviour
{
    public GameObject boltsPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Castbolts());
    }

    // Update is called once per frame
    IEnumerator Castbolts()
    {
        yield return new WaitForSeconds(5f);
        boltsPrefab.SetActive(true);
    }
}
