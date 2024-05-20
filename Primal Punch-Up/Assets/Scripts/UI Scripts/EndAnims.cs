using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnims : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void PlayAnim1()
    {
        animator1.Play("ArrowMove");
    }

    public void PlayAnim2()
    {
        animator2.Play("ArrowMove");
    }

    public void PlayAnim3()
    {
        animator3.Play("ArrowMove");
    }
}
