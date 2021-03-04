using SmashMonsters.Scenes.Base;
using TMPro;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace SmashMonsters.Scenes.Stages.BaseStage
{
	public class TimeController : BaseScreenController
	{
		/*----------------------------------------------------------------------------------------*
		 * Constants
		 *----------------------------------------------------------------------------------------*/

		private const float SecondsByMinute = 60;

		private const string TimeFormat = "00";
	
		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/

		[Inject]
		private GameState.GameState gameState;

		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private TextMeshProUGUI _timerText;

		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private SceneAsset celebrationScene;

		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/

		private float _time;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			_timerText = GetComponent<TextMeshProUGUI>();

			if (gameState.Timer > 0)
			{
				_time = gameState.Timer * SecondsByMinute;
			}
			else
			{
				gameObject.SetActive(false);
			}
		}

		private void FixedUpdate()
		{
			_time -= Time.fixedDeltaTime;
		
			float minutes = Mathf.Floor(_time / SecondsByMinute);
			float seconds = Mathf.Floor(_time % SecondsByMinute);
		
			string minutesText = minutes.ToString(TimeFormat);
			string secondsText = seconds.ToString(TimeFormat);
		
			_timerText.text = $"{minutesText} : {secondsText}";

			if (minutes <= 0 && seconds <= 0 /*&& gameState.Timer != 3*/)
			{
				LoadScene(celebrationScene);
			}
		}

	}
}
