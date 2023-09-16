using System.IO;
using UnityEngine;

public class OpenCam : MonoBehaviour
{
    // Start is called before the first frame update
    private WebCamTexture webCamTexture;
    private bool isCapturing = false;

    // Start is called before the first frame update
    void Start() {
        // Start the camera feed
        webCamTexture = new WebCamTexture();
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update() {
        // Check for input to capture a photo
        if (Input.GetKeyDown(KeyCode.Space) && !isCapturing) {
            isCapturing = true;
            CapturePhoto();
        }
    }

    void CapturePhoto() {
        // Capture a frame from the camera
        Texture2D photoTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
        photoTexture.SetPixels(webCamTexture.GetPixels());
        photoTexture.Apply();

        // Save the photo to the device's storage
        byte[] photoBytes = photoTexture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/capturedPhoto.png", photoBytes);

        // Import the photo as a Texture or Sprite
        // Here, you can use photoTexture as a material's mainTexture
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = photoTexture;

        isCapturing = false;
    }
}

