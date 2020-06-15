using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    string scorePrefix = "Score: ";
    [SerializeField]
    string scorePostfix = " points";

    [SerializeField]
    string timePrefix = "Time: ";
    [SerializeField]
    string timePostfix = "s";

    [SerializeField]
    Text debugField;
    [SerializeField]
    Text scoreField;

    float playingTime = 0;

    int score = 0;

    public int AddPoints
    {
        set { score += value; }
    }

    void Update()
    {
        if(FindObjectOfType(typeof(Ship))) playingTime += Time.deltaTime;
    }

    void FixedUpdate()
    {
        scoreField.text = scorePrefix + score + '\n' + 
            timePrefix + (int)playingTime + timePostfix;
        if (FindObjectOfType(typeof(Ship))) 
            debugField.text = $"X: {FindObjectOfType<Ship>().gameObject.transform.position.x}\nY: {FindObjectOfType<Ship>().gameObject.transform.position.y}\nMusic:\n Alyans Na Zare (Phonk Edition) | yungpiece\n Lol U Died";
    }
}
