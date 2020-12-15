using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMenu : MonoBehaviour
{
    [SerializeField] GameObject ShapePanel;
    [SerializeField] GameObject CurrentShape;
    [SerializeField] GameObject Hexagon;
    [SerializeField] GameObject Triangle;

    private void Awake()
    {
        CurrentShape = GameObject.FindGameObjectWithTag("Shape");
    }

    public void ActivateShapePanel()
    {
        ShapePanel.SetActive(!ShapePanel.activeInHierarchy);
    }

    public void ChangeShape(string shape)
    {
        if (!CurrentShape.name.Contains(shape))
        {
            Vector3 currentShapePosition = CurrentShape.transform.position + Vector3.up;
            switch (shape)
            {
                case "Hexagon":
                    Destroy(CurrentShape);
                    CurrentShape = Instantiate(Hexagon, currentShapePosition, Quaternion.identity);
                    break;
                case "Triangle":
                    Destroy(CurrentShape);
                    CurrentShape = Instantiate(Triangle, currentShapePosition, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
        ActivateShapePanel();
    }
}
