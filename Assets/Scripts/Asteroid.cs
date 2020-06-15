using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    int pointsForDestroying = 40;

    [SerializeField]
    private float MinImpulseForce = 0.1f;
    [SerializeField]
    private float MaxImpulseForce = 0.5f;

    [SerializeField]
    GameObject explosion;

    bool isSplitted = false;

    public bool IsSplitted
    {
        get
        {
            return isSplitted;
        }
        set
        {
            isSplitted = value;
        }
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

    // Start is called before the first frame update
    void Start()
    {

        //pushing rock in the direction of vector
        GetComponent<Rigidbody2D>().AddForce(RandomImpulse(), ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().AddForce(ImpulseToCenter(), ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Warping.Warp(gameObject);
    }

    public void SplitOrDestroy()
    {
        if (isSplitted)
        {
            FindObjectOfType<HUD>().gameObject.GetComponent<HUD>().AddPoints = pointsForDestroying / 2;
            Destroy(gameObject);
        }
        else
        {
            GameObject asteroid1 = Instantiate(gameObject, gameObject.transform.position - gameObject.transform.localScale, 
                Quaternion.identity) as GameObject;
            asteroid1.GetComponent<Asteroid>().IsSplitted = true;
            asteroid1.gameObject.transform.localScale /= Mathf.Sqrt(2);

            GameObject asteroid2 = Instantiate(gameObject, gameObject.transform.position - gameObject.transform.localScale,
                Quaternion.identity) as GameObject;
            asteroid2.GetComponent<Asteroid>().IsSplitted = true;
            asteroid2.gameObject.transform.localScale /= Mathf.Sqrt(2);

            FindObjectOfType<HUD>().gameObject.GetComponent<HUD>().AddPoints = pointsForDestroying;
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        AudioManager.Play(AudioClipName.Explosion);
        GameObject explosionObject = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as GameObject;
        if (isSplitted) explosionObject.transform.localScale /= Mathf.Sqrt(2);
    }
}
