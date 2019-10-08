using UnityEngine;

namespace SM
{
  public interface ITargetable
  {
    // TODO is this method depricated now?
    void setSelected(GameObject tReticule);
    Transform getTransform();
  }
}
