# CinecaAndHoloLens2

- "Start Session" -> StartSendingData()
	
	Send the Unity position and orientation to the server each 5 seconds (using coroutine)

- "Stop Session" -> StopSendingData()
	
	Send the dump command to the server and dispose the object (the command "stop session" does no longer work after launched this command)