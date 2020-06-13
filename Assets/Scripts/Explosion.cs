using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        //adding a timer for destroying an object after animation is completed
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        timer.Run();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Finished) Destroy(gameObject);
    }
}
