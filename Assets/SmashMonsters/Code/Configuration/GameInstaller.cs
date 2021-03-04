using SmashMonsters.Code.Characters.Base.Actions.Cooldown;
using SmashMonsters.Code.Characters.Base.Actions.Impulse;
using SmashMonsters.Services;
using Zenject;

namespace SmashMonsters.Configuration
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<GameState.GameState>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
			Container.Bind<AudioService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        
			Container.Bind<InputActionCooldownHelper>().AsTransient();
			Container.Bind<InputActionDashHelper>().AsTransient();
		}

	}
}