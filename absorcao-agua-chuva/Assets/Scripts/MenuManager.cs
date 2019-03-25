using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	
    public void QuitGame () {
        Application.Quit();
    }
    
    public void LoadScene (string scene) {
        SceneManager.LoadScene(scene);
    }
}
