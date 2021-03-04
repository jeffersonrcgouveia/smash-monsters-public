using AvoEx.ObservableType;

namespace SmashMonsters.Player.Input
{
	public interface CharacterMovementInput
	{
		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/
    
		public ObFloat Horizontal { get; }
    
		public ObFloat Vertical { get; }
    
		public ObFloat LastHorizontal { get; }
    
		public ObFloat LastVertical { get; }
    
		public ObFloat HorizontalRaw { get; }
    
		public ObFloat VerticalRaw { get; }

		public ObBool IsJumping { get; }

		public ObBool IsWalking { get; }

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/
    
	}
}
