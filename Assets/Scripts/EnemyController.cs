using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;

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
