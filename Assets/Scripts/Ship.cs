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
    Text positionUi;

    Vector2 thrustDirection;

    #endregion

    #region Methods

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

        positionUi.text = $"X: {transform.position.x}\nY: {transform.position.y}\nZ: {transform.position.z}";

    }
    #endregion

}
