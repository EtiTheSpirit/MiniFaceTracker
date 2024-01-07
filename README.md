# Mini Face Tracker

This tool was designed by me *for* me after my older sister gave me her Vive Lip Tracker and I wanted to use it.

## Usage

* Build the app. It's kind of a "run and go" deal, where you open it and it just does its thing on its own.
* This relies on the [OSCVRC](https://github.com/EtiTheSpirit/OSCVRC) library I designed to send data over the network. You will need to clone that repo as well.
* Consider using [OSCmooth](https://github.com/regzo2/OSCmooth) on your avatar. It is an **outstanding** one-click tool that smooths out the rather choppy 10 FPS animations that OSC gives to other players.

## Design

After spending an annoying amount of time digging through Vive's docs and public tools on how to set up the face tracker, I ran into three problems:
1. VRCFT is useful, but it feels a bit unnecessary and bulky for what I want to do.
2. I want to be able to read parameters myself to generate unique combinations, which I do for avatars that have jaw flap bones to make them look *much* better than just using `JawOpen`.
3. I don't want to have to install any of Vive's data on my system at all if I can avoid it.

Thus, Mini Face Tracker was born. It provides bare minimum access to the face tracker (including a stripped version of `sr_runtime` that will probably crash or something if you try to use eye tracking data), and sends every shape over to VRChat via OSC on the path `Face/MouthShape/...` where `...` is an entry from [MouthShape](https://github.com/EtiTheSpirit/MiniFaceTracker/blob/master/FaceTrackerDriver/FaceTracker/Data/MouthShape.cs).
