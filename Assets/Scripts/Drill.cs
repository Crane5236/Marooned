using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    public int drillLevel;
    public int collectionRate;

    public OutputBox outputBox;
    public Text levelText;

    private void Start()
    {
        drillLevel = 1;

        StartCoroutine(RunDrill());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }

    void Mine()
    {
        outputBox.CollectOre(collectionRate);
    }

    IEnumerator RunDrill()
    {
        while(true)
        {
            Mine();

            yield return new WaitForSeconds(1f);
        }
    }

    public void LevelUp()
    {
        switch (drillLevel)
        {
            case 1:
                drillLevel++;
                levelText.text = "Drill \nLevel " + drillLevel;
                collectionRate = 10;
                break;
            case 2:
                drillLevel++;
                levelText.text = "Drill \nLevel " + drillLevel;
                collectionRate = 50;
                break;
            case 3:
                drillLevel++;
                levelText.text = "Drill \nLevel " + drillLevel;
                collectionRate = 250;
                break;
            case 4:
                drillLevel++;
                levelText.text = "Drill \nLevel " + drillLevel;
                collectionRate = 1500;
                break;
            default:
                break;
        }
    }
}
