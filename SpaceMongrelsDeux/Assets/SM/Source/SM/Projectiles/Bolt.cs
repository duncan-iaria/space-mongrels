using UnityEngine;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Projectile/Bolt", order = 100)]
    public class Bolt : Projectile
    {
        public override void initialize(GameObject tObject)
        {
            SMProjectile tempProjectile = tObject.GetComponent<SMProjectile>();
            if (tempProjectile != null)
            {
                tempProjectile.damage = damage;
                tempProjectile.speed = speed;
                tempProjectile.isPenetrating = isPenetrating;
            }
            else
            {
                Debug.LogWarning("No ship was found on the GameObject - Have you set up your ship correctly?");
            }
        }
    }
}