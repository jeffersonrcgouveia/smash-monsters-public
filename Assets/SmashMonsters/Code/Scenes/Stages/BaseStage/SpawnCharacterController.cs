using System.Collections.Generic;
using SmashMonsters.Code.Characters.Base;
using SmashMonsters.Code.Characters.Base.Hurt;
using SmashMonsters.Player.Input;
using SmashMonsters.Player.Input.Impl;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SmashMonsters.Scenes.Stages.BaseStage
{
	public class SpawnCharacterController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		[Inject]
		private DiContainer diContainer;

		[Inject]
		private GameState.GameState gameState;

		[Inject]
		private CharactersPrefabsController charactersPrefabs;

		/*----------------------------------------------------------------------------------------*
		 * Exposed Events
		 *----------------------------------------------------------------------------------------*/
	
		[field:SerializeField]
		public UnityEvent<Transform> OnSpawn { get; set; }
	
		/*----------------------------------------------------------------------------------------*
		 * SerializeField
		 *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private Transform playerOneSpawnPoint;

		[SerializeField]
		private Transform playerTwoSpawnPoint;

		[SerializeField]
		private Transform playerTreeSpawnPoint;

		[SerializeField]
		private Transform playerFourSpawnPoint;

		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/

		private Dictionary<GameState.GameState.Player, Transform> _spawnPointsByPlayer;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			_spawnPointsByPlayer = new Dictionary<GameState.GameState.Player, Transform>
			{
				{GameState.GameState.Player.PlayerOne, playerOneSpawnPoint}, 
				{GameState.GameState.Player.PlayerTwo, playerTwoSpawnPoint}, 
				{GameState.GameState.Player.PlayerTree, playerTreeSpawnPoint}, 
				{GameState.GameState.Player.PlayerFour, playerFourSpawnPoint}
			};
		
			foreach (KeyValuePair<GameState.GameState.Player, Character> pair in gameState.CharactersByPlayer)
			{
				SpawnCharacter(pair.Key, charactersPrefabs.CharactersPrefabByCharacter[pair.Value],
					_spawnPointsByPlayer[pair.Key].position);
			}
		}

		private void SpawnCharacter(GameState.GameState.Player player, GameObject characterPrefab, Vector3 spawnPoint)
		{
			GameObject character = diContainer.InstantiatePrefab(characterPrefab, spawnPoint, Quaternion.identity, null);
		
			if (player == GameState.GameState.Player.PlayerOne)
			{
				character.AddComponent<PlayerMovementInput>();
				character.AddComponent<PlayerActionInput>();
			}
			else
			{
				character.AddComponent<CPUMovementInput>();
				character.AddComponent<CPUActionInput>();
			}
			CharacterInputController inputController = character.GetComponent<CharacterInputController>();
			inputController.PopulateInputs();
		
			OnSpawn?.Invoke(character.transform);
		
			CharacterHurtController hurtController = character.GetComponent<CharacterHurtController>();
			hurtController.Lives.Value = gameState.Lives;
			hurtController.Lives.AddObserver(lives =>
			{
				if (lives > 0)
				{
					character.transform.position = spawnPoint;
				}
				else
				{
					Destroy(character);
					gameState.Winners.Remove(player);
				}
			});
			gameState.CharactersGOByPlayer[player] = character;
		}

	}
}
