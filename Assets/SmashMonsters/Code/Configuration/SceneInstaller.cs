using SmashMonsters.Scenes.Stages.BaseStage;
using UnityEngine;
using Zenject;

namespace SmashMonsters.Configuration
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		private CharactersPrefabsController charactersPrefabsController;
	
		public override void InstallBindings()
		{
			Container.Bind<CharactersPrefabsController>().FromComponentInNewPrefab(charactersPrefabsController).AsSingle().NonLazy();
		}

	}
}