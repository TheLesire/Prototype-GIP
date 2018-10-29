using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 7f; //visueel decleraren (ook in Unity)
    [SerializeField] float jumpPower = 330f;
    [SerializeField] Transform GroundTrigger;
    [SerializeField] float GroundTriggerRadius = 1f;
    [SerializeField] LayerMask GroundLayer;


    private Animator marioAnimator;
    Rigidbody2D rb; //Parameter Rigidbody2D is rd
    SpriteRenderer sr; // parameter SpriteRenderer wordt sr
    float curSpeed = 0f;
    bool jump = false;
    bool isGrounded = false;

    
    private void Awake() //rb en sr goed instellen
    {
        marioAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() //deze wordt opgeroepen bij een nieuwe update van de scene
    {
        curSpeed = Input.GetAxis("Horizontal") * maxSpeed; //De snelheid is gelijk aan de richting*de maximumsnelheid

        ChangeDirection(); //roept de functie ChangeDirection op
        marioAnimator.SetFloat("Speed", Mathf.Abs(curSpeed));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //kijk of het personage kan en wil springen
        {
            jump = true; //srping
        }
  
    }


    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundTrigger.position, GroundTriggerRadius, GroundLayer); //vraagt zich af of het personage op de grond staat
        Move(); //roept de functie bewegen op   
        Jump(); //roept de functie springen op
    }


    private void OnDrawGizmosSelected() //geeft de groundtrigger een kleur en cirkel
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GroundTrigger.position, GroundTriggerRadius);
    }
    private void Move() //De functie om te bewegen
    {
        rb.velocity = new Vector2(curSpeed, rb.velocity.y); //De snelheid is gelijk aan de parameters tegenwoordigesnelheid en de snelheid op de y as
    }


    void ChangeDirection() //De functie om het personage van kant te laten veranderen
    {
        if( curSpeed > 0 && sr.flipX ) //Als de tegenwoordige snelheid kleiner is als 0 en het personage is nog niet omgedraaid
        {
            sr.flipX = false; //Draai dan om
        }
        else if( curSpeed < 0 && !sr.flipX) //Als de tegenwoordige snelheid kleiner is als 0 en het personage is al omgedraaid
        {
            sr.flipX = true; //Draai dan terug om
        }
    }


    void Jump() //De functie om te springen
    {
        if(jump) //als je wilt jumpen
        {
            jump = false; //Dan kan je niet nog is jumpen
            rb.AddForce(Vector2.up * jumpPower); //versnel aan hand van de y functie * de jumpPower
            marioAnimator.SetBool("isTrigger", jump);
        }

        
    }
}

    