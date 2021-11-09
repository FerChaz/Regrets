using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/DialogueObject")] 

public class DialogObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses; 

    public string[] Dialogue => dialogue; 

    public Response[] Responses => responses;

    public bool HasResponses => Responses != null && Responses.Length > 0;
}
