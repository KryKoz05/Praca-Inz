using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poruszanie : MonoBehaviour
{
    public Kontroler kontroler;
    public float predkosc;
    public Joystick joystick;
    private bool skok = false;
    float ruch = 0f;
  

    
    void Update()
    {
        if (joystick.Horizontal >= .2f)
        {
            ruch = predkosc;
        }
        else if( joystick.Horizontal <= -.2f)
        {
            ruch = -predkosc;
        }
        else
        {
            ruch = 0;
        }

        if (joystick.Vertical >= .5f)
        {
            skok = true;
        }
        else
        {
            skok = false;
        }

    }

    void FixedUpdate()
    {

        kontroler.Rusz(ruch * Time.fixedDeltaTime, skok);
        skok = false;
    }
}
