using System.Collections;
using UnityEngine;

public class IsisPowers : MonoBehaviour
{

    public Transform boltsParent; // contenedor de los hijos
    public Transform player;
    public LineRenderer isislineRenderer;
    private GameObject[] bolts;
    float isisrayDistance = 20f;
    public GameObject meleeattack;
    public LineRenderer pillarLineRenderer;

    public Transform pillarsParent;
    private Transform[] pillarStarts;
    private Transform[] pillarEnds;

    void Start()
    {

        int count = boltsParent.childCount;
        bolts = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            bolts[i] = boltsParent.GetChild(i).gameObject;
        }
        int pillarCount = pillarsParent.childCount;

        pillarStarts = new Transform[pillarCount];
        pillarEnds = new Transform[pillarCount];

        for (int i = 0; i < pillarCount; i++)
        {
            Transform pillar = pillarsParent.GetChild(i);

            pillarStarts[i] = pillar.GetChild(0); // Start
            pillarEnds[i] = pillar.GetChild(1);   // End
        }
    }
    public IEnumerator Melee()
    {
        Debug.Log("Melee");
        yield return new WaitForSeconds(0.9f);
        meleeattack.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        meleeattack.SetActive(false);
    }

    public IEnumerator Castbolts()
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
    public IEnumerator CastRay()

    {
        Vector2 origin = transform.position;
        Vector2 direction = (player.position - transform.position).normalized;
        yield return StartCoroutine(warningRay(origin, direction));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(damageRay(origin, direction));
    }
    public IEnumerator warningRay(Vector2 origin, Vector2 isisraydirection)
    {
        float isisrayDistance = 20f;
        isislineRenderer.startWidth = 0.1f;
        isislineRenderer.endWidth = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(origin, isisraydirection, isisrayDistance, LayerMask.GetMask("Player"));

        Debug.DrawRay(origin, isisraydirection * isisrayDistance, Color.red, 1f);

        // Visual del rayo 
        isislineRenderer.SetPosition(0, origin);

        if (hit.collider != null)
            isislineRenderer.SetPosition(1, origin + isisraydirection * (isisrayDistance + 100f));
        else
            isislineRenderer.SetPosition(1, origin + isisraydirection * (isisrayDistance + 100f));

        isislineRenderer.enabled = true;

        yield return new WaitForSeconds(0.5f);

        isislineRenderer.enabled = false;
    }
    public IEnumerator damageRay(Vector2 origin, Vector2 isisraydirection)
    {
        isislineRenderer.startWidth = 2f;
        isislineRenderer.endWidth = 4f;
        RaycastHit2D hit = Physics2D.Raycast(origin, isisraydirection, isisrayDistance, LayerMask.GetMask("Player"));

        Debug.DrawRay(origin, isisraydirection * isisrayDistance, Color.yellow, 1f);

        if (hit.collider != null)
        {
            Debug.Log("Golpeó a: " + hit.collider.name);


            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(1);
            }
        }
        else
        {
            Debug.Log("No golpeó nada");
        }

        // Visual del rayo
        isislineRenderer.SetPosition(0, origin);

        if (hit.collider != null)
            isislineRenderer.SetPosition(1, origin + isisraydirection * (isisrayDistance + 100f));
        else
            isislineRenderer.SetPosition(1, origin + isisraydirection * isisrayDistance);

        isislineRenderer.enabled = true;

        yield return new WaitForSeconds(0.5f);

        isislineRenderer.enabled = false;
    }
    public IEnumerator CastPilars()
    {
        for (int i = 0; i < pillarStarts.Length; i++)
        {
            yield return StartCoroutine(WarningPillar(pillarStarts[i], pillarEnds[i]));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(DamagePillar(pillarStarts[i], pillarEnds[i]));
        }
    }
    IEnumerator WarningPillar(Transform start, Transform end)
    {
        pillarLineRenderer.startWidth = 0.2f;
        pillarLineRenderer.endWidth = 0.2f;

        pillarLineRenderer.SetPosition(0, start.position);
        pillarLineRenderer.SetPosition(1, end.position);


        pillarLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.6f);

        pillarLineRenderer.enabled = false;
    }
    IEnumerator DamagePillar(Transform start, Transform end)
    {
        pillarLineRenderer.startWidth = 2f;
        pillarLineRenderer.endWidth = 4f;


        pillarLineRenderer.SetPosition(0, start.position);
        pillarLineRenderer.SetPosition(1, end.position);

        pillarLineRenderer.enabled = true;


        RaycastHit2D hit = Physics2D.Linecast(
            start.position,
            end.position,
            LayerMask.GetMask("Player")
        );

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            hit.collider.GetComponent<PlayerHealth>().TakeDamage(1);
        }

        yield return new WaitForSeconds(0.4f);

        pillarLineRenderer.enabled = false;
    }
}
