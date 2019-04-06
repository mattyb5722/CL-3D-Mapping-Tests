using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Test6 : MonoBehaviour {
    /// <summary>
    /// This is the Client for Test 1. It works with the sendData python file.
    /// Works to move a player object around a space. Tests the connection to the server.
    /// </summary>

    WebSocket server;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;

    private ArrayList players = new ArrayList();

    private ArrayList xValues = new ArrayList();
    private ArrayList yValues = new ArrayList();

    // Use this for initialization
    void Start() {
        server = new WebSocket("ws://ec2-18-218-100-236.us-east-2.compute.amazonaws.com:8081");
        players.Add(player1);
        players.Add(player2);
        players.Add(player3);

        xValues.Add(500f);
        xValues.Add(500f);
        xValues.Add(500f);

        yValues.Add(600f);
        yValues.Add(600f);
        yValues.Add(600f);
    }

    private void Update() {

        for (int i = 0; i < players.Capacity; i++) {
            ((GameObject) players[i]).transform.position = new Vector3((float) xValues[i] / 10, 0f, (float) yValues[i] / 10);
        }
    }

    // {"frameno":2820,"persons":{"61":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"68":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"69":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}}}}

    /*
    {"frameno": 58, "persons": {
        "7": {"img_loc": {"img_y": "669", "img_x": "386"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "0.696339", "x": "3.631012", "z": "2.717178"}}, 
        "1": {"img_loc": {"img_y": "365", "img_x": "519"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "-2.342742", "x": "2.304862", "z": "2.790121"}}, 
        "1": {"img_loc": {"img_y": "566", "img_x": "559"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "-0.333519", "x": "1.906119", "z": "2.828705"}}}}
*/

    private void FixedUpdate() {
        if (Time.fixedTime % 15 == 0) {
            server.OnMessage += (sender, e) => {
                if (e.Type == Opcode.Text) {
                    ////////////////////////////////////////////////////////////////////
                    //print(e.Data);
                    string temp = e.Data;
                    for (int q = 0; q < 3; q++) {
                        int i = e.Data.IndexOf("img_y");
                        yValues [i] = float.Parse(e.Data.Substring(i + 9, 3));
                        int j = e.Data.IndexOf("img_x");
                        xValues [i] = float.Parse(e.Data.Substring(j + 9, 3));
                        print("y: " + yValues [i] + " x:" + xValues [i]);
                        temp = temp.Substring(j + 9, temp.Length - i- 9);
                    }
                    ////////////////////////////////////////////////////////////////////
                }
            };
            server.Connect();
        }
    }

    public ArrayList getXValues() {
        return xValues;
    }
}
