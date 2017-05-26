using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterScene : MonoBehaviour {

    void Start() {
        Scene scene = SceneManager.GetActiveScene();
        string temp = scene.name.Substring(0,5);
        if (temp == "Level" || temp == "level") {
            SetLastLevel();
        }
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    
	public void SetLastLevel() {
		Scene scene = SceneManager.GetActiveScene();
		PlayerPrefs.SetString("Level", scene.name);
	}

    public void LoadLastLevel() {
        string lastlevel = PlayerPrefs.GetString("Level");
        PlayerPrefs.DeleteKey("Level");
        LoadScene(lastlevel);
    }
}
