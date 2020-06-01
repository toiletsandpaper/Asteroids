using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    private float thrustForce = 5f;
    [SerializeField]
    private float rotateDegreesPerSecond = 10f;

    Vector2 thrustDirection;
    float radius;

    void Start()
    {
        //getting a radius of collider, just for correct warping an object
        radius = gameObject.GetComponent<CircleCollider2D>().radius;

        //changing start direction from "right" to "forward", by adding a 90 degrees
        thrustDirection = new Vector2(Mathf.Cos((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad));
    }

    /// <summary>
    /// Method, that warp the ship to the opposite side, when he leaves the visible area.
    /// </summary>
    void OnBecameInvisible()
    {
        //just for no to do this nullable
        float newX = transform.position.x;
        float newY = transform.position.y;

        //describing the new coordinates, when object leaves the visible area
        //subtracting the scale of object (radius of collider), for wapring to outside area for more beauty
        if (transform.position.x > ScreenUtils.ScreenRight) newX = ScreenUtils.ScreenLeft - radius;
        if (transform.position.x < ScreenUtils.ScreenLeft) newX = ScreenUtils.ScreenRight - radius;
        if (transform.position.y > ScreenUtils.ScreenTop) newY = ScreenUtils.ScreenBottom - radius;
        if (transform.position.y < ScreenUtils.ScreenBottom) newY = ScreenUtils.ScreenTop - radius;

        //declare the vector with new coordinates
        //and warping the object to this coordinates
        Vector2 position = new Vector2(newX, newY);
        transform.position = position;
    }

    
    void FixedUpdate()
    {
        //giving force to object, when Thrust-button is pressed
        if(Input.GetAxis("Thrust") > 0)
        {
            Vector2 movement = thrustDirection * thrustForce;
            gameObject.GetComponent<Rigidbody2D>().AddForce(movement, ForceMode2D.Force);
        }

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
    }
}
