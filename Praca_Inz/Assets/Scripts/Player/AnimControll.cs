using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControll : MonoBehaviour
{
    public Movement moving;
    public Animator anim;
    public Controller c;

    
    void FixedUpdate()
    {
        anim.SetFloat("Speed", Mathf.Abs(moving.movement));
        if (c.gorundCheck)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }
    }
}
