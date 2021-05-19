using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputBox : MonoBehaviour
{
    private bool canUse;
    public long oreToSmelt;

    public Text buttonPrompt;
    public Player player;
    public Image furnaceInputMenu;
    public Button input1, input10, input100, input1000, input10000, input100000, inputAll;

    // Start is called before the first frame update
    void Start()
    {
        canUse = false;
        oreToSmelt = 0;
        buttonPrompt.text = "E";
        buttonPrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && canUse)
        {
            furnaceInputMenu.gameObject.SetActive(true);
            player.canControl = false;
        }

        if (furnaceInputMenu.IsActive())
        {
            DisableButtons();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonPrompt.gameObject.SetActive(true);
            canUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonPrompt.gameObject.SetActive(false);
        canUse = false;
    }

    public void ExitMenu()
    {
        furnaceInputMenu.gameObject.SetActive(false);
        player.canControl = true;
    }

    private void DisableButtons()
    {
        if (player.ore < 1)
        {
            input1.interactable = false;
            inputAll.interactable = false;
        }
        else
        {
            input1.interactable = true;
            inputAll.interactable = true;
        }

        if (player.ore < 10)
            input10.interactable = false;
        else
            input10.interactable = true;

        if (player.ore < 100)
            input100.interactable = false;
        else
            input100.interactable = true;

        if (player.ore < 1000)
            input1000.interactable = false;
        else
            input1000.interactable = true;

        if (player.ore < 10000)
            input10000.interactable = false;
        else
            input10000.interactable = true;

        if (player.ore < 100000)
            input100000.interactable = false;
        else
            input100000.interactable = true;
    }

    public void InputOre(int ore)
    {
        player.ore -= ore;
        oreToSmelt += ore;
    }

    public void InputAllOre()
    {
        oreToSmelt += player.ore;
        player.ore = 0;
    }
}
