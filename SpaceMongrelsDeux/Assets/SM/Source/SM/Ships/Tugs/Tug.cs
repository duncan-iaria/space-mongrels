using UnityEngine;
using System.Collections;
using SM;


[CreateAssetMenu(menuName = "SM/Ships/Tug", order = 100)]
public class Tug : SMShip
{
    public override void initialize(GameObject tObject)
    {
        SMPawnShip tempShip = tObject.GetComponent<SMPawnShip>();
        if (tempShip != null)
        {
            tempShip.maxHealth = maxHealth;
            tempShip.currentHealth = maxHealth;
        }
        else
        {
            Debug.LogWarning("No ship was found on the GameObject - Have you set up your ship correctly?");
        }
    }

    public override void takeDamage(GameObject tObject, int tAmt)
    {
        Debug.Log("Take damage called on ship");
        SMPawnShip tempShip = tObject.GetComponent<SMPawnShip>();
        if (tempShip)
        {
            int tempHealth = tempShip.currentHealth -= tAmt;
            if (tempHealth <= 0)
            {
                onDeath(tObject);
            }
            else
            {
                tempShip.currentHealth = tempHealth;
            }
        }
    }

    public override void onDeath(GameObject tObject)
    {
        Debug.Log("We Dead :(");
    }
}