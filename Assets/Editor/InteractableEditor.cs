
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
{
   public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(Interactable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can only use unity Events.", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.UseEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();       
            }
        }
        else
        {
           
            base.OnInspectorGUI();
            if (interactable.UseEvents)
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();
            }
            else
            {
                if (interactable.GetComponent<InteractionEvent>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());

            }
        }
       
    }
}
