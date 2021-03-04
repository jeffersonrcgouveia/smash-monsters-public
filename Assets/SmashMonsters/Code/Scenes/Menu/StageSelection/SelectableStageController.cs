using TopDownMedieval.Plugins.Commons.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SmashMonsters.Scenes.Menu.StageSelection
{
	public class SelectableStageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private Texture2D cursorImage;

		[SerializeField]
		private Image thumbnail;

		[SerializeField]
		private Text text;

		[SerializeField]
		private Image preview;

		[SerializeField]
		private Text previewText;
	
		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void OnEnable()
		{
			Vector2 hotSpot = new Vector2(cursorImage.width / 2, cursorImage.height / 2);
			Cursor.SetCursor(cursorImage, hotSpot, CursorMode.Auto);
		}

		private void OnDisable()
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			preview.sprite = thumbnail.sprite;
			preview.ToggleAlpha();
			previewText.text = text.text;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			preview.sprite = null;
			preview.ToggleAlpha();
			previewText.text = null;
		}
	}
}
