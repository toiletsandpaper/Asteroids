using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    #region Fields

    [SerializeField]
    GameObject[] prefabAsteroids;

    [SerializeField]
    private float MinDelay = 1f;
    [SerializeField] 
    private float MaxDelay = 3f;
    [SerializeField]
    int MaxSpawnTries = 3;

    Timer timer;

    const int SpawnBorderSize = 300;
    int minSpawnX;
    int maxSpawnX;
    int minSpawnY;
    int maxSpawnY;

    Vector3 location = new Vector3();
    Vector2 min = new Vector2();
    Vector2 max = new Vector2();
    int indexOfAsteroid = 0;

    #endregion

    #region Methods
    void Start()
    {

        // save spawn boundaries for efficiency
        minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.height - SpawnBorderSize;

        // create and start timer
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = Random.Range(MinDelay, MaxDelay);
        timer.Run();

    }

    void Update()
    {
        //if count of asteroids is <15 - spawn new asteroid and start delay timer
        if (timer.Finished && GameObject.FindGameObjectsWithTag("Asteroid").Length < 15)
        {
            SpawnAsteroid();

            timer.Duration = Random.Range(MinDelay, MaxDelay);
            timer.Run();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //destroy the game object, if it leaves BoxCollider and start the delay timer
        Destroy(other.gameObject);
        if (timer.Finished)
        {
            timer.Duration = Random.Range(MinDelay, MaxDelay);
            timer.Run();
        }
    }

    /// <summary>
    /// Spawn asteroid with chechink on collider-free space
    /// </summary>
    private void SpawnAsteroid()
    {
        //generate a random index for asteroid
        indexOfAsteroid = Random.Range(0, prefabAsteroids.Length);

        // generate random location and calculate teddy bear collision rectangle
        location.x = Random.Range(minSpawnX, maxSpawnX);
        location.y = Random.Range(minSpawnY, maxSpawnY);
        location.z = -Camera.main.transform.position.z;
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
        SetMinAndMax(worldLocation, indexOfAsteroid);


        // make sure we don't spawn into a collision
        int spawnTries = 1;
        while (Physics2D.OverlapArea(min, max) != null &&
               spawnTries < MaxSpawnTries)
        {
            // change location and calculate new rectangle points
            location.x = Random.Range(minSpawnX, maxSpawnX);
            location.y = Random.Range(minSpawnY, maxSpawnY);
            worldLocation = Camera.main.ScreenToWorldPoint(location);
            SetMinAndMax(worldLocation, indexOfAsteroid);

            spawnTries++;
        }
        if(Physics2D.OverlapArea(min, max) == null)
        {
            GameObject asteroid = Instantiate(prefabAsteroids[indexOfAsteroid]) as GameObject;
            asteroid.transform.position = worldLocation;

        }
        /*Vector3 newLocation = new Vector3(Random.Range(minSpawnX, maxSpawnX),
                                       Random.Range(minSpawnY, maxSpawnY),
                                       -Camera.main.transform.position.z);
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(newLocation);
        GameObject asteroid = Instantiate(prefabAsteroids[Random.Range(0, prefabAsteroids.Length)]) as GameObject;
        asteroid.transform.position = worldLocation;*/
    }

    /// <summary>
	/// Sets min and max for a objcect collision rectangle
	/// </summary>
	/// <param name="location">location of object</param>
    void SetMinAndMax(Vector3 location, int index)
    {
        min.x = location.x - prefabAsteroids[index].GetComponent<CircleCollider2D>().radius;
        min.y = location.y - prefabAsteroids[index].GetComponent<CircleCollider2D>().radius;
        max.x = location.x + prefabAsteroids[index].GetComponent<CircleCollider2D>().radius;
        max.y = location.y + prefabAsteroids[index].GetComponent<CircleCollider2D>().radius;
    }
    #endregion

}
