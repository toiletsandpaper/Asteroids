using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourAsteroidsSpawner : MonoBehaviour
{
    #region Fields

    [SerializeField]
    GameObject[] prefabAsteroids;


    #endregion

    // Start is called before the first frame update
    void Start()
    {

        Instantiate(prefabAsteroids[Random.Range(0, prefabAsteroids.Length)], new Vector2(0, ScreenUtils.ScreenTop), Quaternion.identity);
        Instantiate(prefabAsteroids[Random.Range(0, prefabAsteroids.Length)], new Vector2(0, ScreenUtils.ScreenBottom), Quaternion.identity);
        Instantiate(prefabAsteroids[Random.Range(0, prefabAsteroids.Length)], new Vector2(ScreenUtils.ScreenRight, 0), Quaternion.identity);
        Instantiate(prefabAsteroids[Random.Range(0, prefabAsteroids.Length)], new Vector2(ScreenUtils.ScreenLeft, 0), Quaternion.identity);
    }

}
