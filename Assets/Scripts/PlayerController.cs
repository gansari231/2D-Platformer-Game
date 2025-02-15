﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField]
    GameOverController player_gameover;
    [SerializeField]
    AudioSource player_jump;
    [SerializeField]
    AudioSource player_movement;
    [SerializeField]
    AudioSource player_land;
    [SerializeField]
    AudioSource player_keypicked;

    float runspeed = 3.0f;
    int player_health = 3;
    [SerializeField]
    float jumpspeed = 10.0f;
    bool crouch;
    [SerializeField]
    bool onGround;

    void Awake()
    {
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Jump");
       
        Player_Movement(horizontal);
        Player_Run(horizontal);
        Player_Jump(vertical);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Player_Crouch();
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
    }

    void Player_Crouch()
    {
        if(crouch == false)
        {
            crouch = true;
            player_animator.SetBool("Crouch", true);
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
            player_jump.Play();
            onGround = false;
        }
        else if(vertical == 0 && onGround == false)
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

    public void KeyCollected()
    {
        player_score.UpdateScore(10);
        player_keypicked.Play();
    }

    public void KillPlayer()
    {
        player_health--;
        if(player_health == 2)
        {
            player_animator.SetTrigger("Hurt");
            life_three.SetActive(false);
        }
        else if(player_health == 1)
        {
            player_animator.SetTrigger("Hurt");
            life_two.SetActive(false);
        }
        else
        {
            player_animator.SetTrigger("Death");
            life_one.SetActive(false);
            player_gameover.GameOver();
            this.enabled = false;
        }
    }
}
