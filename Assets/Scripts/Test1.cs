using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Test1 : MonoBehaviour
{
    /// <summary>
    /// This is the Client for Test 1. It works with the sendData python file.
    /// Works to move a player object around a space. Tests the connection to the server.
    /// </summary>

    WebSocket server;

    public GameObject player1;
    private float x = 500f;
    private float y = 600f;

    // Use this for initialization
    void Start()
    {
        server = new WebSocket("ws://ec2-18-218-100-236.us-east-2.compute.amazonaws.com:8081");
        player1.GetComponent<Renderer>().material.color = Color.red;
    }

    private void Update()
    {
        player1.transform.position = new Vector3(x / 10, 0f, y / 10);
    }
    // {"frameno":2820,"persons":{"61":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"68":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"69":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}}}}
    private void FixedUpdate()
    {
        if (Time.fixedTime % 15 == 0)
        {
            server.OnMessage += (sender, e) =>
            {
                if (e.Type == Opcode.Text)
                {
                    //print(e.Data);
                    int i = e.Data.IndexOf("img_y");
                    //int j = e.Data.IndexOf("img_x");
                    y = float.Parse(e.Data.Substring(i + 9, 3));
                    //x = float.Parse(e.Data.Substring(j + 9, 3));
                    print("y: " + y + " x:" + x);
                }
            };
            server.Connect();
        }
    }
}
