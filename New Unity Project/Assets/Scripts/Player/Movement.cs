using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Controller c;
    public float v;
    
    private bool jump = false;
    public  float movement = 0f;
   

    
    void Update()
    {

        movement = Input.GetAxis("Horizontal") * v;


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {

        c.Move(movement * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
