using UnityEngine;
using UnityEngine.Video;

public class Videomanag : MonoBehaviour
{
    [SerializeField] VideoPlayer videoplayer;
    [SerializeField] VideoClip video;

    [SerializeField] GameObject teleplats;
    [SerializeField] mummy followplayer;
    [SerializeField] GameObject dialogo;



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
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }
    }



    void OnVideoEnd(VideoPlayer vp)
    {
        videoplayer.gameObject.SetActive(false);
        teleplats.SetActive(true);
        followplayer.enabled = true;
        dialogo.SetActive(true);
        Time.timeScale = 1;
    }
}
