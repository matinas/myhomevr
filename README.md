# myhomevr
A simple Unity VR scene representing my real house. It can be run either with Google Cardboard (GoogleVR already included) or a SteamVR-compatible headset as HTC Vive, Oculus Rift or Microsoft Mixed Reality Headsets (SteamVR must be installed in the system).

**To run it with Google Cardboard:**
1. Check Android is selected as Build Platform in the Build Settings (File > Build Settings)
2. Check Google Cardboard is selected in the Virtual Reality Supported list in the Rendering section of Player Settings (Edit > Project Settings > Other Settings > Rendering)
3. Translate Main Camera upwards so Y=1.8
4. Build and Run

**To run it with a SteamVR-compatible HMD:**
1. Check PC is selected as Build Platform in the Build Settings (File > Build Settings)
2. Check OpenVR is selected in the Virtual Reality Supported list in the Rendering section of Player Settings (Edit > Project Settings > Other Settings > Rendering)
3. Translate Main Camera downwards so Y=0.3. This is because the camera position defines the center of the play space, so this way the play space lies directly above the floors.
4.	Build and Run
