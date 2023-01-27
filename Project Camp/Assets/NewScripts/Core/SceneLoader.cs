using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;

namespace BeaconProject.Core
{
	public class SceneLoader
	{
		private readonly ICoroutineRunner _runner;

		public SceneLoader(ICoroutineRunner runner)
		{
			_runner = runner;
		}

		public void Load(string name, Action onLoaded = null) =>
			_runner.StartCoroutine(SceneLoading(name, onLoaded));

		private IEnumerator SceneLoading(string name, Action onLoaded = null)
		{
			AsyncOperation waitForLoad = SceneManager.LoadSceneAsync(name);

			while (!waitForLoad.isDone)
				yield return null;

			onLoaded?.Invoke();
		}
	}
}

