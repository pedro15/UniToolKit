using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UniToolkit.Examples.Singleton
{
    public class SingletonExampleusr : MonoBehaviour
    {
        public InputField InputText;
        public Button btn;
        
        private void Start()
        {
            btn.onClick.AddListener(new UnityEngine.Events.UnityAction(SendMessage));   
        }

        void SendMessage()
        {
            SingletonTest.Instance.SayHello(InputText.text);
        }
    }
}