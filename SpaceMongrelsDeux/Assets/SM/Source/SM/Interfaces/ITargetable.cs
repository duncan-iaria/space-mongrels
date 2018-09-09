using UnityEngine;

namespace SM
{
    public interface ITargetable
    {
        void setSelected(GameObject tReticule);
        Transform getTransform();
    }
}
