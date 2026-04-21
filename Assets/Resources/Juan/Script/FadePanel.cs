using Unity.VisualScripting;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void animationPanel()
    {
        anim.SetTrigger("Faded");
    }
}
