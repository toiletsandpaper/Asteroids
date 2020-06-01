using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warping : MonoBehaviour
{
  /*  void Warp(GameObject gameObject)
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
    }*/
}
