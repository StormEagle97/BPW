using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScript : MonoBehaviour
{
    public int Points = 500;
    public int PerkPoints = 0;
    public Text PointsDisplay;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PointsDisplay.text = Points.ToString();
    }
}
