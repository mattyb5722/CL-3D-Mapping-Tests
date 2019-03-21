# CL-3D-Mapping-Tests
For 2019 Spring Semester I am working on research at the Criave Lab. The Criave Lab is a research program run by the Architecture Schools that creats multi user virtual reality system for large group presentation on a 360 screen. In addition to working on a presentation I am also working on several general projects in the lab that will enhance current and future projects. This repo consists of one of these projects that is centered around tracking users in the lab and mapping them to a 3D environment. 

For this project I am using Unity as my 3D environment and kinects for the tracking. On the tracking side I am using python to capture the data and send it to Unity over a websocket server. This can be seen in the sendData.py test file. Within Unity I am using a panoramic camera to display the environment the player is in. This is then displayed on the 11636x1200 screen in Craive Lab.

By the end of the semester we hope to implement this in the presentation I am working on as well as other projects. To do this we are working towards making this project easier to implement for Architecture students who may not have ample coding knowledge.
