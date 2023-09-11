using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class ImageDropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RawImage imageDisplay; // UI element to display the dropped image (assign in the Inspector)
    private Texture2D droppedTexture; // To store the dropped image

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Highlight or provide feedback when a drag operation enters the UI element
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Remove any highlighting or feedback when a drag operation exits the UI element
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Check if an image file was dropped
        if (eventData.pointerDrag != null)
        {
            // Get the dropped object (should be a GameObject with an Image component)
            GameObject droppedObject = eventData.pointerDrag;

            // Check if the dropped object contains an Image component
            if (droppedObject != null)
            {
                // Get the Image component and its texture (the dropped image)
                Image imageComponent = droppedObject.GetComponent<Image>();

                if (imageComponent != null)
                {
                    droppedTexture = imageComponent.mainTexture as Texture2D;

                    if (droppedTexture != null)
                    {
                        // Display the dropped texture in the UI
                        imageDisplay.texture = droppedTexture;

                        // You can now process the dropped image (e.g., convert to base64 and send for moderation)
                        // Example: byte[] imageBytes = droppedTexture.EncodeToPNG();
                        //          string base64Image = System.Convert.ToBase64String(imageBytes);
                        //          SendImageForModeration(base64Image);
                    }
                }
            }
        }
    }

    // Implement additional functions for moderation, as needed
}
