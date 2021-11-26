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
    public string scenceLoad;
    public Vector3 posision;
    public int souls;
    //Cofres
    public bool[] chest;
    public bool[] wall;
    //Currency
    public Vector3 recoverSoulsPosition;
    public int recoverSoulsCount;
    public bool needRecover;


    public GameData() {
        //scenceLoadPlayer=pasamos el val del Avility SO;
        //soulsPlayer= pasamos el val del Avility SO;     (Renombrar)
        //x_Player, y_Player, z_Player=pasamos el val del Avility SO;
    }

    public void SaveData() {
				
    }

	public void LoadData() {
	
	}
}