using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;
public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private Dialogue dialogue;
    private ResponseEvent[] responseEvents;
    private List<GameObject> tempResponses = new List<GameObject>();
    private void Start()
    {
        dialogue = GetComponent<Dialogue>();
    }
    
    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }


    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0f;

        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;


            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            //responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

            tempResponses.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        foreach(GameObject button in tempResponses)
        {
            Destroy(button);
        }
        tempResponses.Clear();

        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke(); 
        }

        responseEvents = null;

        if (response.DialogObject)
        {
            dialogue.ShowDialogue(response.DialogObject);
        }
        else
        {
            dialogue.CloseDialogueBox();
        }

        
    }
}
