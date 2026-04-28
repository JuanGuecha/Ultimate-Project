using UnityEngine;
using UnityEngine.Video;

public class Videomanag : MonoBehaviour
{
    [SerializeField] VideoPlayer videoplayer;
    [SerializeField] VideoClip video;
    void Start()
    {
        videoplayer.clip = video;
        videoplayer.loopPointReached += OnVideoEnd;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        videoplayer.clip = video;
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            videoplayer.gameObject.SetActive(true);
            videoplayer.Play();
        }
    }



    void OnVideoEnd(VideoPlayer vp)
    {
        videoplayer.gameObject.SetActive(false);
    }
}
