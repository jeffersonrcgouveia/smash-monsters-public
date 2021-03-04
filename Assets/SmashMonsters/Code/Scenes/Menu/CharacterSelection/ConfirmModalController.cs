using TopDownMedieval.Plugins.Commons.Utils;
using UnityEngine;

namespace SmashMonsters.Scenes.Menu.CharacterSelection
{
	public class ConfirmModalController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		private CanvasGroup _canvas;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			_canvas = GetComponent<CanvasGroup>();
		}

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void Show()
		{
			CanvasUtils.Show(_canvas);
		}

		public void Hide()
		{
			CanvasUtils.Hide(_canvas);
		}

	}
}
