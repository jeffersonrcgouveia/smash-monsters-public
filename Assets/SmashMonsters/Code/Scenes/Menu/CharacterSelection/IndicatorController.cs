using UnityEngine;
using UnityEngine.UI;

namespace SmashMonsters.Scenes.Menu.CharacterSelection
{
	public class IndicatorController : MonoBehaviour
	{	
		/*----------------------------------------------------------------------------------------*
	     * Constants
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/
    
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/
    
		[SerializeField]
		private Canvas canvas;

		[SerializeField]
		private Image playerCharacterImage;
    
		[SerializeField]
		private Texture2D cursorImage;
    
		[SerializeField]
		private Texture2D cursorGrabImage;
    
		[SerializeField]
		private Texture2D cursorPointerImage;

		[field:SerializeField]
		public GameState.GameState.Player Player { get; set; }

		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/

		private readonly Color _whiteColor = Color.white;

		private readonly Color _blackColor = Color.black;

		private bool _isGrabbed;

		public bool IsCharacterSelected { get; set; }

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

//    private void Start()
//    {
//        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
//    }
//
//    private void Update()
//	{
//		if (!_isGrabbed) return;
//		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
//			Input.mousePosition, canvas.worldCamera, out Vector2 pos);
//		transform.position = canvas.transform.TransformPoint(pos);
//	}
//
//    public void OnTriggerStay2D(Collider2D other)
//    {
//        Image image = other.GetComponent<Image>();
//        if (!image) return;
//        playerCharacterImage.sprite = image.sprite;
//        playerCharacterImage.color = _whiteColor;
//	}
//
//    private void OnTriggerExit2D(Collider2D other)
//    {
//        playerCharacterImage.sprite = null;
//        playerCharacterImage.color = _blackColor;
//    }
//
//    public void HandleIndicatorClicked()
//	{
//		_isGrabbed = !_isGrabbed;
//		Texture2D image = _isGrabbed ? cursorGrabImage : cursorImage;
//		Vector2 hotSpot = new Vector2(cursorGrabImage.width / 2, cursorGrabImage.height / 2);
//		Cursor.SetCursor(image, hotSpot, CursorMode.Auto);
//		if (!_isGrabbed && playerCharacterImage.sprite)
//		{
//			IsCharacterSelected = true;
//		}
//		else
//		{
//			IsCharacterSelected = false;
//		}
//	}

		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/

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
