using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreManager : MonoBehaviour {
	public int gold, ability, score, debugPass;
	public int[] unlock = new int[60];
	public bool lost=false, debugOn;
	void OnGUI () {
		if (Application.loadedLevelName == "Logo")
			return;
		GUI.color = Color.yellow;
		GUI.skin.label.fontSize = Screen.width/31;
		GUI.Label (new Rect(Screen.width*0.87f, Screen.height*0.03f-5, 600, 100), "" + gold.ToString());
		/*if (Application.loadedLevelName == "Mystery"&&!GameObject.FindObjectOfType<GoldPayment>().pay) {
			GUI.color = Color.yellow;
			GUI.Label (new Rect (Screen.width * 0.23f, Screen.height * 0.25f, Screen.width * 0.75f, Screen.height * 0.75f), "         Choose a mystery box!\n\n Costs 600 gold coins.\n If you get an ability you already have,\n you will be refunded 200 gold!");
		}*/
		if (Application.loadedLevelName !="Shop")
			return;
		switch (ability) {
		case 0:{
			GUI.color = Color.black;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " BLANK\n No ability selected! Only for Hardcore\n players!"); break;}
		case 1:{
			GUI.color = Color.cyan;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " FORCE SHIELD (5s)\n Protects the platform from any boxes. You shall not pass!"); break;}
		case 2:{
			GUI.color = Color.red;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " MAGNET (5s)\n Repels iron boxes. I reject your offer, Mr. Iron!"); break;}
		case 3:{
			GUI.color = Color.yellow;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " HARDEN (5s)\n Makes the platform heavier and more stable. Fortify!"); break;}
		case 4:{
			GUI.color = Color.white;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " SAW (10s)\n Destroys wooden boxes. Would you like to play a game?"); break;}
		case 5:{
			GUI.color = Color.green;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " TIME IS MONEY (5s)\n Adds 5s to the timer whenever a coin box passes through."); break;}
		case 6:{
			GUI.color = Color.red;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " BOMB DEFUSE (5s)\n Transforms all bombs that pass through into normal boxes."); break;}
		case 7:{
			GUI.color = Color.white;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " GHOST (5s)\n Transforms all bouncy boxes that pass through into ghost boxes."); break;}
		case 8:{
			GUI.color = Color.cyan;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " FROST (10s)\n Freezes wood, bouncy and coin boxes. Break frozen bouncy boxes!"); break;}
		case 9:{
			GUI.color = Color.blue;
			GUI.Label (new Rect (Screen.width*0.45f, Screen.height*0.73f, Screen.width*0.35f, Screen.height*0.30f), " FORCE PUSH (3s)\n Creates a Force Field that pushes boxes to the left. Use the force!"); break;}
	}
	
}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.gold = gold;
		data.ability = ability;
		data.score = score;
		for(int i=1; i<=10; i++)
		data.unlock[i]=unlock[i];
		data.debugOn=debugOn;


		bf.Serialize (file, data);
		file.Close();
		}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			gold = data.gold;
			ability=data.ability;
			score=data.score;
			for(int i=1; i<=10; i++)
			unlock[i]=data.unlock[i];
			unlock[0]=1;
			debugOn=data.debugOn;
		}
	}

	public void Start(){
		if (Application.loadedLevelName == "Logo")
			return;
		if (!File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			gold = ability = score = 0;
			debugOn=false;
			for (int i=1; i<=10; i++)
				unlock [i] = 0;
			Save ();
		}
		debugPass = 0;
		lost = false;
		Load ();
		if (Application.loadedLevelName == "Frost Cavern"||Application.loadedLevelName == "Death Star"||Application.loadedLevelName=="Pirate Ship")
			return;
		/*switch (ability) {
		case 0:	GameObject.FindObjectOfType<ButtonEmptyTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 1:	GameObject.FindObjectOfType<ButtonShieldTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 2:	GameObject.FindObjectOfType<ButtonMagnetizeTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 3:	GameObject.FindObjectOfType<ButtonHardenTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 4:	GameObject.FindObjectOfType<ButtonSawTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 5:	GameObject.FindObjectOfType<ButtonBankTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 6:	GameObject.FindObjectOfType<ButtonDefuseTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 7:	GameObject.FindObjectOfType<ButtonGhostTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 8:	GameObject.FindObjectOfType<ButtonFrostTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		case 9:	GameObject.FindObjectOfType<ButtonForceTarget> ().transform.position = new Vector3 (6.27f, 0, -3);break;
		}*/
		
	}
[Serializable]
class PlayerData{
	
		public int gold;
		public int ability;
		public int score;
		public int[] unlock = new int[60];
		public bool debugOn;
	}
}
