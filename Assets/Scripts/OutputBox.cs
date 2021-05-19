using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputBox : MonoBehaviour
{
    public int oreInBox = 0;
    public int maxSize = 100;
    public int level;
    public bool isSmelted;

    private bool canUse;

    public Text capacityText;
    public Text buttonPrompt;
    public Player player;

    private void Start()
    {
        buttonPrompt.text = "E";
        buttonPrompt.gameObject.SetActive(false);
        canUse = false;
        level = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e") && canUse)
        {
            if (isSmelted == false)
            {
                player.ore += oreInBox;
                player.totalOre += oreInBox;
            }
            else
                player.smeltedOre += oreInBox;

            oreInBox = 0;
            capacityText.text = oreInBox + " /\n" + maxSize;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }

    public void CollectOre(int ore)
    {
        if (oreInBox + ore > maxSize)
            oreInBox = maxSize;
        else if (oreInBox < maxSize)
            oreInBox += ore;

        capacityText.text = oreInBox + " /\n" + maxSize;
    }
}
