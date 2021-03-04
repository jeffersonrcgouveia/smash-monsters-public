using System.Collections.Generic;
using SmashMonsters.Code.Characters.Base;
using UnityEngine;

namespace SmashMonsters.Scenes.Stages.BaseStage
{
	public class CharactersPrefabsController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private GameObject lizartasPrefab;

		[SerializeField]
		private GameObject pikachuPrefab;

		/*----------------------------------------------------------------------------------------*
		 * Attributes
		 *----------------------------------------------------------------------------------------*/

		public Dictionary<Character, GameObject> CharactersPrefabByCharacter { get; private set; }

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			CharactersPrefabByCharacter = new Dictionary<Character, GameObject>
			{
				{Character.Lizartas, lizartasPrefab},
				{Character.Pikachu, pikachuPrefab}
			};
		}

	}
}
