using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menus : MonoBehaviour
{
    public void doExitGame()
    {
        Application.Quit();
        Debug.Log("Çıkış yapıldı");
    }
	public void goMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
