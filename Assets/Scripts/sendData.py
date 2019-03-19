from websocket import create_connection
import numpy as np
from random import randrange
import json
import time

if __name__ == "__main__":
    ws = create_connection("ws://ec2-18-218-100-236.us-east-2.compute.amazonaws.com:8081")
    f = open("C:/Users/m5w5b/Desktop/Craive Lab/Craive Lab Tests/Assets/Scripts/Set1.txt")
    lines = f.readlines()
    for line in lines:
        print(line)
        ws.send(line)
        time.sleep(1)
    while True:
        ws.send(lines[125])
        time.sleep(1)
    ws.close()
