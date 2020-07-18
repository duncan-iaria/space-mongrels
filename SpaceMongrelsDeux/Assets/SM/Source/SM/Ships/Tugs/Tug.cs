using UnityEngine;
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
            tempShip.mass = mass.value;
        }
        else
        {
            Debug.LogWarning("No ship was found on the GameObject - Have you set up your ship correctly?");
        }
    }

    public override void takeDamage(GameObject tObject, int tAmt)
    {
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

    public override void onCollision(GameObject tObject, Collision2D tCollision)
    {
        SMPawnShip tempShip = tObject.GetComponent<SMPawnShip>();
        if (tempShip)
        {
            int tempImpactDamage = (int)((Mathf.Abs(tCollision.relativeVelocity.x) + Mathf.Abs(tCollision.relativeVelocity.y)) * collisionDamageModifier);
            takeDamage(tObject, tempImpactDamage);
        }
    }

    public override void onDeath(GameObject tObject)
    {
        SMPawnShip tempShip = tObject.GetComponent<SMPawnShip>();
        if (tempShip)
        {
            Destroy(tObject);
        }

        Debug.Log("We Dead :(");
    }
}