# myhomevr
A Unity VR scene representing my real house. It can be run either with Google Cardboard (GoogleVR already included) or a SteamVR-compatible headset as HTC Vive, Oculus Rift or Microsoft Mixed Reality Headsets (SteamVR must be installed in the system). The scene "LivingRoom 3 DoF" can be un with any headset, and the scene "LivingRoom 6 DoF" can be run just with an OpenVR-enabled headset as it requires motion controllers and 6 DoF tracking.

The vast of the actual assets were taken from the [Simple Home Stuff](https://www.assetstore.unity3d.com/en/#!/content/69129) free asset as well as a few additional free doors pulled from Turbosquid. Beyond those, there are a few objects I've done myself using simple Unity primitives (mainly cubes and cylinders) such as the rounded table at the left of the room, the moving ceiling fan hanging from the center of the living room, and all the general house layout (walls, floor, etc).

   <img src="https://user-images.githubusercontent.com/5633645/35434575-6ba491d4-0266-11e8-8a86-1daa752a1b7a.png" alt="home_1" style="max-width:100%" width="1048" heigth="664">
   <img src="https://user-images.githubusercontent.com/5633645/35844812-26e62b58-0aee-11e8-8d10-4e04edef5f55.png" alt="home_1" style="max-width:100%" width="1048" heigth="664">   
   <img src="https://user-images.githubusercontent.com/5633645/36345596-99eef608-140c-11e8-90e6-bd9cb90fc1b9.png" alt="home_2" style="max-width:100%" width="512" heigth="512">
   <img src="https://user-images.githubusercontent.com/5633645/35844768-ec46c930-0aed-11e8-916e-480bc2837d5d.gif" alt="home_2" style="max-width:100%" width="512" heigth="512">

## A bit more information

- _Materials and Textures_: the floor has a material which has a parquet texture associated and the corresponding normal map so to achieve some roughness in parquet's wood. Also the walls, the ceiling and the objects from the Simple Home Stuff package has their own material. All of the materials uses the Standard shader to render the object though.
- _Scale_: I've tried the scale both in Google Cardboard and HTC Vive and the room and all the objects seems to be pretty much life-size. Furthermore, to define the camera position to be used in the mobile VR headset I used the HTC Vive tracking as a helper, so to hardcode a real world camera position which corresponds to my height (after headset donned).
- _Lighting_: : I added a few point lights, two on the room's ceiling , another one on the floor lamp near the two-folded door, one in the main corridor and one in each of the rooms. Take into account that some of them are not well perceived when using mobile VR due to the graphics optimizations Unity does to get it to work efficiently.
- _Animation_: I animated the ceiling fan in the center of the room. I initially guessed it wouldn't be difficult to animate as it was just matter of adding a cylindrical base for the fan in which all the blades are attached, and then the animation would be achieved by rotating just the cylindrical base (considering the blades are child objects of the base, so they will also rotate accordingly). Anyway, I couldn't achieve a smooth animation using the Unity's Animation System as when the animation was configured to loop there was a bit of jumpiness between the last keyframe and the first one. The alternative solution was to animate it through a custom Script which just rotates the fan base by a few degrees on each frame indefinitely.
- _Audio_: I've added a sound effect for the spinning ceiling fan so you can hear it spinning from above your head. It's basically a mono sound attached to the fan itself so Unity applies the HRTF function to get spatialized sound for it. Additionally, I added some ambient sound coming from the TV when it's powered on. For this one, I've used a VideoClip so I can texture a plane placed above the TV display with a video. Doing so it should have also reproduced the video's audio but didn't work for me, so I have to download the audio track for the video separately and import it into Unity as an audio file. Then I added the audio as a new AudioClip which referenced by the VideoClip component. I've also added some sound effects for navigation and when interacting with doors and other elements. _Note: there is a bug when the VideoPlayer that causes a spike in frame time when stopped (400 ms): https://issuetracker.unity3d.com/issues/videoplayer-dot-stop-causes-a-performance-spike_

# User Interaction

This section describes all the features related to User Interaction and Navigation. I implemented my own navigation and interaction library based on the [VR Samples](https://assetstore.unity.com/packages/essentials/tutorial-projects/vr-samples-51519) project. The included interaction library supports three different types of interaction (all those can be configured in the Selection Radial element as part of the Main Camera components):

1.	**Fully gazed:** this type of interaction requires that the user looks in the direction of an interactible object (a selection radial will appear always an interactible object is gazed) so the selection radial automatically starts filling. When the bar completes the corresponding action is triggered. This type of interaction is useful for mobile headsets in which the only type of input is head rotation. For example, Google Cardboard. Note: it can also be used for high-end headsets though.
2.	**Gaze plus 2DUI with selection radial**: this type of interaction requires that the user looks in the direction of an interactible object (a selection radial will appear always an interactible object is gazed) and then click the fire button so the selection radial starts filling. When the bar completes the corresponding action is triggered. This type of interaction is useful for mobile headsets which include some form of 2D input, such as a button, a touchpad or a trigger. For example, Samsung GearVR. Note: it can also be used for high-end headsets though.
3.	**Gaze plus 2DUI without selection radial:** this type of interaction requires that the user looks in the direction of an interactible object (a selection radial will appear always an interactible object is gazed) and then click or double click the fire button so the action is triggered. In this case, the selection radial must not be filled and the interaction triggers automatically as soon as click is detected. This type of interaction is useful for mobile headsets which include some form of 2D input, such as a button, a touchpad or a trigger. For example, Samsung GearVR. Note: it can also be used for high-end headsets though.

Using any of the methods above, a UI tooltip text is shown indicating the action that the user can trigger when selecting the corresponding object. In the following subsections all the navigation and interaction features are detailed.

When using a high-end VR system with motion controllers (scene "LivingRoom 6 DoF") an additional interaction method is added completely based on natural interaction, so the user can just grab objects with his virtual hands.

## Navigation

### 3 DOF

For the Cardboard version (scene "LivingRoom 3 DoF"), the navigation was implemented using little platforms among the scene so after selecting one of them by gazing and waiting the selection radial to fill, the user immediately moves to that location in a blink of an eye (it also includes a little fade out/in in order to avoid discomfort). A moving arrow will also appear as the user gaze to a navigation platform, indicating that he can move there. The locations the user can move will be pre-defined, and there are one or two navigation platforms in each room of the house, so the user is able to move along all the house jumping between these predefined spots.

<img src="https://user-images.githubusercontent.com/5633645/36345614-32e12ad4-140d-11e8-915e-077b14387e45.png" alt="home_2" style="max-width:100%" width="512" heigth="512">

<img src="https://user-images.githubusercontent.com/5633645/36345616-36ff1464-140d-11e8-972e-ea12d272a1ff.png" alt="home_2" style="max-width:100%" width="512" heigth="512">

### 6 DOF

For the hieh-end headsets version (scene "LivingRoom 6 DoF"), the navigation was implemented with different types of teleportation. The type of teleportation to be used can be selected through a menu that is triggered when pressing the grip buttons on the controllers. A little green sphere is shown at the left of each method indicating whether the option is active or not.

IMAGEN!

**Teleport Points:** this option implies that there will be many predefined spots which we can use to move through the scene, much similar to the 3 DoF case. Pressing the touchpad on the motion controllers an arc will be shown starting from the controller position. To teleport to one of the predefined spots just moving the arc above it and releasing the button press.

IMAGEN!

**Free Teleporting:** with this approach the user has more freedom to move as he can move anywhere in the scene without restrictions or pre-definied spots (except for unreachable places such as places behing walls, doors, etc). The way to teleport is very similar to the previous one, simply pressing the controller's touch pad to point to the position where we want to move and releasing the button press for triggering the teleportation.  sing the Vive Controllers the user points to any position in the scene, press the trigger and automatically teleports to that location.

IMAGEN!

**Hybrid:** both methods can be enabled at the same time.

## Interaction

### 3 DOF

There is a subset of the objects in the scene which can be selected/manipulated (mainly objects above tables, the TV, the ceiling fan, etc. but not furniture, walls and other less natural objects to interacts with). For the 3 DOF version, the selection and manipulation is fully gaze-based. So you look to an object and, depending on the interaction method configured, the selection radial starts filing up and a little UI tooltip text will be shown indicating the action to be triggered. If it gets completely filled the object is selected and depending on the object a different action will be triggered (if it's a "grabable" object it will be shown near the user for a few seconds, if it's an interactible-only object it will trigger the associated action, for example, turning the TV on/off). The interaction is kind of magical, as the user is selecting objects from the distance using just his gaze.

The following interactions are supported so far:

1.	When selecting the ceiling fan, if turned off, it will start spinning. If selected when powered on it will be turned off.
2.	When selecting the TV, if turned off, it will be turned on and start showing some pre-loaded video. If selected when powered on it will be turned off.
3. When selecting any of the doors, it will be opened or closed accordingly.

### 6 DOF

In this case the selection and manipulation is be based on the use of motion controllers. So to grab an object you just put your virtual hand near the object and press the trigger. In this case the interactions will be fully natural, as the user will be interacting with the different objects in the same way he would do in the real world. 

In this line, the following interactions are supported so far:

1. Books, lamps and other small objects in the scene can be grabbed with the virtual hand by pressing the trigger. When the trigger is released the object grabbed will be released (you can throw it if released while moving the hand with some velocity).
2. A useful object is the Universal Remote that can be found above the short table. After grabbing it you can point to different objects and press the touchpad button to interact with them. For example, the TV can be powered on/off, the fan can be powered on/off, and even some of the doors can be opened/closed using it. Now that's an universal remote! Check the video below this lines.
3. Doors can be opened/closed by grabbing the handle by pulling the controller's trigger and moving the hand back or forth as you would do to open/close a real door.

<a target="_blank" href="https://www.youtube.com/watch?v=jw8KjPF3hcQ"><img src="http://img.youtube.com/vi/jw8KjPF3hcQ/0.jpg" alt="Roll a Ball 3D" width="240" height="180" border="10" /></a>

# How to run it

## Run from the Unity Editor

The project can be run directly from the Editor as it includes a basic mouse-based camera movement script. Hit Play from the Editor and it should be up and running in a few seconds. Just don't forget to put the focus on the Game tab so it correctly takes the mouse cursor as an input. It's also recommended to change the Build Settings Platform to PC when running from the Editor so it runs with the highest graphics quality (by default Android Build Platform is selected).

## Build and run with Google Cardboard

1.	Check Android is selected as Build Platform in the Build Settings (File > Build Settings).
2.	Check Google Cardboard is selected in the Virtual Reality Supported list in the Rendering section of Player Settings (Edit > Project Settings > Other Settings > Rendering).
3.	Initially the position must be configured manually, but now the application already sets the camera position accordingly if it detects it's running on an Android device. This position was obtained checking the camera transformation when using the HTC Vive headset, so it's based on real data. Anyway, obviously the best position will depend on the height of the user, so ideally the app should ask for the position in the case of mobile VR or let the user modify the initial position somehow (at least in the Y axis).
4.	Check that the interaction method to be used is Gaze-based. This can be selected in the Selection Radial script attached to the Main Camera component.
5.	Build and Run.

## Build and run with a SteamVR-compatible HMD

1.	Check PC is selected as Build Platform in the Build Settings (File > Build Settings).
2.	Check OpenVR is selected in the Virtual Reality Supported list in the Rendering section of Player Settings (Edit > Project Settings > Other Settings > Rendering).
3.	Check that the interaction method is the desired (fully gaze-based, gaze-based plus 2DUI and selection bar, or gaze-based plus 2DUI without selection bar). This can be selected in the Selection Radial script attached to the Main Camera component.
4.	Build and Run.

# Demo videos

## 3 DOF

https://www.youtube.com/watch?v=wGEwUGgWl5E

## 6 DOF

https://www.youtube.com/watch?v=jw8KjPF3hcQ
