using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class EnemyController : MonoBehaviour
{
    float speed = 2;
    SpriteRenderer enemy_SR;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.KillPlayer();
        }
    }

    void Start()
    {
        enemy_SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 15 && transform.position.x <= 19)
        {
            enemy_SR.flipX = true;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            enemy_SR.flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}*/

public class EnemyController : MonoBehaviour
{

    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;
    SpriteRenderer enemy_SR;

    void Start()
    {
        enemy_SR = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.KillPlayer();
        }
    }

    private void Update()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            transform.Rotate(new Vector2(0.0f, 180.0f));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
            transform.Rotate(new Vector2(0.0f, 0.0f));
        }
    }

}
