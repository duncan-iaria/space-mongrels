using UnityEngine;
using System.Collections;

namespace SM
{
  public abstract class Projectile : ScriptableObject
  {
    public float lifeTimeInSeconds = 30;
    public int damage;
    public float speed;
    public bool isPenetrating;

    public abstract void initialize(GameObject tObject);
  }
}