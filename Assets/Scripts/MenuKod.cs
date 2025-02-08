using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuKod : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Oyun sahnesinin ad�n� yaz
    }

    public void OpenMarket()
    {
        SceneManager.LoadScene("MarketScene"); // Market sahnesinin ad�n� yaz
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapand�! (�al��t�r�rken Unity Edit�r'de kapanmaz.)");
    }
}
