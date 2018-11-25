using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SM
{
    public class SMNotificationFlag : MonoBehaviour
    {
        public TextMeshPro notificationMessage;

        public void setMessageAndLocation(string tMessage, Vector3 tTargetPosition)
        {
            setMessage(tMessage);
            setLocation(tTargetPosition);
        }

        private void setMessage(string tMessage)
        {
            notificationMessage.text = tMessage;
        }

        private void setLocation(Vector3 tTargetPostion)
        {
            transform.position = tTargetPostion;
        }
    }
}
