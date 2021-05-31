using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameParameters
{
    public static Vector2 RandomScreenPoint()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 0.1f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.9f, 0.9f));

        float moveX = Random.Range(min.x, max.x);
        float moveY = Random.Range(min.y, max.y);

        return new Vector2(moveX, moveY);
    }
}
