using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Controller c;
    public float v;
    public Joystick joystick;
    private bool jump = false;
    public  float movement = 0f;
   

    
    void Update()
    {
    
        if (joystick.Horizontal >= .2f)
        {
            movement = v;
           
        }
        else if( joystick.Horizontal <= -.2f)
        {
            movement = -v;
            
        }
        else
        {
            movement = 0;
            
        }

        if (joystick.Vertical >= .5f)
        {
            jump = true;
 
        }
        else
        {
            jump = false;
           
        }

    }

    void FixedUpdate()
    {

        c.Move(movement * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
