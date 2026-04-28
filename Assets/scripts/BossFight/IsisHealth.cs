using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class IsisHealth : MonoBehaviour
{
    public float health = 400f;
    public Animator IsisAnim;
    [SerializeField] VideoPlayer videoplayer;
    void Start()
    {
        videoplayer.loopPointReached += OnVideoEnd;
        IsisAnim.SetFloat("Health", health);
    }
    public void isisTakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Isis Health: " + health + " took damage: " + amount);
        IsisAnim.SetTrigger("Hurt");
        IsisAnim.SetFloat("Health", health);
    }


    public IEnumerator IsisDies()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        videoplayer.Play();

    }
    void OnVideoEnd(VideoPlayer vp)
    {
        videoplayer.gameObject.SetActive(false);
    }
}
