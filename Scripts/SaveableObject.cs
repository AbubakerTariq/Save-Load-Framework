using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    [HideInInspector] public string ObjectID;

    public virtual void CaptureObjectState() { }
    private void Oestroy()
    {
        // Capture object state when the object is destroyed
        CaptureObjectState();        
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(ObjectID))
        {
            ObjectID = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}