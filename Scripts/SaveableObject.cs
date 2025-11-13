using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    [HideInInspector] public string ObjectID;

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
