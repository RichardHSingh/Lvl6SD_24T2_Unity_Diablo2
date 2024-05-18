using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private float horizontalInput;
    private Rigidbody2D body;
    private Animator anime;
    //private bool grounded;
    private BoxCollider2D boxCollider;
    private bool isSquatting = false;


    // Start is called before the first frame update
    void Start()
    {
        // References to grab 
        // [ first = rigidBody | second = animator | third = boxCollider]
        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        //======== METHOD FOR CHAR DIRECTION ========
        // flips player when moving either direction
        // right movement direction
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 6, 1);

        // left movement direction
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 6, 1);

        // allows for player to jump using space bar
        // grounded = only allows to jump once on ground
        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();
        
        // if (Input.GetKeyDown(KeyCode.S))
        //     isSquatting()

        // setting animator parameter
        anime.SetBool("run", horizontalInput != 0);
        anime.SetBool("grounded", isGrounded());
    }

    //======== METHOD FOR CHAR JUMP ========
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anime.SetTrigger("jump");
        //grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.tag == "Ground")
        //     grounded = true;
    }

    //======== METHOD FOR CHAR BEING GROUNDED ========
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    public bool canAttack()
    {
        return isGrounded();
    }
   
//    public bool isSquatting();
//    {

//    }
}  
 