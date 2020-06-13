using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float MinImpulseForce = 0.1f;
    [SerializeField]
    private float MaxImpulseForce = 0.5f;

    [SerializeField]
    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

        //pushing rock in the direction of vector
        //GetComponent<Rigidbody2D>().AddForce(RandomImpulse(), ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddForce(ImpulseToCenter(), ForceMode2D.Impulse);
    }

    Vector2 RandomImpulse()
    {
        //randomize speed and angle of moving
        float speed = Random.Range(MinImpulseForce, MaxImpulseForce);
        float angle = Random.Range(0, 2 * Mathf.PI);

        //creating a vector for rock's moving
        return
            new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
    }

    Vector2 ImpulseToCenter()
    {
        float speed = Random.Range(MinImpulseForce, MaxImpulseForce);

        return
            new Vector2(-gameObject.transform.position.x, -gameObject.transform.position.y) * speed;
    }

    private void OnBecameInvisible()
    {
        Warping.Warp(gameObject);
    }

    public void OnDestroy()
    {
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }
}
