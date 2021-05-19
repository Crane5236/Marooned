using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    public int furnaceLevel;
    public int smeltingRate;

    public InputBox inputBox;
    public OutputBox outputBox;
    public Text levelText;
    public SpriteRenderer flameSprite;

    private Color orange;

    // Start is called before the first frame update
    void Start()
    {
        orange.a = 1;
        orange.r = 1;
        orange.g = 0.566588f;
        orange.b = 0.04245281f;

        flameSprite.color = Color.black;

        furnaceLevel = 1;
        StartCoroutine(RunFurnace());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }

    void Smelt()
    {
        if (inputBox.oreToSmelt > 0 && outputBox.oreInBox < outputBox.maxSize)
        {
            flameSprite.color = orange;
            inputBox.oreToSmelt -= smeltingRate;
            outputBox.CollectOre(smeltingRate);
        }
        else
        {
            flameSprite.color = Color.black;
        }
    }

    IEnumerator RunFurnace()
    {
        while(true)
        {
            Smelt();

            yield return new WaitForSeconds(1f);
        }
    }

    public void LevelUp()
    {
        switch (furnaceLevel)
        {
            case 1:
                furnaceLevel++;
                levelText.text = "Furnace \nLevel " + furnaceLevel;
                smeltingRate = 10;
                break;
            case 2:
                furnaceLevel++;
                levelText.text = "Furnace \nLevel " + furnaceLevel;
                smeltingRate = 50;
                break;
            case 3:
                furnaceLevel++;
                levelText.text = "Furnace \nLevel " + furnaceLevel;
                smeltingRate = 250;
                break;
            case 4:
                furnaceLevel++;
                levelText.text = "Furnace \nLevel " + furnaceLevel;
                smeltingRate = 1500;
                break;
            default:
                break;
        }
    }
}
