using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    //Serialized
    [SerializeField] float maxSpeed = 7f; //visueel decleraren (ook in Unity)
    [SerializeField] float jumpPower = 330f;
    [SerializeField] Transform GroundTrigger;
    [SerializeField] LayerMask GroundLayer;

    //Movement
    private Animator marioAnimator;
    Rigidbody2D rb; //Parameter Rigidbody2D is rd
    SpriteRenderer sr; // parameter SpriteRenderer wordt sr
    float curSpeed = 0f;
    bool canJump = false;
    bool isGrounded = false;
    public int points;
    private const float GroundTriggerRadius = 0.1f;


    //Health
    public int health = 10;
    public float InvincibleTimeAfterHurt = 2;


    private void Awake() //rb en sr goed instellen
    {
        marioAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() //deze wordt opgeroepen bij een nieuwe update van de scene
    {
        curSpeed = Input.GetAxis("Horizontal") * maxSpeed; //De snelheid is gelijk aan de richting*de maximumsnelheid

        FlipCharacterSpritesX(); //roept de functie FlipCharacterSpritesX op
        marioAnimator.SetFloat("Speed", Mathf.Abs(curSpeed));


        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) //kijk of het personage kan en wil springen
        {
            canJump = true; //spring
        }
    }


    private void FixedUpdate() //wordt opgeroepen bij een nieuwe update van de scene (Vooral gebruiken bij Rigidbody's)
    {
        isGrounded = Physics2D.OverlapCircle(GroundTrigger.position, GroundTriggerRadius, GroundLayer); //vraagt zich af of het personage op de grond staat
        marioAnimator.SetBool("IsGrounded", isGrounded);
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


    void FlipCharacterSpritesX() //De functie om het personage van kant te laten veranderen
    {
        if (sr.flipX && curSpeed > 0) //Als de tegenwoordige snelheid kleiner is als 0 en het personage is nog niet omgedraaid
        {
            sr.flipX = false; //Draai dan om
        }
        else if (!sr.flipX && curSpeed < 0 ) //Als de tegenwoordige snelheid kleiner is als 0 en het personage is al omgedraaid
        {
            sr.flipX = true; //Draai dan terug om
        }
    }


    void Jump() //De functie om te springen
    {
        if (canJump) //als je wilt jumpen
        {
            canJump = false; //Dan kan je niet nog is jumpen
            rb.AddForce(Vector2.up * jumpPower); //versnel aan hand van de y functie * de jumpPower
        }

    }

    public void TriggerHurt(float hurtTime)
    {
        StartCoroutine(HurtBlinker(hurtTime));
    }

    IEnumerator HurtBlinker(float hurtTime)
    {
        //Ignore collision with enemies
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);

        //Start looping blink animation
        marioAnimator.SetLayerWeight(1, 1);

        //wait
        yield return new WaitForSeconds(hurtTime);

        //Stop blinking and re-enable collision
        marioAnimator.SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
    }

    void Hurt()
    {
        health--;
        if(health <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            TriggerHurt(InvincibleTimeAfterHurt);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Patrol enemy = collision.collider.GetComponent<Patrol>();
        if (enemy != null)
        {
            Hurt();
        }
    }
}

    