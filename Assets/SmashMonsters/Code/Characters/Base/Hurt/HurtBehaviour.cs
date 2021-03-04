using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Hurt
{
	public class HurtBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterHurtController _characterHurtController;

		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (_characterHurtController == null)
			{
				_characterHurtController = animator.transform.GetComponent<CharacterHurtController>();
			}

			_characterHurtController.GetUp();
		}
	}
}
