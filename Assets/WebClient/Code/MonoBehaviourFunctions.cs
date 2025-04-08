using System.Collections;
using UnityEngine;

namespace WebClient
{
    public class MonoBehaviourFunctions : MonoBehaviour
    {
        public Coroutine RunCoroutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void KillCoroutine(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}