# MetroidVR
This is a prototype trying to replicate Metroid Prime in a VR RoomScale experience.


Game Mechanics ---

Movement
	Jetboosters
		There are a lot of ways of moving the roomscale through a 3D game environment; Here are some that I will be testing going forward 

		Camera Steering -
			Duck down into a trigger collider that is below the player's head while touching the circle pad to start going. Then, you can drift by moving you finger left or right on the pad.

			Pros
				So far this seems to make people less motion sick since you are moving you whole body to move in the game space

			Cons:
				Since this is an action game you couldn't strife as easily

		 Point Steering -
		 	Using your free hand you can be able to point a direction you want to go.

		 	Pros:
		 		More flexiblity with movement

 			Cons:
		 		Feel a bit sick when moving backwards

	 	Circle Pad Steering -
	 		Takes the input from the D-Pad and applys the movement based on the camera's forward direction

	 		Pros:
	 			Extra control over the movement

 			Cons:
 				The most nause inducing movement yet

		Jetpack Steering -
			This method will be used by holding a button down and leaning in a direction to determine when you are going.

			To be tested

		Roomscale would have a collider on it so they cant dash out of the play area. Fire a ray with the direction they are moving in order to detect if they need to slow down.

	Jump shoes
		Need to figure out

Gameplay
	Progress through a 3D randomly generated layout in order to fight enemies and make it out. This game would have rogue like elements like no progression between attempts and RNG on the items you collect along the way.

Combat
	Charge cannon which you can pull the trigger and charge your shot
	
	Can switch weapon type by using fingertip button to cycle through options; Either single direction way or by click and hold then twisting your arm left or right to select new weapon type.
	
	Different weapons could be elemental that affect different enimes in different way; Or weapon types so say like Charge shot or missles

Collision
	Have a point at he base of the feet child a collider to that and make the parent always face the headset location. Then when the player moves in room scale "drag" the collider parent.
