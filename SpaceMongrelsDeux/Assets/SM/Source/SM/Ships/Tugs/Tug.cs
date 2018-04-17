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
        }
        else
        {
            Debug.LogWarning("No ship was found on the GameObject - Have you set up your ship correctly?");
        }
    }
}