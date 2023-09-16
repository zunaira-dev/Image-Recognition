using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

[Serializable]
public class ModerationResponse
{
    public bool IsImageAdultClassified;
    public bool IsImageRacyClassified;
    
}

public class ImageModeration : MonoBehaviour
{
    private string apiKey = "68b2eb9cab814a4d8e2eda0bb880b84f"; 
    private string endpoint = "https://faseh.cognitiveservices.azure.com/contentmoderator/moderate/v1.0/ProcessImage/Evaluate";

    public string imageUrl = "https://cdn.discordapp.com/attachments/946491728326709308/1150701800748498944/nintchdbpict000393240913.jpg";

    private void Start()
    {
        StartCoroutine(ModerateImage());
    }

    private IEnumerator ModerateImage()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "Ocp-Apim-Subscription-Key", apiKey },
            { "Content-Type", "application/json" }
        };

        string requestData = "{ \"DataRepresentation\": \"URL\", \"Value\": \"" + imageUrl + "\" }";

        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
        UploadHandlerRaw uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(requestData));
        request.uploadHandler = uploadHandler;
        request.downloadHandler = new DownloadHandlerBuffer();

        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responseJson = request.downloadHandler.text;
            //Debug.Log("Moderation Response: " + responseJson);

            
            ModerationResponse moderationResponse = JsonUtility.FromJson<ModerationResponse>(responseJson);

            
            bool isImageAdultClassified = moderationResponse.IsImageAdultClassified;
            bool isImageRacyClassified = moderationResponse.IsImageRacyClassified;


            if (isImageAdultClassified || isImageRacyClassified)
            {
                Debug.Log("Inappropriate content found!");
            }
            else
                Debug.Log("No Inappropriate content found");
        }
    }
}
