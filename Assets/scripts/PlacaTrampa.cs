using UnityEngine;

public class PlacaTrampa : MonoBehaviour
{
    public GameObject trampa;
    void Start()
    {
        trampa.SetActive(false);
    }

    void OncollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trampa.SetActive(true);
        }
    }


}
