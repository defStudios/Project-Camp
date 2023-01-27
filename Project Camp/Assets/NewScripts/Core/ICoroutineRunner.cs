using System.Collections;
using UnityEngine;

namespace BeaconProject.Core
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
