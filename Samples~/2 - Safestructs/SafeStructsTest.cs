using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniToolkit.Security;

namespace UniToolkit.Examples
{
    public class SafeStructsTest : MonoBehaviour
    {
        /* 
         * Declare it as:
         * SafeInt ( for int )
         * SafeFloat (for float)
         * SafeBool (for bool)
         * SafeString (for string)
         * 
         * */

        public SafeInt MySecuredInt;

        [SerializeField]
        private SafeString MySecuredText = "Hello safe world !";
       
        private IEnumerator Start()
        {

            Debug.Log("My Secured Text:  " +  MySecuredText);

            while(Application.isPlaying)
            {
                // You can assig de type directly like you assing normally :)
                MySecuredInt = Random.Range(0, 100);
                
                yield return new WaitForSeconds(2f);
            }

        }

        private void OnGUI()
        {
            GUI.Label(new Rect(Screen.width / 2 - 25 , 25 , 100, 50), MySecuredInt.ToString(), new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = Color.white },
                fontSize = 48
            });

            GUI.Label(new Rect(10, 100, 400, 100), "This is used to prevent memory hack at runtime," +
                " use it on sensitive data (Like player's health)");
        }

    }
}