using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmashMonsters.Scenes.Base
{
	public class BaseScreenController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		public virtual void LoadScene(SceneAsset scene)
		{
			SceneManager.LoadSceneAsync(scene.name);
		}

	}
}