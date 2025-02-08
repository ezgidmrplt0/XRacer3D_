using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuKod : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Oyun sahnesinin adýný yaz
    }

    public void OpenMarket()
    {
        SceneManager.LoadScene("MarketScene"); // Market sahnesinin adýný yaz
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapandý! (Çalýþtýrýrken Unity Editör'de kapanmaz.)");
    }
}
