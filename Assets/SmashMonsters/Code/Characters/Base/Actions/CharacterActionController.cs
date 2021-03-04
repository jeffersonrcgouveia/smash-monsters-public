using SmashMonsters.Code.Characters.Base.Actions.Cooldown;
using SmashMonsters.Code.Characters.Base.Movement;
using SmashMonsters.Code.Characters.Base.Projectiles;
using SmashMonsters.Player.Input;
using UnityEngine;
using Zenject;

namespace SmashMonsters.Code.Characters.Base.Actions
{
	[RequireComponent(typeof(Animator))]
	public class CharacterActionController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/

		protected Animator Animator { get; private set; }

		private CharacterGroundedController _groundedController;

		private CharacterInputController _inputController;

		private CharacterTurnController _turnController;

		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		[field: Inject]
		protected InputActionCooldownHelper CooldownHelper { get; }

		/*----------------------------------------------------------------------------------------*
		 * Attributes
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		protected ActHandler OnValidateActionCooldown { get; set; }

		protected virtual void Awake()
		{
			Animator = GetComponent<Animator>();
			_inputController = GetComponent<CharacterInputController>();
		}

		protected virtual void Start()
		{
			_turnController = GetComponent<CharacterTurnController>();
			_groundedController = GetComponent<CharacterGroundedController>();

			_inputController.Action.IsGroundedCallback += () => _groundedController.IsGrounded;
        
			_inputController.Action.IsActing.AddObserver(isActing =>
			{
				CharacterActionInput.InputAction action = _inputController.Action.Action;
				if (isActing && !CooldownHelper.ValidateCooldown(action)) return;
				SetAnimatorParameters(isActing, action);
				if (!isActing) return;
				OnValidateActionCooldown?.Invoke(action);
			});
		}

		private void Update()
		{
			CooldownHelper.UpdateCheck();
		}

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/

		private void SetAnimatorParameters(bool isActing, CharacterActionInput.InputAction action)
		{
			Animator.SetInteger("ActionIndex", (int) action);
			Animator.SetBool("IsActing", isActing);
		}

		/*----------------------------------------------------------------------------------------*
	     * Utility Methods
	     *----------------------------------------------------------------------------------------*/

		protected void SpawnProjectileForward(ProjectileController projectilePrefab, Transform fireSocket)
		{
			Vector3 shootDir = _turnController.IsFacingLeft ? Vector3.left : Vector3.right;
			ProjectileController projectile = Instantiate(projectilePrefab, fireSocket.transform.position, Quaternion.identity);
			projectile.Direction = shootDir;
		}

		/*----------------------------------------------------------------------------------------*
		 * Inner Classes and Delegates
		 *----------------------------------------------------------------------------------------*/

		protected delegate void ActHandler(CharacterActionInput.InputAction action);

	}
}
