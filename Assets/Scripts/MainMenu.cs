using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image mainMenu;
    public Image tutorial1, tutorial2, tutorial3, tutorial4, tutorial5, tutorial6;
    public Player player;

    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        player.canControl = true;
    }

    public void Tutorial(int menu)
    {
        switch (menu)
        {
            case 0:
                tutorial1.gameObject.SetActive(true);
                mainMenu.gameObject.SetActive(false);
                break;
            case 1:
                tutorial2.gameObject.SetActive(true);
                tutorial1.gameObject.SetActive(false);
                break;
            case 2:
                tutorial3.gameObject.SetActive(true);
                tutorial2.gameObject.SetActive(false);
                break;
            case 3:
                tutorial4.gameObject.SetActive(true);
                tutorial3.gameObject.SetActive(false);
                break;
            case 4:
                tutorial5.gameObject.SetActive(true);
                tutorial4.gameObject.SetActive(false);
                break;
            case 5:
                tutorial6.gameObject.SetActive(true);
                tutorial5.gameObject.SetActive(false);
                break;
            case 6:
                mainMenu.gameObject.SetActive(true);
                tutorial6.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}
