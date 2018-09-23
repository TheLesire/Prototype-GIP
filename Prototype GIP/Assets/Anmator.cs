public float speed = 1.0f;
public float jumpSpeed = 0.5f;
public LayerMask groundLayer;

private Animator marioAnimator;
private Transform gCheck;
private float scaleX = 1.0f;
private float scaleY = 1.0f;

void Start()
{
    marioAnimator = GetComponent();
    gCheck = transform.FindChild("GCheck");

}

void FixedUpdate()
{
    float speed = 1.0f;
    float mSpeed = Input.GetAxis("Horizontal");
    marioAnimator.SetFloat("Speed", Mathf.Abs(mSpeed));
    bool isTouched = Physics2D.OverlapPoint(gCheck.position, groundLayer);

    if (Input.GetKey(KeyCode.Space))
    {

        if (isTouched)
        {
            rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);
            isTouched = false;
        }
    }
    marioAnimator.SetBool("isTouched", isTouched);

    if (mSpeed & gt; 0){
        transform.localScale = new UnityEngine.Vector2(scaleX, scaleY);
    }
		else if (mSpeed & lt; 0){
        transform.localScale = new UnityEngine.Vector2(-scaleX, scaleY);
    }

    this.rigidbody2D.velocity = new UnityEngine.Vector2(mSpeed * speed, rb = GetComponent<rigidbody2D>(velocity.y);