using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private float thrustForce = 5f;
    [SerializeField]
    private float rotateDegreesPerSecond = 10f;
    [SerializeField]
    GameObject explosion;

    [SerializeField]
    Bullet bullet;

    [SerializeField]
    Text positionUi;

    [SerializeField]
    AudioClip gameMusic;
    [SerializeField]
    AudioClip deathMusic;

    Vector2 thrustDirection;

    #endregion

    /// <summary>
    /// Returns the direction of ship's moving
    /// </summary>
    public Vector2 ShipDirection { get { return thrustDirection; } }

    #region UnityMethods

    private void Awake()
    {
        //just playing some music on background
        Camera.main.GetComponent<AudioSource>().clip = gameMusic;
        Camera.main.GetComponent<AudioSource>().Play();
    }
    void Start()
    {
        //changing start direction from "right" to "forward", by adding a 90 degrees
        thrustDirection = new Vector2(Mathf.Cos((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad));
    }

    /// <summary>
    /// Warp the ship, when he leaves one of the border.
    /// </summary>
    void OnBecameInvisible()
    {
        Warping.Warp(gameObject);
    }

    /// <summary>
    /// Death method
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            //changing the music to death-music :)
            Camera.main.GetComponent<AudioSource>().clip = deathMusic;
            Camera.main.GetComponent<AudioSource>().Play();
            
            //instantitate an explosion and destroying ship
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        //giving force to object, when Thrust-button is pressed
        if(Input.GetAxis("Thrust") > 0)
        {
            Vector2 movement = thrustDirection * thrustForce;
            gameObject.GetComponent<Rigidbody2D>().AddForce(movement, ForceMode2D.Force);
        }
        //rotate the object, when Rotate-buttin is pressed
        if (Input.GetAxis("Rotate") != 0)
        {
            //calculate rotation amount and apply rotation
            float rotationInput = Input.GetAxis("Rotate");
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            //changing force direction
            //changing start direction from "right" to "forward", by adding a 90 degrees
            thrustDirection = new Vector2(Mathf.Cos((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad));
            
        }
        //shoots the bullet, when "Left-Control" is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //adding transform.rotation to object for instantiate the bullet in ship's direction angle
            Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        }

        //giving to UI text the coordinates of ship and some other info
        positionUi.text = $"X: {transform.position.x}\nY: {transform.position.y}\nMusic:\n Alyans Na Zare (Phonk Edition) | yungpiece\n Lol U Died";

    }
    #endregion

}
