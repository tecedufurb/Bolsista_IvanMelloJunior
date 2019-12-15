using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TerrainType {
    PERMEAVEL, SEMI_PERMEAVEL, IMPERMEAVEL
};

public class MenuManager : MonoBehaviour {
	
    public static TerrainType terrainType;

    public void QuitGame () {
        Application.Quit();
    }
    
    public void LoadScene (string scene) {
        SceneManager.LoadScene(scene);
    }

    public void SelectTerrainType(int type) {
        terrainType = (TerrainType)type;
        SceneManager.LoadScene("main");
    }
}
