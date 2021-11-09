using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator player_animator;
    [SerializeField]
    Collider2D player_collider;
    [SerializeField]
    Collider2D crouch_collider;

    float runspeed = 3.0f;
    [SerializeField]
    float jumpspeed = 5.0f;
    bool crouch;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Jump");

        Player_Movement(horizontal);
        Player_Run(horizontal);
        Player_Jump(vertical);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Player_Crouch();
        }      
    }

    void Player_Crouch()
    {
        if(crouch == false)
        {
            crouch = true;
            player_collider.enabled = false;
            crouch_collider.enabled = true;
            player_animator.SetBool("Crouch", crouch);
        }
        else
        {
            crouch = false;
            player_collider.enabled = true;
            crouch_collider.enabled = false;
            player_animator.SetBool("Crouch", false);
        }
    }

    void Player_Jump(float vertical)
    {
        if(vertical > 0)
        {
            player_animator.SetBool("Jump", true);
            Vector2 MoveUp = transform.position;
            MoveUp.y += vertical * jumpspeed * Time.deltaTime;
            transform.position = MoveUp;
        }
        else if(vertical == 0)
        {
            player_animator.SetBool("Jump", false);
        }
    }

    void Player_Movement(float horizontal)
    {
        player_animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;

        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    void Player_Run(float horizontal)
    {
        Vector2 move_position = transform.position;
        move_position.x += horizontal * runspeed * Time.deltaTime;
        transform.position = move_position;
    }
}
