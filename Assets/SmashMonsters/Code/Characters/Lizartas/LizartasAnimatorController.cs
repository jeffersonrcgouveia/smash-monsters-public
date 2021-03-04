using System.Diagnostics.CodeAnalysis;
using SmashMonsters.Code.Characters.Base;
using SmashMonsters.Code.Characters.Base.Actions.Attack;
using SmashMonsters.Code.Characters.Base.Actions.Cooldown;
using SmashMonsters.Code.Characters.Base.Actions.Impulse;
using SmashMonsters.Code.Characters.Base.Plugins.CSharpAnimator;
using UnityEngine;
using static SmashMonsters.Code.Characters.Base.BaseAnimatorController.State;
using static SmashMonsters.Code.Characters.Base.BaseAnimatorController.StateMachine;

namespace SmashMonsters.Code.Characters.Lizartas
{
	[SuppressMessage("ReSharper", "HeapView.ObjectAllocation")]
	public class LizartasAnimatorController : BaseAnimatorController
	{
		/*----------------------------------------------------------------------------------------*
		 * Motions
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Parameters
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		protected override GraphContext ConstructGraph()
		{
			base.ConstructGraph();
			return Graph()
			[
				Layer(Layer.Base).DefaultState(Idle)
				[
					StateMachine(InAir)
					[
						StateMachine(AirAttacks)
						[
							#region AirAttacks
							State(AirAttack, AirAttackMotion)
								.Behaviour<CooldownBehaviour>(behaviour =>
								{
									behaviour.Cooldown = 0.1f;
								})
								.Behaviour<AttackBehaviour>(behaviour =>
								{
									behaviour.AttackInfo = new AttackInfo
									{
										Damage = 8.5f,
										KnockbackPower = 30,
										KnockbackScaling = 104,
										Direction = new Vector2(1, 0)
									};
								}),
							State(AirAttackTop, AirAttackTopMotion)
								.Behaviour<CooldownBehaviour>(behaviour =>
								{
									behaviour.Cooldown = 0.2f;
								})
								.Behaviour<AttackBehaviour>(behaviour =>
								{
									behaviour.AttackInfo = new AttackInfo
									{
										Damage = 5f,
										KnockbackPower = 50,
										KnockbackScaling = 113,
										Direction = new Vector2(0, 1)
									};
								}),
							State(AirAttackFront, AirAttackFrontMotion)
								.Behaviour<CooldownBehaviour>(behaviour =>
								{
									behaviour.Cooldown = 0.5f;
								})
								.Behaviour<AttackBehaviour>(behaviour =>
								{
									behaviour.AttackInfo = new AttackInfo
									{
										Damage = 3f,
										KnockbackPower = 50,
										KnockbackScaling = 150,
										Direction = new Vector2(1, 0)
									};
								}),
							State(AirAttackBottom, AirAttackBottomMotion)
								.Behaviour<CooldownBehaviour>(behaviour =>
								{
									behaviour.Cooldown = 0.2f;
								})
								.Behaviour<AttackBehaviour>(behaviour =>
								{
									behaviour.AttackInfo = new AttackInfo
									{
										Damage = 3f,
										KnockbackPower = 40,
										KnockbackScaling = 180,
										Direction = new Vector2(1, 1)
									};
								})
							#endregion
						]
					],
					StateMachine(SimpleAttacks)
					[
						#region SimpleAttacks
						State(SimpleAttack, SimpleAttackMotion)
							.Behaviour<CooldownBehaviour>(behaviour =>
							{
								behaviour.Cooldown = 0.1f;
							})
							.Behaviour<AttackBehaviour>(behaviour =>
							{
								behaviour.AttackInfo = new AttackInfo
								{
									Damage = 1.4f,
									KnockbackPower = 10,
									KnockbackScaling = 45,
									Direction = new Vector2(1, 0)
								};
							}),
						State(SimpleAttackTop, SimpleAttackTopMotion)
							.Behaviour<CooldownBehaviour>(behaviour =>
							{
								behaviour.Cooldown = 0.2f;
							})
							.Behaviour<AttackBehaviour>(behaviour =>
							{
								behaviour.AttackInfo = new AttackInfo
								{
									Damage = 5f,
									KnockbackPower = 40,
									KnockbackScaling = 120,
									Direction = new Vector2(0, 1)
								};
							}),
						State(SimpleAttackFront, SimpleAttackFrontMotion)
							.Behaviour<CooldownBehaviour>(behaviour =>
							{
								behaviour.Cooldown = 0.5f;
							})
							.Behaviour<AttackBehaviour>(behaviour =>
							{
								behaviour.AttackInfo = new AttackInfo
								{
									Damage = 10f,
									KnockbackPower = 70,
									KnockbackScaling = 90,
									Direction = new Vector2(1, 0)
								};
							})
							.Behaviour<ImpulseBehaviour>(behaviour =>
							{
								behaviour.ImpulseTime = 2.5f;
							}),
						State(SimpleAttackBottom, SimpleAttackBottomMotion)
							.Behaviour<CooldownBehaviour>(behaviour =>
							{
								behaviour.Cooldown = 0.2f;
							})
							.Behaviour<AttackBehaviour>(behaviour =>
							{
								behaviour.AttackInfo = new AttackInfo
								{
									Damage = 6f,
									KnockbackPower = 12,
									KnockbackScaling = 100,
									Direction = new Vector2(1, 1)
								};
							}),
						#endregion
					
						#region SimpleHeavyAttacks
						State(SimpleHeavyAttackTop, SimpleHeavyAttackTopMotion)
							.Behaviour<CooldownBehaviour>(behaviour =>
							{
								behaviour.Cooldown = 1f;
							})
							.Behaviour<AttackBehaviour>(behaviour =>
							{
								behaviour.AttackInfo = new AttackInfo
								{
									Damage = 11f,
									KnockbackPower = 30,
									KnockbackScaling = 90,
									Direction = new Vector2(0, 1)
								};
							}),
						State(SimpleHeavyAttackFront, SimpleHeavyAttackFrontMotion)
							.Behaviour<CooldownBehaviour>(behaviour =>
							{
								behaviour.Cooldown = 1f;
							})
							.Behaviour<AttackBehaviour>(behaviour =>
							{
								behaviour.AttackInfo = new AttackInfo
								{
									Damage = 18f,
									KnockbackPower = 60,
									KnockbackScaling = 75,
									Direction = new Vector2(1, 0)
								};
							}),
						State(SimpleHeavyAttackBottom, SimpleHeavyAttackBottomMotion)
							.Behaviour<CooldownBehaviour>(behaviour =>
							{
								behaviour.Cooldown = 1f;
							})
							.Behaviour<AttackBehaviour>(behaviour =>
							{
								behaviour.AttackInfo = new AttackInfo
								{
									Damage = 11f,
									KnockbackPower = 55,
									KnockbackScaling = 190,
									Direction = new Vector2(1, 1)
								};
							})
						#endregion
					]
				]
			];
		}
		//
		// protected override TransitionGroupContext ConstructAttacksTransitions()
		// {
		// 	base.ConstructAttacksTransitions();
		// 	
		// 	return Transitions()
		// 	[
		// 		Transition().Source(SpecialHeavyAttackTop).Destination(Root_Base).Find(0)
		// 		[
		// 			Trigger(StopAction)
		// 		],
		// 		Transition().Source(SpecialHeavyAttackBottom).Destination(Root_Base).Find(0)
		// 		[
		// 			Trigger(StopAction)
		// 		]
		// 	];
		// }
		//
		// protected override TransitionGroupContext ConstructJumpsTransitions()
		// {
		// 	base.ConstructJumpsTransitions();
		// 	return Transitions();
		// }
		//
		// protected override TransitionGroupContext ConstructAirAttacksTransitions()
		// {
		// 	base.ConstructAirAttacksTransitions();
		// 	return Transitions();
		// }

		/*----------------------------------------------------------------------------------------*
	     * Values
	     *----------------------------------------------------------------------------------------*/

	}
}
