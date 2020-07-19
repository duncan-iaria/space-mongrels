using UnityEngine;
using SNDL;

namespace SM
{
  public abstract class SMShip : ScriptableObject
  {
    public string shipName;
    public int maxHealth;
    public SMLevelData interiorLevel;
    public float collisionDamageModifier = 1;

    public FloatVariable mass;

    public DropTable dropTable;

    public abstract void initialize(GameObject tObject);
    public abstract void takeDamage(GameObject tObject, int tAmt);
    public abstract void onCollision(GameObject tObject, Collision2D tCollision);
    public abstract void onDeath(GameObject tObject);
  }
}