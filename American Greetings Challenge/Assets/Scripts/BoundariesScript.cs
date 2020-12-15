using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesScript : MonoBehaviour
{
    [SerializeField] Transform Ground, LeftWall, RightWall, Ceiling;

    Vector2 resolution;

    private void Awake()
    { 
        resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        SetBoundaries();
    }

    private void Update()
    {
        if (resolution.x != Screen.currentResolution.width || resolution.y != Screen.currentResolution.height)
        {
            print("CHANGED RESOLUTION");
            resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
            SetBoundaries();
            GameObject.FindGameObjectWithTag("Shape").GetComponent<ShapeScript>().ResetPosition();
        }
    }

    private void SetBoundaries()
    {
        Ceiling.position = new Vector2(0, Camera.main.orthographicSize + Ground.GetComponent<BoxCollider2D>().size.y / 2);
        Ground.position = new Vector2(0, -Camera.main.orthographicSize - Ground.GetComponent<BoxCollider2D>().size.y / 2);
        LeftWall.position = new Vector2(Camera.main.aspect * -Camera.main.orthographicSize - Ground.GetComponent<BoxCollider2D>().size.y / 2, 0);
        RightWall.position = new Vector2(Camera.main.aspect * Camera.main.orthographicSize + Ground.GetComponent<BoxCollider2D>().size.y / 2, 0);

    }
}
