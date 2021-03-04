using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Movement
{
	public class MovementBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterTurnController _turnController;

		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (_turnController == null)
			{
				_turnController = animator.transform.GetComponent<CharacterTurnController>();
			}
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			_turnController.OnTurnAnimEnd();
		}
	}
}
