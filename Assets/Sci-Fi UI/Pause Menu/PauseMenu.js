//*******************************************************************************
//*																							*
//*							Written by Grady Featherstone								*
//										© Copyright 2011										*
//*******************************************************************************
var mainMenuSceneName : String;
var scrollPosition : Vector2;
var Skin : GUISkin;
var pauseMenuFont : Font;
private var pauseEnabled = false;
var AntialiasingEnabled : AntialiasingAsPostEffect;
var ResMenu : boolean = false;			

function Start(){
	pauseEnabled = false;
	Time.timeScale = 1;
	AudioListener.volume = 1;
}

function Update(){

	//check if pause button (escape key) is pressed
	if(Input.GetKeyDown("o")){
	
		//check if game is already paused		
		if(pauseEnabled == true){
			//unpause the game
			pauseEnabled = false;	
		}
		
		//else if game isn't paused, then pause it
		else if(pauseEnabled == false){
			pauseEnabled = true;
		}
	}
}

private var showGraphicsDropDown = false;

function OnGUI(){

//GUI.skin.box.font = pauseMenuFont;
GUI.skin = Skin;
//GUI.skin.button.font = pauseMenuFont;

	if(pauseEnabled == true){
		
		//Make a background box
		GUI.Box(Rect(Screen.width /2 - 125,Screen.height /2 - 100,250,400), "Pause Menu");
		
		//Make leave game button
		if(Application.loadedLevel == 2){
		if(GUI.Button(Rect(Screen.width /2 - 125,Screen.height /2 - 50,250,50), "Back to Lobby")){
            if (Network.connections.Length == 1) {
                Debug.Log("Disconnecting: " + Network.connections[0].ipAddress + ":" + Network.connections[0].port);
                Network.CloseConnection(Network.connections[0], true);
                Application.LoadLevel(mainMenuSceneName);
            } 
            else
                if (Network.connections.Length == 0)
                    Debug.Log("No one is connected");
                else
                    if (Network.connections.Length > 1)
                        Debug.Log("Too many connections. Are we running a server?");
		}
		}
		
		//Make Change Graphics Quality button
		if(GUI.Button(Rect(Screen.width /2 - 125,Screen.height /2 ,250,50), "Change Graphics Quality")){
		
			if(showGraphicsDropDown == false){
				showGraphicsDropDown = true;
			}
			else{
				showGraphicsDropDown = false;
			}
		}
		
		//Make master volume control + its label
		GUI.Label(Rect(Screen.width /2 - 125,Screen.height /2 + 70,250,50), "      Master volume :");
		AudioListener.volume = GUI.HorizontalSlider(Rect(Screen.width /2 - 105,Screen.height /2 + 100,200,10), AudioListener.volume, 0, 1);
		
		
		
		//Create the Graphics settings buttons, these won't show automatically, they will be called when
		//the user clicks on the "Change Graphics Quality" Button, and then dissapear when they click
		//on it again....
		if(showGraphicsDropDown == true){
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 ,250,50), "Fastest")){
				QualitySettings.currentLevel = QualityLevel.Fastest;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 50,250,50), "Fast")){
				QualitySettings.currentLevel = QualityLevel.Fast;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 100,250,50), "Simple")){
				QualitySettings.currentLevel = QualityLevel.Simple;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 150,250,50), "Good")){
				QualitySettings.currentLevel = QualityLevel.Good;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 200,250,50), "Beautiful")){
				QualitySettings.currentLevel = QualityLevel.Beautiful;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 250,250,50), "Fantastic")){
				QualitySettings.currentLevel = QualityLevel.Fantastic;
			}
			//Make Antialiasing control
			if(GUI.Button(Rect(Screen.width /2 + 380,Screen.height /2 ,250,50), "Antialiasing")){
				if(AntialiasingEnabled.enabled){
					AntialiasingEnabled.enabled = false;
				}
				else{
					AntialiasingEnabled.enabled = true;
				}
			}
			
			//Make resolution control
			if(GUI.Button(Rect(Screen.width /2 + 380, Screen.height /2 + 50, 250, 50), "Fullscreen")){
				Screen.SetResolution(Screen.width, Screen.height, true);
			}
			
			
			if(Input.GetKeyDown("o")){
				showGraphicsDropDown = false;
			}
		
		//We dont need quit game button
		//if (GUI.Button (Rect (Screen.width /2 - 100,Screen.height /2 + 50,250,50), "Quit Game")){
		//	Application.Quit();
		//}
		}
		
	}
}