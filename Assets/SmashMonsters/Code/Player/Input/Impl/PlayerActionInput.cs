using System.Collections.Generic;
using AvoEx.ObservableType;
using SmashMonsters.Player.Input.Utils;
using UnityEngine;
using static SmashMonsters.Player.Input.CharacterActionInput;
using static SmashMonsters.Player.Input.CharacterActionInput.InputAction;
using static SmashMonsters.Player.Input.Utils.PlayerInputUtils;

namespace SmashMonsters.Player.Input.Impl
{
	public class PlayerActionInput : MonoBehaviour, CharacterActionInput
	{
		/*----------------------------------------------------------------------------------------*
		 * Constants
		 *----------------------------------------------------------------------------------------*/
	
		private const float MinMovementAxis = 0.0f;
		private const float MaxMovementAxis = 0.3f;
	
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterInputController _inputController;
	
		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/
    
		private readonly Dictionary<CharacterActionInput.InputAction, HoldingActionInfo> _holdingActionsByActions =
			new Dictionary<CharacterActionInput.InputAction, HoldingActionInfo>();
    
		public ObBool IsActing { get; } = new ObBool();
    
		public CharacterActionInput.InputAction Action { get; private set; }

		/*----------------------------------------------------------------------------------------*
		 * Custom Events
		 *----------------------------------------------------------------------------------------*/

		public CharacterActionInput.IsGroundedEvent IsGroundedCallback { get; set; }

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			_inputController = GetComponent<CharacterInputController>();
		}

		private void Update()
		{
			if (UnityEngine.Input.GetButtonDown(SimpleAttack.ToString()))
			{
				bool? isGrounded = IsGroundedCallback?.Invoke();
				if (!isGrounded.HasValue || isGrounded.Value)
				{
					CharacterActionInput.InputAction action = ChooseActionByMovingDirection(SimpleAttack,
						SimpleAttackTop,
						SimpleAttackFront,
						SimpleAttackBottom,
						SimpleHeavyAttackTop,
						SimpleHeavyAttackFront,
						SimpleHeavyAttackBottom);

					Action = action;
				}
				else
				{
					CharacterActionInput.InputAction action = ChooseActionByMovingDirection(AirAttack,
						AirAttackTop,
						AirAttackFront,
						AirAttackBottom);

					Action = action;
				}
			
				IsActing.Value = true;
			}
			else if (UnityEngine.Input.GetButtonDown(SpecialAttack.ToString()))
			{
				bool? canAct = IsGroundedCallback?.Invoke();
				if (!canAct.HasValue || canAct.Value)
				{
					CharacterActionInput.InputAction action = ChooseActionByMovingDirection(SpecialAttack,
						SpecialAttackTop,
						SpecialAttackFront,
						SpecialAttackBottom,
						SpecialHeavyAttackTop,
						SpecialHeavyAttackFront,
						SpecialHeavyAttackBottom);

					Action = action;
					IsActing.Value = true;
				}
			}
			else
			{
				IsActing.Value = false;
			}

			HandleHoldingButtons();
		}

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/

		private CharacterActionInput.InputAction ChooseActionByMovingDirection(CharacterActionInput.InputAction action,
			CharacterActionInput.InputAction actionTop,
			CharacterActionInput.InputAction actionFront,
			CharacterActionInput.InputAction actionBottom,
			CharacterActionInput.InputAction? heavyActionTop = null,
			CharacterActionInput.InputAction? heavyActionFront = null,
			CharacterActionInput.InputAction? heavyActionBottom = null)
		{
			CharacterActionInput.InputAction resultAction = action;

			float verticalAxis = _inputController.Movement.Vertical;
			float horizontalAxis = _inputController.Movement.Horizontal;

			float absVerticalAxis = Mathf.Abs(_inputController.Movement.Vertical);
			float absHorizontalAxis = Mathf.Abs(_inputController.Movement.Horizontal);

			if (absVerticalAxis > 0)
			{
				resultAction = ChooseActionByAxisDirection(verticalAxis, _inputController.Movement.LastVertical,
					action,
					actionTop,
					actionBottom,
					heavyActionTop,
					heavyActionBottom);
			}
			else if (absHorizontalAxis > 0)
			{
				resultAction = ChooseActionByAxisDirection(horizontalAxis, _inputController.Movement.LastHorizontal,
					action,
					actionFront,
					actionFront,
					heavyActionFront,
					heavyActionFront);
			}

			// Debug.Log("action: " + resultAction);

			return resultAction;
		}

		private CharacterActionInput.InputAction ChooseActionByAxisDirection(float axis, float lastAxis,
			CharacterActionInput.InputAction action,
			CharacterActionInput.InputAction actionLesserThanZero,
			CharacterActionInput.InputAction actionGreaterThanZero,
			CharacterActionInput.InputAction? heavyActionLesserThanZero = null,
			CharacterActionInput.InputAction? heavyActionGreaterThanZero = null)
		{
			CharacterActionInput.InputAction resultAction = action;
			if (axis > 0 && lastAxis <= axis)
			{
				if (heavyActionLesserThanZero.HasValue)
				{
					resultAction = axis < MaxMovementAxis ? heavyActionLesserThanZero.Value : actionLesserThanZero;
				}
				else
				{
					resultAction = actionLesserThanZero;
				}
			}
			else if (axis < 0 && lastAxis >= axis)
			{
				if (heavyActionGreaterThanZero.HasValue)
				{
					resultAction = axis > -MaxMovementAxis ? heavyActionGreaterThanZero.Value : actionGreaterThanZero;
				}
				else
				{
					resultAction = actionGreaterThanZero;
				}
			}

			return resultAction;
		}

		private void HandleHoldingButtons()
		{
			foreach (KeyValuePair<CharacterActionInput.InputAction, HoldingActionInfo> pair in _holdingActionsByActions)
			{
				HoldingActionInfo info = pair.Value;
				HoldButton(pair.Key.ToString(), info.onHoldingStart, info.onHolding, info.onHoldingEnd,
					info.waitClickTimeToFireOnHolding);
			}
		}

		public void AddHoldingAction(CharacterActionInput.InputAction action, PlayerInputUtils.ButtonValidateCallBack onHoldingStart,
			PlayerInputUtils.ButtonHoldingCallBack onHolding, PlayerInputUtils.ButtonCallBack onHoldingEnd, bool waitClickTimeToFireOnHolding = true)
		{
			HoldingActionInfo info;
			info.onHoldingStart = onHoldingStart;
			info.onHolding = onHolding;
			info.onHoldingEnd = onHoldingEnd;
			info.waitClickTimeToFireOnHolding = waitClickTimeToFireOnHolding;
        
			_holdingActionsByActions.Add(action, info);
		}

		public bool RemoveHoldingAction(CharacterActionInput.InputAction action)
		{
			return _holdingActionsByActions.Remove(action);
		}

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

		public struct HoldingActionInfo
		{
			internal PlayerInputUtils.ButtonValidateCallBack onHoldingStart;
			internal PlayerInputUtils.ButtonHoldingCallBack onHolding;
			internal PlayerInputUtils.ButtonCallBack onHoldingEnd;
			internal bool waitClickTimeToFireOnHolding;
		}
	}
}
