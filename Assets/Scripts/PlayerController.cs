using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator player_animator;
    [SerializeField]
    Rigidbody2D player_Rb;
    [SerializeField]
    ScoreController player_score;
    [SerializeField]
    GameObject life_one, life_two, life_three;

    float runspeed = 3.0f;
    int player_health = 3;
    [SerializeField]
    float jumpspeed = 10.0f;
    bool crouch;
    [SerializeField]
    bool onGround;

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
            player_animator.SetBool("Crouch", crouch);
        }
        else
        {
            crouch = false;
            player_animator.SetBool("Crouch", false);
        }
    }

    void Player_Jump(float vertical)
    {
        if(vertical > 0 && onGround == true)
        {
            player_animator.SetBool("Jump", true);
            player_Rb.AddForce(new Vector2(0.0f, 5.0f) * jumpspeed, ForceMode2D.Impulse);
            onGround = false;
        }
        else if(vertical == 0 && onGround == false)
        {
            player_animator.SetBool("Jump", false);
            onGround = true;
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

    public void KeyCollected()
    {
        player_score.UpdateScore(10);
    }

    public void KillPlayer()
    {
        player_health--;
        if(player_health == 2)
        {
            //player_animator.SetBool("Hurt", true);
            life_three.SetActive(false);
        }
        else if(player_health == 1)
        {
            //player_animator.SetBool("Hurt", true);
            life_two.SetActive(false);
        }
        else
        {
            //player_animator.SetBool("Hurt", true);
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
