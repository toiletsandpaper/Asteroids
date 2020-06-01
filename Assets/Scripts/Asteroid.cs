using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float MinImpulseForce = 1f;
    [SerializeField]
    private float MaxImpulseForce = 3f;

    // Start is called before the first frame update
    void Start()
    {

        //randomize speed and angle of moving
        float speed = Random.Range(MinImpulseForce, MaxImpulseForce);
        float angle = Random.Range(0, 2 * Mathf.PI);

        //creating a vector for rock's moving
        Vector2 finalImpulse = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;

        //pushing rock in the direction of vector
        GetComponent<Rigidbody2D>().AddForce(finalImpulse, ForceMode2D.Impulse);

    }

    private void OnBecameInvisible()
    {
        Warping.Warp(gameObject);
    }
}
