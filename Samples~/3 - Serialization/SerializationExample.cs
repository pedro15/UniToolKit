using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Import this if you are using xml serialization
using System.Xml.Serialization;

// We need to import this to use serialization
using UniToolkit.Serialization;

//Note: If you want to change the encryption keys they are located in : 'EncryptionUtility.cs' file.

namespace UniToolkit.Examples
{
    public class SerializationExample : MonoBehaviour
    {

        #region Custom Classes 

        [System.Serializable, XmlRoot("Person")]
        public class PersonXML
        {
            [XmlArrayItem("Name", typeof(string))]
            public string Name;
            [XmlArrayItem("Age", typeof(string))]
            public int Age;

            public override string ToString()
            {
                return "Name: " + Name + " Age: " + Age;
            }

            public static implicit operator PersonXML (PersonNormal o)
            {
                return new PersonXML(o.Name, o.Age);
            }

            public PersonXML(string Name , int Age)
            {
                this.Name = Name;
                this.Age = Age;
            }

            public PersonXML() { }

        }

        [System.Serializable]
        public class PersonNormal
        {
            public string Name;
            public int Age;

            public static implicit operator PersonNormal (PersonXML oxml)
            {
                return new PersonNormal(oxml.Name, oxml.Age);
            }

            public PersonNormal(string Name , int Age)
            {
                this.Name = Name;
                this.Age = Age;
            }

            public PersonNormal() { }

        }

        #endregion

        #region Fields

        public Button ReloadButtun;
        public Button SaveButtun;

        public InputField NameField;
        public InputField AgeField;

        public Dropdown TypeComboBox;
        public Toggle EncryptedToggle;

        #endregion


        #region Private Fields

        PersonNormal _Person;

        public string FileName = "SavedFile";

        string Path
        {
            get
            {
                return System.IO.Path.Combine(Application.dataPath, FileName);
            }
        }

        enum SerializationType : int
        {
            Binary = 0,
            Json = 1,
            XML = 2
        }


        SerializationType SerType;

        bool IsEncrypted;

        #endregion



        private void Start()
        {
            ReloadButtun.onClick.AddListener( new UnityAction(ReloadSave));
            SaveButtun.onClick.AddListener(new UnityAction(Save));
            TypeComboBox.onValueChanged.AddListener(new UnityAction<int>(OptionChanged));
            EncryptedToggle.onValueChanged.AddListener(new UnityAction<bool>(TypeChanged));

            IsEncrypted = EncryptedToggle.isOn; 

        }


        // De Serialize 

        void ReloadSave()
        {

            if (System.IO.File.Exists(Path))
            {

                // To de-serialize we took only 1 Line :)

                if (SerType == SerializationType.Binary)
                {
                    // On Binary
                    _Person = BinarySerializer.Deserialize<PersonNormal>(Path, IsEncrypted);
                }
                else if (SerType == SerializationType.Json)
                {
                    // From Json
                    _Person = JSONSerializer.DeserializeFromFile<PersonNormal>(Path , IsEncrypted);
                }
                else if (SerType == SerializationType.XML)
                {
                    // From XML 
                    _Person = XMLSerializer.DeserializeXML<PersonXML>(Path, IsEncrypted);
                }

                Debug.Log("Loaded");

            }

            if (_Person != null )
            {
                NameField.text = _Person.Name;
                AgeField.text = _Person.Age.ToString();

            }

        }

        // Serialize

        void Save()
        {
            int age = 0;
            int.TryParse(AgeField.text, out age);
            
            _Person = new PersonNormal(NameField.text, age);

            // To Serialize we took only 1 lune too. :) 

            if (SerType == SerializationType.Binary)
            {
                // To binary
                BinarySerializer.Serialize(_Person, Path, IsEncrypted);
            }
            else if (SerType == SerializationType.Json)
            {
                // To JSON 
                JSONSerializer.SerializeToFile(_Person, Path , IsEncrypted);
            }
            else if (SerType == SerializationType.XML)
            {

                PersonXML xmlPerson = _Person; 

                // To XML
                XMLSerializer.SerializeXML(xmlPerson, Path, IsEncrypted);
            }

            Debug.Log("Saved");

        }

        void OptionChanged(int index)
        {
            SerType = (SerializationType)index;
        }

        void TypeChanged(bool val)
        {
            IsEncrypted = val;
        }

    }
}