using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Test5 : MonoBehaviour
{
    /// <summary>
    /// This is the Client for Test 5. 
    /// </summary>

    WebSocket server;
    private bool running = false;
    private bool reverse = false;

    public GameObject player;
    public GameObject person;

    private float playerX;
    private float playerY;

    private float personX;
    private float personY;

    private bool dislodgedX = false;
    private bool dislodgedY = false;

    public bool online;
    public bool labFormat;
    public bool random;

    /*
    Lab Format
        {"frameno":2820,"persons":{"61":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"68":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}},"69":{"fingerpos":{"state":true,"x":"1.343203","y":"-0.400039","z":"2.803337"},"headpos":{"x":"1.181285","y":"-0.082563","z":"2.734333"},"img_loc":{"img_x":"631","img_y":"591"}}}}
    SendData Format
        {"frameno": 58, "persons": {"7": {"img_loc": {"img_y": "669", "img_x": "386"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "0.696339", "x": "3.631012", "z": "2.717178"}}, "1": {"img_loc": {"img_y": "365", "img_x": "519"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "-2.342742", "x": "2.304862", "z": "2.790121"}}, "1": {"img_loc": {"img_y": "566", "img_x": "559"}, "fingerpos": {"y": "0.000000", "x": "0.000000", "state": false, "z": "0.000000"}, "headpos": {"y": "-0.333519", "x": "1.906119", "z": "2.828705"}}}}
    */

    // Use this for initialization
    void Start(){
        playerX = player.transform.position.x * 10;
        playerY = player.transform.position.z * -10;
        personX = playerX;
        personY = playerY;
        print("y: " + playerY + " x:" + playerX);

        if (online) { server = new WebSocket("ws://ec2-18-218-100-236.us-east-2.compute.amazonaws.com:8081"); }
    }

    private void Connection()
    {
        server.OnMessage += (sender, e) =>
        {
            if (e.Type == Opcode.Text){
                //print(e.Data);
                // Send Data Format 
                int i = e.Data.IndexOf("img_y");
                int j = e.Data.IndexOf("img_x");
                if (labFormat){
                    personY = float.Parse(e.Data.Substring(i + 8, 3));
                    personX = float.Parse(e.Data.Substring(j + 8, 3));
                }else if (!labFormat){
                    personY = float.Parse(e.Data.Substring(i + 9, 3));
                    personX = float.Parse(e.Data.Substring(j + 9, 3));
                }
                print("y: " + personY + " x:" + personX);
            }
        };
        server.Connect();
    }

    private void FixedUpdate(){
        if (Time.fixedTime % 1 == 0){
            if (running && !online){
                if (random){
                    personX += UnityEngine.Random.Range(-50f, 50f);
                    personY += UnityEngine.Random.Range(-50f, 50f);
                }else{
                    if (!reverse){ personY += 50f; }
                    else{
                        personY -= 50f;
                        personX -= 50f;
                    }

                }
                //print("y: " + personY + " x:" + personX);
            }
        }
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            running = !running;
            if (!running && online){ server.Close(); }
            else if (running && online) { Connection(); }
            print("Running: " + running);
        }
        else if (Input.GetKeyDown("space")){
            reverse = !reverse;
        }

        if (!dislodgedX && !dislodgedY && OffMap(personX / 10, personY / 10)) {
            dislodgedX = true;
            dislodgedY = true;
            print("dislodgedX: " + dislodgedX + " dislodgedY: " + dislodgedY);
            //person.SetActive(true);
        } 
        
        if (!dislodgedX) {
            playerX = personX;
        } else if (playerX == personX) {
            //person.SetActive(false);
            dislodgedX = false;
            print("dislodgedX: " + dislodgedX);
        }


        if (!dislodgedY) {
            playerY = personY;
        } else if (playerY == personY) {
            //person.SetActive(false);
            dislodgedY = false;
            print("dislodgedY: " + dislodgedY);
        }
        
        person.transform.position = new Vector3(personX / 10, 21.35f, -1 * (personY / 10));
        player.transform.position = new Vector3(playerX / 10, 21.35f, -1 * (playerY / 10));
    }

    private bool OffMap(float x, float y){
        // Left Insert
        if (100 < y && y < 140 && x < 60) { return true; }
        // Right Insert
        else if (100 < y && y < 140 && x > 140) { return true; }
        // Front Insert
        else if (80 < x && x < 120 && y < 60) { return true; }
        // Back Insert
        else if (80 < x && x < 120 && y > 180) { return true; }
 
        else { return false; }
    }
}
