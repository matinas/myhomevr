# myhomevr
A simple Unity VR scene representing my real house. It can be run either with Google Cardboard (GoogleVR already included) or a SteamVR-compatible headset as HTC Vive, Oculus Rift or Microsoft Mixed Reality Headsets (SteamVR must be installed in the system). It's still work in progress, so I for now I put focus on having at least a nice living room but not the rest of the rooms. Anyway, the perimeter walls are in place and the complete layout of the house is well completed.

The idea is to include the rest of the furniture and other missing elements trying to represent them with as high fidelity as possible but as I guess I won't be modeling any custom 3D models I'll have to search for similar assets in the stores. The vast of the actual assets were taken from the [Simple Home Stuff](https://www.assetstore.unity3d.com/en/#!/content/69129) free asset as well as a few additional free doors pulled from Turbosquid. Beyond those, there are a few objects I've done myself using simple Unity primitives (mainly cubes and cylinders) such as the rounded table at the left of the room, the moving ceiling fan hanging from the center of the living room, and the TV display where I projected some video later.


**A bit more information**

- _Materials and Textures_: the floor has a material which has a parquet texture associated and the corresponding normal map so to achieve some roughness in parquet's wood. Also the walls, the ceiling and the objects from the Simple Home Stuff package has their own material. All of the materials uses the Standard shader to render the object though.
- _Scale_: I've tried the scale both in Google Cardboard and HTC Vive and the room and all the objects seems to be pretty much life-size. Furthermore, to define the camera position to be used in the mobile VR headset I used the HTC Vive tracking as a helper, so to hardcode a real world camera position which corresponds to my height (after headset donned).
- _Lighting_: the scene includes a few point lights, two on the room's ceiling , another one on the floor lamp near the two-folded door and a fourth at the end of the main corridor. Take into account that some of them are not well perceived when using mobile VR due to the graphics optimizations Unity does to get it to work efficiently.
- _Animation_: I animated the ceiling fan in the center of the room. I initially guessed it wouldn't be difficult to animate as it was just matter of adding a cylindrical base for the fan in which all the blades are attached, and then the animation would be achieved by rotating just the cylindrical base (considering the blades are child objects of the base, so they will also rotate accordingly). Anyway, I couldn't achieve a smooth animation using the Unity's Animation System as when the animation was configured to loop there was a bit of jumpiness between the last keyframe and the first one. The alternative solution was to animate it through a custom Script which just rotates the fan base by a few degrees on each frame indefinitely.
- _Audio_: I've added a sound effect for the spinning ceiling fan so you can hear it spinning from above your head. It's basically a mono sound attached to the fan itself so Unity applies the HRTF function to get spatialized sound for it. Additionally, I added some ambient sound coming from the TV. For this one, I've used a VideoClip so I can texture a plane placed above the TV display with a video. Doing so it should have also reproduced the video's audio but didn't work for me, so I had to download the audio track for the video separately and import it into Unity as an audio file. Then I added the audio as a new AudioClip which referenced by the VideoClip component.


**To run it with Google Cardboard:**

1.	Check Android is selected as Build Platform in the Build Settings (File > Build Settings).
2.	Check Google Cardboard is selected in the Virtual Reality Supported list in the Rendering section of Player Settings (Edit > Project Settings > Other Settings > Rendering).
3.	Translate Main Camera so X=-3.5, Y=3.1 and Z=-1. This position was obtained checking the camera transformation when using the HTC Vive headset, so it's based on real data. Anyway, obviously the best position will depend on the height of the user, so ideally the app should ask for the position in the case of mobile VR or let the user modify the initial position somehow (at least in the Y axis).
4.	Build and Run.

**To run it with a SteamVR-compatible HMD:**

1.	Check PC is selected as Build Platform in the Build Settings (File > Build Settings).
2.	Check OpenVR is selected in the Virtual Reality Supported list in the Rendering section of Player Settings (Edit > Project Settings > Other Settings > Rendering).
3.	Translate Main Camera so X=-3.5, Y=1.6 and Z=-1. This is because the camera position defines the center of the play space, so this way the play space lies directly above the floor (which is at Y=1.6). It would probably be better to reset the position of all the components so the floor and camera both lies at Y=0, but for now just set it with the aforementioned value.
4.	Build and Run.
