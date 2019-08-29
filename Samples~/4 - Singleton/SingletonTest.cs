using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// We need to import the namespace to use Singleton
using UniToolkit.Utility;

namespace UniToolkit.Examples
{
    public class SingletonTest : MonoSingleton<SingletonTest>
    {

        public Text HelloText;

        public float LetterIteration = 0.25f;

        public void SayHello(string Name)
        {
            StartCoroutine(mSayText("Hello " + Name + "."));
        }

        IEnumerator mSayText(string cont)
        {

            string saytxt = ""; 


            for (int i = 0; i < cont.Length; i++)
            {

                saytxt += cont[i];

                HelloText.text = saytxt;

                yield return new WaitForSeconds(LetterIteration);
            }

            StopAllCoroutines();

        }

    }
}