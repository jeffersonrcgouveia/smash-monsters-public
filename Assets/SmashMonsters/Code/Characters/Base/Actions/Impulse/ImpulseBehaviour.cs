using SmashMonsters.Code.Characters.Base.ImpulseDown;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Actions.Impulse
{
	public class ImpulseBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterImpulseDownController _impulseDownController;

		/*----------------------------------------------------------------------------------------*
		 * Exposed Variables
		 *----------------------------------------------------------------------------------------*/
	
		[field:SerializeField]
		public float ImpulseTime { get; set; }

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			PopulateComponents(animator);
			_impulseDownController.StartLerpImpulse(ImpulseTime);
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			_impulseDownController.StopLerpImpulse();
		}

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/

		private void PopulateComponents(Animator animator)
		{
			if (_impulseDownController != null) return;
			_impulseDownController = animator.GetComponent<CharacterImpulseDownController>();
		}

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

	}
}
