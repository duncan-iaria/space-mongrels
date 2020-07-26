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

    private string message;
    public TextMeshProUGUI messageTextUI;

    public void init()
    {
      messageTextUI.text = message;
    }
  }
}
