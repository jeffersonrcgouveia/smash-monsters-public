using System.Collections.Generic;
using System.Text;
using SmashMonsters.Code.Characters.Base.Hurt;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SmashMonsters.Scenes.Stages.BaseStage
{
	public class CharacterAttributesPanelController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Constants
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		[Inject]
		private GameState.GameState gameState;

		/*----------------------------------------------------------------------------------------*
		 * SerializeField
		 *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private GameState.GameState.Player player;

		/*----------------------------------------------------------------------------------------*
		 * Attributes
		 *----------------------------------------------------------------------------------------*/
	
		private readonly List<GameObject> _livesGOs = new List<GameObject>();

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			if (!gameState.CharactersGOByPlayer.ContainsKey(player))
			{
				gameObject.SetActive(false);
				return;
			}

			GameObject character = gameState.CharactersGOByPlayer[player];
		
			HandleLivesImages(character);

			TextMeshProUGUI damageText = GetComponentInChildren<TextMeshProUGUI>();

			CharacterHurtController hurtController = character.GetComponent<CharacterHurtController>();
			hurtController.Lives.AddObserver(lives =>
			{
				GameObject lastGO = _livesGOs[_livesGOs.Count - 1];
				_livesGOs.Remove(lastGO);
				Destroy(lastGO);
			});
			hurtController.Damage.AddObserver(damage =>
			{
				damageText.text = new StringBuilder(damage.Value.ToString()).Append("%").ToString();
			});
		}

		private void HandleLivesImages(GameObject character)
		{
			SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer>();
			foreach (Image image in GetComponentsInChildren<Image>())
			{
				image.sprite = spriteRenderer.sprite;
				_livesGOs.Add(image.gameObject);
			}
		}

		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Utility Methods
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Coroutines
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/
	}
}
