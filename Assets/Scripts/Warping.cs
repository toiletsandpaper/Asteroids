using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warping : MonoBehaviour
{
    /// <summary>
    /// Warp the ship to the opposite side, when he leaves the visible area.
    /// </summary>
    /// <param name="gameObject">Game object, that will be warped.</param>
    public static void Warp(GameObject gameObject)
    {
        //just for no to do this nullable
        float newX = gameObject.transform.position.x;
        float newY = gameObject.transform.position.y;

        //describing the new coordinates, when object leaves the visible area
        //subtracting the scale of object (radius of collider), for wapring to outside area for more beauty
        if (gameObject.transform.position.y < ScreenUtils.ScreenBottom) newY = ScreenUtils.ScreenTop; // bottom -> top
        if (gameObject.transform.position.x > ScreenUtils.ScreenRight) newX = ScreenUtils.ScreenLeft; // right -> left
        if (gameObject.transform.position.x < ScreenUtils.ScreenLeft) newX = ScreenUtils.ScreenRight; // left -> right
        if (gameObject.transform.position.y > ScreenUtils.ScreenTop) newY = ScreenUtils.ScreenBottom; // top -> bottom

        //declare the vector with new coordinates
        //and warping the object to this coordinates
        Vector2 position = new Vector2(newX, newY);
        gameObject.transform.position = position;
    }
}
