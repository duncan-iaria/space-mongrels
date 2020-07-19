using System.Collections.Generic;
using UnityEngine;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Inventory/DropTable", order = 100)]
  public class DropTable : ScriptableObject
  {
    public List<DropItem> dropList = new List<DropItem>();
    public GameObject itemPrefab;

    public void generateDrop(Vector3 tPosition)
    {
      foreach (DropItem dropItem in dropList)
      {
        if (Random.Range(0f, 100f) < dropItem.dropChance)
        {
          GameObject tempItemGameObject = Instantiate(itemPrefab, tPosition, Quaternion.identity);

          SMItem tempItem = tempItemGameObject.GetComponent<SMItem>();
          tempItem.Item = dropItem.item;
          tempItem.drop();
        }
      }
    }
  }
}