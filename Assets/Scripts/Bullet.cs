using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    int liveDuration = 2;

    Timer timer;

    Vector2 direction;

    private void OnBecameInvisible()
    {
        Warping.Warp(gameObject);
    }

    /// <summary>
    /// Destroying the asteroids, when its collide with this object
    /// </summary>
    /// <param name="collision">other object</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid") 
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Asteroid>().SplitOrDestroy(); ;
        }
    }
    private void Start()
    {
        //adding a live-time to gameobject
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = liveDuration;
        timer.Run();
        
        //gets current direction and pushes gameobject to it
        direction = new Vector2(Mathf.Cos((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad));
        ApplyForce(direction);
    }

    private void FixedUpdate()
    {
        //destroying gameobject after N-seconds
        if (timer.Finished) Destroy(gameObject);
    }

    /// <summary>
    /// Applying force to bullet
    /// </summary>
    /// <param name="direction">direction of bullet</param>
    void ApplyForce(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }
}
