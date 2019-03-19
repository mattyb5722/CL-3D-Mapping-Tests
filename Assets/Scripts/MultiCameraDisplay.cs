using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCameraDisplay : MonoBehaviour
{

    public Camera overhead;
    public Camera right;
    public Camera front;
    public Camera left;

    void Update()
    {
        // Test 1 (Only Walls: 11636 x 1200)
        /*
        overhead.rect = new Rect(0f, 0f, .1268f, 1f);
        left.rect = new Rect(.1845f, 0f, .2166f, 1f);
        front.rect = new Rect(.4587f, 0f, .1682f, 1f);
        right.rect = new Rect(.6846f, 0f, .2166f, 1f);
        */
        // Test 2 (Walls and Corners: 11636 x 1200)
        /*
        overhead.rect = new Rect(0f, 0f, .1268f, 1f);
        left.rect = new Rect(.1268f, 0f, .2742f, 1f);
        front.rect = new Rect(.4011f, 0f, .2835f, 1f);
        right.rect = new Rect(.6846f, 0f, .2742f, 1f);
        */
        // Test 3 (Walls and Overlapping Corners: 11636 x 1200)
        /*
        overhead.rect = new Rect(0f, 0f, .1268f, 1f);
        left.rect = new Rect(.1268f, 0f, .3319f, 1f);
        front.rect = new Rect(.4011f, 0f, .2835f, 1f);
        right.rect = new Rect(.6269f, 0f, .3319f, 1f);
        */
    }
}
