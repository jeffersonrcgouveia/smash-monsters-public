using AvoEx.ObservableType;
using SmashMonsters.Scenes.Base;
using SmashMonsters.Services;
using UnityEngine;
using Zenject;

namespace SmashMonsters.Scenes.Menu.Settings
{
	public class SettingsScreenController : BaseScreenController
	{
		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		[Inject]
		private GameState.GameState gameState;

		[Inject]
		private AudioService audioService;

		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private Texture2D cursorImage;

		[SerializeField]
		private RangeValuePanelController livesController;

		[SerializeField]
		private RangeValuePanelController timerController;
    
		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		private void OnEnable()
		{
			livesController.Value.Value = gameState.Lives;
			timerController.Value.Value = gameState.Timer;
		
			livesController.Value.AddObserver(LivesCallback);
			timerController.Value.AddObserver(TimerCallback);

			Vector2 hotSpot = new Vector2(cursorImage.width / 2, cursorImage.height / 2);
			Cursor.SetCursor(cursorImage, hotSpot, CursorMode.Auto);
		}

		private void OnDisable()
		{
			livesController.Value.DelObserver(LivesCallback);
			timerController.Value.DelObserver(TimerCallback);
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void LivesCallback(ObInt value)
		{
			gameState.Lives = value;
		}

		private void TimerCallback(ObInt value)
		{
			gameState.Timer = value;
		}

		public void MusicVolumeCallback(float volume)
		{
			audioService.SetBackgroundMusicVolume(volume);
		}

	}
}
