using System.Diagnostics.CodeAnalysis;
using SmashMonsters.Code.Characters.Base.Hurt;
using SmashMonsters.Code.Characters.Base.Movement;
using SmashMonsters.Code.Characters.Base.Plugins.CSharpAnimator;
using UnityEngine;
using static SmashMonsters.Code.Characters.Base.BaseAnimatorController.State;
using static SmashMonsters.Code.Characters.Base.BaseAnimatorController.StateMachine;

namespace SmashMonsters.Code.Characters.Base
{
	[SuppressMessage("ReSharper", "HeapView.ObjectAllocation")]
	public class BaseAnimatorController : CSharpAnimator_ClassBase
	{
		/*----------------------------------------------------------------------------------------*
		 * Motions
		 *----------------------------------------------------------------------------------------*/

		[field: Header("Motions")]
		[field: SerializeField] protected Motion IdleMotion { get; set; }
		[field: SerializeField] protected Motion WalkMotion { get; set; }
		[field: SerializeField] protected Motion RunMotion { get; set; }
		[field: SerializeField] protected Motion RunTurnMotion { get; set; }
		[field: SerializeField] protected Motion SimpleAttackMotion { get; set; }
		[field: SerializeField] protected Motion SimpleAttackTopMotion { get; set; }
		[field: SerializeField] protected Motion SimpleAttackFrontMotion { get; set; }
		[field: SerializeField] protected Motion SimpleAttackBottomMotion { get; set; }
		[field: SerializeField] protected Motion SimpleHeavyAttackTopMotion { get; set; }
		[field: SerializeField] protected Motion SimpleHeavyAttackFrontMotion { get; set; }
		[field: SerializeField] protected Motion SimpleHeavyAttackBottomMotion { get; set; }
		[field: SerializeField] protected Motion SpecialAttackMotion { get; set; }
		[field: SerializeField] protected Motion SpecialAttackTopMotion { get; set; }
		[field: SerializeField] protected Motion SpecialAttackFrontMotion { get; set; }
		[field: SerializeField] protected Motion SpecialAttackBottomMotion { get; set; }
		[field: SerializeField] protected Motion SpecialHeavyAttackTopMotion { get; set; }
		[field: SerializeField] protected Motion SpecialHeavyAttackFrontMotion { get; set; }
		[field: SerializeField] protected Motion SpecialHeavyAttackBottomMotion { get; set; }
		[field: SerializeField] protected Motion JumpMotion { get; set; }
		[field: SerializeField] protected Motion JumpEndMotion { get; set; }
		[field: SerializeField] protected Motion DoubleJumpMotion { get; set; }
		[field: SerializeField] protected Motion AirAttackMotion { get; set; }
		[field: SerializeField] protected Motion AirAttackTopMotion { get; set; }
		[field: SerializeField] protected Motion AirAttackFrontMotion { get; set; }
		[field: SerializeField] protected Motion AirAttackBottomMotion { get; set; }
		[field: SerializeField] protected Motion HitMotion { get; set; }
		[field: SerializeField] protected Motion HurtFallingMotion { get; set; }
		[field: SerializeField] protected Motion HurtMotion { get; set; }
		[field: SerializeField] protected Motion GetUpMotion { get; set; }

		/*----------------------------------------------------------------------------------------*
		 * Parameters
		 *----------------------------------------------------------------------------------------*/

		protected ParameterData Horizontal { get; set; }
		protected ParameterData Vertical { get; set; }
		protected ParameterData CanTurn { get; set; }
		protected ParameterData IsActing { get; set; }
		protected ParameterData ActionIndex { get; set; }
		protected ParameterData StopAction { get; set; }
		protected ParameterData IsGrounded { get; set; }
		protected ParameterData JumpCounter { get; set; }
		protected ParameterData TakeDamage { get; set; }
		protected ParameterData IsTakingKnockback { get; set; }
		protected ParameterData IsGettingUp { get; set; }
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/
	
		public override AnimatorData Construct()
		{
			ConstructParams();
			ConstructGraph();
			ConstructTransitions();
		
			return currentData;
		}

		protected virtual void ConstructParams()
		{
			Horizontal = FloatParam("Horizontal");
			Vertical = FloatParam("Vertical");
			CanTurn = BoolParam("CanTurn");
			IsActing = BoolParam("IsActing");
			ActionIndex = IntParam("ActionIndex");
			StopAction = TriggerParam("StopAction");
			IsGrounded = BoolParam("IsGrounded");
			JumpCounter = IntParam("JumpCounter");
			TakeDamage = TriggerParam("TakeDamage");
			IsTakingKnockback = BoolParam("IsTakingKnockback");
			IsGettingUp = TriggerParam("IsGettingUp");
		}

		protected virtual GraphContext ConstructGraph()
		{
			return Graph()
			[
				Layer(Layer.Base).DefaultState(Idle)
				[
					StateMachine(Gait)
					[
						State(Idle, IdleMotion),
						State(Walk, WalkMotion),
						State(Run, RunMotion),
						State(RunTurn, RunTurnMotion).Behaviour<MovementBehaviour>()
					],
					StateMachine(InAir)
					[
						StateMachine(Jumps)
						[
							State(State.Jump, JumpMotion),
							State(JumpEnd, JumpEndMotion).Behaviour<CannotMoveBehaviour>(),
							State(DoubleJump, DoubleJumpMotion)
						],
						StateMachine(AirAttacks)
						[
							State(AirAttack, AirAttackMotion).Behaviour<StopMovingAndTurningBehaviour>(),
							State(AirAttackTop, AirAttackTopMotion).Behaviour<StopMovingAndTurningBehaviour>(),
							State(AirAttackFront, AirAttackFrontMotion).Behaviour<StopMovingAndTurningBehaviour>(),
							State(AirAttackBottom, AirAttackBottomMotion).Behaviour<StopMovingAndTurningBehaviour>()
						]
					],
					StateMachine(SimpleAttacks)
					[
						State(SimpleAttack, SimpleAttackMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SimpleAttackTop, SimpleAttackTopMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SimpleAttackFront, SimpleAttackFrontMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SimpleAttackBottom, SimpleAttackBottomMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SimpleHeavyAttackTop, SimpleHeavyAttackTopMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SimpleHeavyAttackFront, SimpleHeavyAttackFrontMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SimpleHeavyAttackBottom, SimpleHeavyAttackBottomMotion).Behaviour<StopMovingAndTurningBehaviour>()
					],
					StateMachine(SpecialAttacks)
					[
						State(SpecialAttack, SpecialAttackMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SpecialAttackTop, SpecialAttackTopMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SpecialAttackFront, SpecialAttackFrontMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SpecialAttackBottom, SpecialAttackBottomMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SpecialHeavyAttackTop, SpecialHeavyAttackTopMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SpecialHeavyAttackFront, SpecialHeavyAttackFrontMotion).Behaviour<StopMovingAndTurningBehaviour>(),
						State(SpecialHeavyAttackBottom, SpecialHeavyAttackBottomMotion).Behaviour<StopMovingAndTurningBehaviour>()
					],
					StateMachine(Hurts)
					[
						State(Hit, HitMotion),
						State(HurtFalling, HurtFallingMotion).Behaviour<HurtFallingBehaviour>(),
						State(State.Hurt, HurtMotion).Behaviour<HurtBehaviour>(),
						State(GetUp, GetUpMotion)
					]
				]
			];
		}

		protected virtual TransitionGroupContext ConstructTransitions()
		{
			ConstructGaitTransitions();
			ConstructAttacksTransitions();
			ConstructJumpsTransitions();
			ConstructAirAttacksTransitions();
			ConstructHurtTransitions();

//		TransitionGroupContext transitionGroupContext = Transitions()
//		transitionGroupContext = transitionGroupContext
//		[
//			Transition().SourceMultiple(Idle, Walk, Run).Destination(InAir)
//			[
//				Bool(IsGrounded, false)
//			]
//		];
//		return transitionGroupContext;

			return Transitions();
		}

		protected virtual TransitionGroupContext ConstructGaitTransitions()
		{
			return Transitions()
			[
				Transition().Entry().Destination(Walk)
				[
					Float(Horizontal, 0.5f, ConditionMode.Less),
					Float(Horizontal, 0.01f, ConditionMode.Greater)
				],
				Transition().Entry().Destination(Walk)
				[
					Float(Horizontal, -0.5f, ConditionMode.Greater),
					Float(Horizontal, -0.01f, ConditionMode.Less)
				],
				Transition().Entry().Destination(Run)
				[
					Float(Horizontal, -0.5f, ConditionMode.Less)
				],
				Transition().Entry().Destination(Run)
				[
					Float(Horizontal, 0.5f, ConditionMode.Greater)
				],
				Transition().Source(Idle).Destination(Walk).TransitionTime(0)
				[
					Float(Horizontal, -0.01f, ConditionMode.Less)
				],
				Transition().Source(Idle).Destination(Walk).TransitionTime(0)
				[
					Float(Horizontal, 0.01f, ConditionMode.Greater)
				],
				Transition().Source(Walk).Destination(Idle).TransitionTime(0)
				[
					Float(Horizontal, 0.01f, ConditionMode.Less),
					Float(Horizontal, -0.01f, ConditionMode.Greater)
				],
				Transition().Source(Walk).Destination(Run).TransitionTime(0)
				[
					Float(Horizontal, -0.5f, ConditionMode.Less)
				],
				Transition().Source(Walk).Destination(Run).TransitionTime(0)
				[
					Float(Horizontal, 0.5f, ConditionMode.Greater)
				],
				Transition().Source(Run).Destination(Walk).TransitionTime(0)
				[
					Float(Horizontal, 0.5f, ConditionMode.Less),
					Float(Horizontal, 0.01f, ConditionMode.Greater)
				],
				Transition().Source(Run).Destination(Walk).TransitionTime(0)
				[
					Float(Horizontal, -0.01f, ConditionMode.Less),
					Float(Horizontal, -0.5f, ConditionMode.Greater)
				],
				Transition().Source(Run).Destination(RunTurn).TransitionTime(0)
				[
					Bool(CanTurn, true)
				],
				Transition().Source(Run).Destination(Idle).TransitionTime(0)
				[
					Float(Horizontal, 0.01f, ConditionMode.Less),
					Float(Horizontal, -0.01f, ConditionMode.Greater)
				],
				Transition().Source(RunTurn).Destination(Run).ExitTime(1).TransitionTime(0),
				Transition().SourceMultiple(Idle, Walk, Run).Destination(SimpleAttacks).TransitionTime(0)
				[
					Bool(IsGrounded, true),
					Bool(IsActing, true),
					Int(ActionIndex, 0, ConditionMode.Greater),
					Int(ActionIndex, 8, ConditionMode.Less)
				],
				Transition().SourceMultiple(Idle, Walk, Run).Destination(SpecialAttacks).TransitionTime(0)
				[
					Bool(IsGrounded, true),
					Bool(IsActing, true),
					Int(ActionIndex, 7, ConditionMode.Greater)
				],
				Transition().SourceMultiple(Idle, Walk, Run, RunTurn).Destination(InAir).TransitionTime(0)
				[
					Bool(IsGrounded, false)
				]
			];
		}

		protected virtual TransitionGroupContext ConstructAttacksTransitions()
		{
			return Transitions()
			[
				Transition().Entry().Destination(SimpleAttackTop)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 2, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SimpleAttackFront)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 3, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SimpleAttackBottom)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 4, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SimpleHeavyAttackTop)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 5, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SimpleHeavyAttackFront)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 6, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SimpleHeavyAttackBottom)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 7, ConditionMode.Equals)
				],

				Transition().Entry().Destination(SpecialAttackTop)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 9, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SpecialAttackFront)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 10, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SpecialAttackBottom)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 11, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SpecialHeavyAttackTop)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 12, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SpecialHeavyAttackFront)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 13, ConditionMode.Equals)
				],
				Transition().Entry().Destination(SpecialHeavyAttackBottom)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 14, ConditionMode.Equals)
				],
				Transition().Source(SimpleAttack).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SimpleAttackTop).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SimpleAttackFront).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SimpleAttackBottom).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SimpleHeavyAttackTop).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SimpleHeavyAttackFront).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SimpleHeavyAttackBottom).Destination(Root_Base).ExitTime(1).TransitionTime(0),
			
				Transition().Source(SpecialAttack).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SpecialAttackTop).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SpecialAttackFront).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SpecialAttackBottom).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SpecialHeavyAttackTop).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SpecialHeavyAttackFront).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Source(SpecialHeavyAttackBottom).Destination(Root_Base).ExitTime(1).TransitionTime(0)
			
//			Transition().SourceMultiple
//			(
//				SimpleAttack,
//				SimpleAttackTop,
//				SimpleAttackFront,
//				SimpleAttackBottom,
//				SimpleHeavyAttackTop,
//				SimpleHeavyAttackFront,
//				SimpleHeavyAttackBottom,
//				SpecialAttack,
//				SpecialAttackTop,
//				SpecialAttackFront,
//				SpecialAttackBottom,
//				SpecialHeavyAttackTop,
//				SpecialHeavyAttackFront,
//				SpecialHeavyAttackBottom
//			).Destination(Root_Base).ExitTime(1).TransitionTime(0)
			];
		}

		protected virtual TransitionGroupContext ConstructJumpsTransitions()
		{
			return Transitions()
			[
				Transition().Source(State.Jump).Destination(DoubleJump).TransitionTime(0)
				[
					Int(JumpCounter, 2, ConditionMode.Equals)
				],
				Transition().SourceMultiple(State.Jump, DoubleJump).Destination(JumpEnd).TransitionTime(0)
				[
					Bool(IsGrounded, true)
				],
				Transition().SourceMultiple(State.Jump, DoubleJump).Destination(AirAttacks).TransitionTime(0)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 14, ConditionMode.Greater)
				],
				Transition().Source(JumpEnd).Destination(Root_Base).ExitTime(1).TransitionTime(0),
				Transition().Entry().Destination(InAir)
				[
					Bool(IsGrounded, false)
				]
			];
		}

		protected virtual TransitionGroupContext ConstructAirAttacksTransitions()
		{
			return Transitions()
			[
				Transition().Entry().Destination(InAir)
				[
					Bool(IsGrounded, false)
				],
				Transition().Entry().Destination(AirAttackTop)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 16, ConditionMode.Equals)
				],
				Transition().Entry().Destination(AirAttackFront)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 17, ConditionMode.Equals)
				],
				Transition().Entry().Destination(AirAttackBottom)
				[
					Bool(IsActing, true),
					Int(ActionIndex, 18, ConditionMode.Equals)
				],
				Transition().SourceMultiple(AirAttack, AirAttackTop, AirAttackFront, AirAttackBottom).Destination(InAir)
					.ExitTime(1).TransitionTime(0)
					[
						Bool(IsGrounded, true)
					]
			];
		}

		protected virtual TransitionGroupContext ConstructHurtTransitions()
		{
			return Transitions()
			[
				Transition().Any().Destination(Hurts).TransitionTime(0)
				[
					Trigger(TakeDamage)
				],
				Transition().Source(Hit).Destination(HurtFalling).TransitionTime(0)
				[
					Bool(IsTakingKnockback, true)
				],
				Transition().Source(Hit).Exit().ExitTime(1).TransitionTime(0)
				[
					Bool(IsTakingKnockback, false)
				],
				Transition().Source(HurtFalling).Destination(State.Hurt).ExitTime(1).TransitionTime(0)
				[
					Bool(IsTakingKnockback, false),
					Bool(IsGrounded, true)
				],
				Transition().Source(State.Hurt).Destination(GetUp).TransitionTime(0)
				[
					Trigger(IsGettingUp)
				],
				Transition().Source(GetUp).ExitTime(1).Exit().TransitionTime(0)
			];
		}

		/*----------------------------------------------------------------------------------------*
	     * Values
	     *----------------------------------------------------------------------------------------*/

		public new enum Layer
		{
			Base
		}

		public new enum StateMachine
		{
			Root_Base,
			Gait,
			SimpleAttacks,
			SpecialAttacks,
			InAir,
			Jumps,
			AirAttacks,
			Hurts
		}

		public new enum State
		{
			Idle,
			Walk,
			Run,
			RunTurn,

			SimpleAttack,
			SimpleAttackTop,
			SimpleAttackFront,
			SimpleAttackBottom,
			SimpleHeavyAttackTop,
			SimpleHeavyAttackFront,
			SimpleHeavyAttackBottom,

			SpecialAttack,
			SpecialAttackTop,
			SpecialAttackFront,
			SpecialAttackBottom,
			SpecialHeavyAttackTop,
			SpecialHeavyAttackFront,
			SpecialHeavyAttackBottom,

			Jump,
			DoubleJump,
			JumpEnd,

			AirAttack,
			AirAttackTop,
			AirAttackFront,
			AirAttackBottom,

			Hit,
			HurtFalling,
			Hurt,
			GetUp
		}

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

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
