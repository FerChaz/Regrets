using UnityEngine;
using System;

public class SessionData {

	private static GameData GAME_DATA; 

	public static bool LoadData() {
        var valid = false;

        var data = PlayerPrefs.GetString("data", "");
        if (! string.IsNullOrWhiteSpace (data) ) {
	        var success = DESEncryption.TryDecrypt(data, out var original);
            Debug.Log($"Succes{success}");
	        if (success) {
		        GAME_DATA = JsonUtility.FromJson<GameData>(original);
                Debug.Log($"Desencriptado{original}");
		        GAME_DATA.LoadData();
		        valid = true;    
	        }
	        else {
		        GAME_DATA = new GameData();
	        }
            
        } else {
            GAME_DATA = new GameData();
        }
        Debug.Log($"Session data Load{ data}");
        return valid;
    }

    public static bool SaveData() {
        const bool valid = false;

        try {
            GAME_DATA.SaveData();
            var result = DESEncryption.Encrypt(JsonUtility.ToJson(SessionData.GAME_DATA));
            PlayerPrefs.SetString("data", result);
            PlayerPrefs.Save();
            Debug.Log($"Session data Save{ result}");
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
        }
        
        return valid;
    }

    public static GameData Data {
        get {
			if (GAME_DATA == null)
                LoadData();
            return GAME_DATA;
		}
    }

}


[Serializable]
public class GameData {
    //Put attributes that you want to save during your game.

    //Player
    public string spawnScene;
    public Vector3 spawnPlayerPosition;
    public int totalSouls;

    //Cofres, puertas, paredes, eventos
    public bool[] openChests = new bool[3];
    public bool[] brokenWalls = new bool[2];
    public bool[] openDoors = new bool[1];
    public bool[] eventsHappened = new bool[0];

    //Currency
    public Vector3 recoverSoulsPosition;
    public int recoverSoulsCount;
    public bool needRecover;


    public GameData() {

        spawnScene = "Intro";
        spawnPlayerPosition.Set(-412f, 1f, 0.0f);           // Posicion inicial en la intro del juego

        // Todos los cofres estan cerrados
        for (int i = 0; i < openChests.Length; i++)
        {
            openChests[i] = false;
        }

        // Todas las paredes estan sin romper
        for (int i = 0; i < brokenWalls.Length; i++)
        {
            brokenWalls[i] = false;
        }

        // Todas las puertas estan cerradas
        for (int i = 0; i < openDoors.Length; i++)
        {
            openDoors[i] = false;
        }

        // Ningun evento ocurrio
        for (int i = 0; i < eventsHappened.Length; i++)
        {
            eventsHappened[i] = false;
        }

        recoverSoulsPosition.Set(0.0f, 0.0f, 0.0f);
        recoverSoulsCount = 0;
        needRecover = false;
    }

    public void SaveData() {
				
    }

	public void LoadData() {
	
	}
}