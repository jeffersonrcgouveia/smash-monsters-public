using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CharacterController = SmashMonsters.Code.Characters.Base.CharacterController;

namespace SmashMonsters.Scenes.Menu.CharacterSelection
{
	public class LoadSelectableCharacters : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Constants
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Injects
	     *----------------------------------------------------------------------------------------*/
    
		[Inject]
		private DiContainer _container;
	
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private CanvasGroup confirmSelectedCanvasGroup;

		[SerializeField]
		private CharacterController[] characters;

		[SerializeField]
		private SelectableCharacterController selectableCharacterPrefab;
	
		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/

		private List<SelectableCharacterController> _characterControllers = new List<SelectableCharacterController>();
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			foreach (CharacterController character in characters)
			{
				GameObject instantiatePrefab = _container.InstantiatePrefab(selectableCharacterPrefab.gameObject);
				instantiatePrefab.transform.SetParent(transform);
				SelectableCharacterController controller = instantiatePrefab.GetComponent<SelectableCharacterController>();
				controller.SelectStageButtonCanvas = confirmSelectedCanvasGroup;
				controller.Character = character.Character;
				controller.Thumbnail.sprite = character.GetComponent<SpriteRenderer>().sprite;
				controller.Label.text = character.Name;
				_characterControllers.Add(controller);
			}
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void UnselectCharacters()
		{
			foreach (SelectableCharacterController controller in _characterControllers)
			{
				controller.Cancel();
			}
		}
	
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
