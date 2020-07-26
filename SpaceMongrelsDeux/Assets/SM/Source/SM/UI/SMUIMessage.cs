using UnityEngine;
using TMPro;

namespace SM
{
  public class SMUIMessage : MonoBehaviour
  {
    public string Message
    {
      get { return message; }
      set { message = value; init(); }
    }

    public float removeAnimationDuration = .5f;
    public float removeAnimationDistance = 2f;

    public float removeAnimationScaleTarget = 0.7f;

    private string message;
    public TextMeshProUGUI messageTextUI;

    public void init()
    {
      messageTextUI.text = message;
    }

    public void remove()
    {
      LeanTween.moveY(this.gameObject, transform.position.y + removeAnimationDistance, removeAnimationDuration).setEaseInSine().setOnComplete(onRemove);
      LeanTween.scale(this.gameObject, new Vector3(removeAnimationScaleTarget, removeAnimationScaleTarget, removeAnimationScaleTarget), removeAnimationDuration).setEaseInSine();
    }

    public void onRemove()
    {
      Destroy(this.gameObject);
    }
  }
}
