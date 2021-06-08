using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public GameObject thePlayer;
    public bool isWalking;
    public float horizontalMove;
    public float verticalMove;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 200.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Horizontal") || (Input.GetButton("Vertical")))
        {
            thePlayer.GetComponent<Animation>().Play("cat_armature_walk");
            horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
            verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 8;
            isWalking = true;
            transform.Rotate(0, horizontalMove, 0);
            transform.Translate(0, 0, verticalMove);
        }
        else
        {
            thePlayer.GetComponent<Animation>().Play("cat_armature_idle");
            isWalking = false;
        }
    }

    void Update()
    {
        /*if (Input.GetButton("Horizontal") || (Input.GetButton("Vertical")))
        {
            thePlayer.GetComponent<Animation>().Play("cat_armature_walk");
            horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
            verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 8;
            isWalking = true;
            transform.Rotate(0, horizontalMove, 0);
            transform.Translate(0, 0, verticalMove);
        }
        else
        {
            thePlayer.GetComponent<Animation>().Play("cat_armature_idle");
            isWalking = false;
        }*/

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Acceleration);
            isGrounded = false;
        }
    }
}
