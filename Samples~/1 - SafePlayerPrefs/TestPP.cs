using UnityEngine;
using UniToolkit.Security;

namespace UniToolkit.Examples
{
    public class TestPP : MonoBehaviour
    {
        public string savekey;
        public float Speed = 10;

        void Start()
        {
            ReloadPosition();

            SafePlayerprefs.SetColor("color", Color.red);
            Debug.Log(SafePlayerprefs.GetColor("color"));
        }

        void ReloadPosition()
        {
            if (SafePlayerprefs.HasKey(savekey))
            {
                // Getting data from our PlayerPrefs.
                transform.position = SafePlayerprefs.GetVector3(savekey);

                Debug.Log("Position Loaded");
            }
        }

        private void Update()
        {

            Vector3 mm = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            mm *= Speed;

            transform.position += mm * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // To Store data simply call SetData method.
                SafePlayerprefs.SetVector3(savekey, transform.position);
                Debug.Log("Position Saved");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SafePlayerprefs.DeleteAll();
                Debug.Log("Position Deleted");
            }

            if (Input.GetKeyDown(KeyCode.R))
                ReloadPosition();

        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 25), "[Space] To Save Position.");

            GUI.Label(new Rect(10, 28, 300, 25), "[Escaoe] To Delete Position.");

            GUI.Label(new Rect(10, 44, 300, 25), "[R] To Reload Position.");
        }
    }
}