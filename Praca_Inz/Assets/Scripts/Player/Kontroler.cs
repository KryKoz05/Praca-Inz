using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Kontroler : MonoBehaviour
{
    
    public Transform sprawdz;
    public bool naZiemi;
    private Vector3 predkosc = Vector3.zero;
    private Rigidbody2D rb;
    public Joystick joystick;
    public bool skok = false;
    public LayerMask CoJestPodlozem;
    const float promienSprawdzenia = .4f;
    private bool prawo = true;
    [SerializeField] private float silaSkoku = 400f;
    private float wygladzenie = .005f;
    [Header("Events")]
    [Space]

    public UnityEvent ladowanie;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (ladowanie == null)
        {
            ladowanie = new UnityEvent();
        }
    }
    
    void FixedUpdate()
    {
        bool bnZ = naZiemi;
        naZiemi = false;

        
        Collider2D[] kolizje = Physics2D.OverlapCircleAll(sprawdz.position, promienSprawdzenia, CoJestPodlozem);
        for (int i = 0; i < kolizje.Length; i++)
        {
            if (kolizje[i].gameObject != gameObject)
            {
                naZiemi = true;
                if (!bnZ)
                    ladowanie.Invoke();
            }
        }
        
    }

    public void Rusz(float kierunek, bool skok)
    {


        if (naZiemi || skok || !naZiemi&&!skok)
        {



            
            Vector3 targetVelocity = new Vector2(kierunek * 10f, rb.velocity.y);
            
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref predkosc, wygladzenie);

            
            if (kierunek > 0 && !prawo)
            {
                
                Obrot();
            }
            
            else if (kierunek < 0 && prawo)
            {
                
                Obrot();
            }

            
            if (naZiemi && skok)
            {
                
                naZiemi = false;
                rb.AddForce(new Vector2(0f, silaSkoku));
            }
        }
    } 


        private void Obrot()
        {
            
            prawo = !prawo;

            
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    
}
