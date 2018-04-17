using UnityEngine;
using System.Collections;
using SM;


[CreateAssetMenu(menuName = "SM/Ships/Reactor", order = 100)]
public class Reactor : SMReactor
{
    public override void initialize(GameObject tObject)
    {
        SMPawnShip tempShip = tObject.GetComponent<SMPawnShip>();
        if (tempShip != null)
        {
            tempShip.moveSpeed = moveSpeed;
            tempShip.horizontalDampening = horizontalDampening;
            tempShip.rotationSpeed = rotationSpeed;
        }
        else
        {
            Debug.LogWarning("No ship was found on the GameObject - Have you set up your ship correctly?");
        }
    }
}