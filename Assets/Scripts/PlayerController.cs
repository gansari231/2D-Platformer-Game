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
    [SerializeField]
    Rigidbody2D player_RB;

    float jumpspeed = 5.0f;
    bool crouch = false;
    bool player_jump;

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Vertical");

        player_animator.SetFloat("Speed", Mathf.Abs(speed));

        Vector3 scale = transform.localScale;

        if (speed < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Player_Crouch();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            Player_Jump();  
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

    void Player_Jump()
    {
        if(player_jump == true)
        {
            player_animator.SetBool("Jump", player_jump);
            Vector2 MoveUp = new Vector2(0.0f, 1.0f);
            player_RB.velocity = MoveUp * jumpspeed;
            player_jump = false;
        }
        else
        {
            player_animator.SetBool("Jump", player_jump);
            player_jump = true;
        }
    }
}
