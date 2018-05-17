using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover {

    GameObject cover;
    Vector2 position;

    public Cover(GameObject cover, Vector2 position)
    {
        this.cover = cover;
        this.position = position;
    }

    public GameObject GetCover()
    {
        return cover;
    }

    public void SetCover(GameObject cover)
    {
        this.cover = cover;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public void SetPosition(Vector2 position)
    {
        this.position = position;
    }
}
