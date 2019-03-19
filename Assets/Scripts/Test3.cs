using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Test3 : MonoBehaviour
{
    /// <summary>
    /// This is the Client for Test 3. 
    /// </summary>

    WebSocket server;
    private Boolean running = false;


    public GameObject player1;
    private float x = 500f;
    //private float y = 1200f;
    private float y = 600f;

    /*
    Lab Format
        {"frameno":2820,"persons":{"61":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"68":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"69":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}}}}
    SendData Format
        {"frameno": 58, "persons": {"7": {"img_loc": {"img_y": "669", "img_x": "386"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "0.696339", "x": "3.631012", "z": "2.717178"}}, "1": {"img_loc": {"img_y": "365", "img_x": "519"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "-2.342742", "x": "2.304862", "z": "2.790121"}}, "1": {"img_loc": {"img_y": "566", "img_x": "559"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "-0.333519", "x": "1.906119", "z": "2.828705"}}}}
    */

    // Use this for initialization
    void Start()
    {
        server = new WebSocket("ws://ec2-18-218-100-236.us-east-2.compute.amazonaws.com:8081");
    }

    private void Connection()
    {
        server.OnMessage += (sender, e) =>
        {
            if (e.Type == Opcode.Text)
            {
                //print(e.Data);
                // Send Data Format 
                int i = e.Data.IndexOf("img_y");
                int j = e.Data.IndexOf("img_x");
                //y = float.Parse(e.Data.Substring(i + 9, 3));
                y += 10f;
                x = float.Parse(e.Data.Substring(j + 9, 3));
                print("y: " + y + " x:" + x);
            }
        };
        server.Connect();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (running)
            {
                running = false;
                server.Close();
            }
            else
            {
                running = true;
                Connection();
            }
            print("Running: " + running);
        }
        if (y < 600)
        {
           y = 600;
        }else if (y > 1800)
        {
            y = 1800;
        }if (x < 500)
        {
            x = 500;
        }else if (x > 500)
        {
            x = 500;
        }
        player1.transform.position = new Vector3( x/10, 21.35f, (-1*(y/10)));
    }

}
