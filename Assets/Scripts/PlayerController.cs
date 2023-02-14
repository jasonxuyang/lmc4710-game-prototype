using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerController : NetworkBehaviour
{
    // Attributes
    public float speed = 20.0f;
    private Vector2 movement = Vector2.zero;

    // Components
    private Rigidbody2D rb;
    private Animator anim;

    // Prevent other from controlling your character(s)
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) this.enabled = false;
    }

    // Start
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren(typeof(Animator)) as Animator;
    }

    // Physics Update
    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = Vector2.zero;

        // Uniform Movement
        if (inputX != 0 || inputY != 0)
        {
            if (inputX != 0 && inputY != 0)
            {
                float mag = (float)(Math.Sqrt(Math.Pow(inputX, 2)) * Math.Sqrt(Math.Pow(inputY, 2)));
                float angle = (float)(Math.Acos(0 / mag) / 2);

                inputX *= (float)Math.Cos(angle);
                inputY *= (float)Math.Sin(angle);
            }

            movement = new Vector2(inputX * speed, inputY * speed);
        }

        rb.velocity = movement;
        PlayerCamera.getInstance().FollowPlayer(transform);
        RenderMovement(inputX, inputY);
    }


    // Render Movement
    private void RenderMovement(float x, float y)
    {
        // UP
        if (y > 0 && Math.Abs(y) >= Math.Abs(x)) anim.SetBool("UP", true);
        else anim.SetBool("UP", false);

        // DOWN
        if (y < 0 && Math.Abs(y) >= Math.Abs(x)) anim.SetBool("DOWN", true);
        else anim.SetBool("DOWN", false);

        // LEFT
        if (x < 0 && Math.Abs(x) > Math.Abs(y)) anim.SetBool("LEFT", true);
        else anim.SetBool("LEFT", false);

        // RIGHT
        if (x > 0 && Math.Abs(x) > Math.Abs(y)) anim.SetBool("RIGHT", true);
        else anim.SetBool("RIGHT", false);

        // IDLE
        if (Math.Abs(x) + Math.Abs(y) == 0) anim.SetBool("IDLE", true);
        else anim.SetBool("IDLE", false);
    }
}