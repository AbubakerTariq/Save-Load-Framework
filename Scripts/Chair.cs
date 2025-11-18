using UnityEngine;

public class Chair : SaveableObject
{
    private class Data
    {
        public bool canSit;
        public Vector3 position;
    }

    private void Awake()
    {
        // Restore the state when the object is instantiated
        var data = (Data)SaveManager.Instance.RestoreObjectState(ObjectID);

        if (data != null)
        {
            // use the canSit property here
            transform.position = data.position;
        }
    }

    public override void CaptureObjectState()
    {
        // Save the state when the object is destroyed
        var data = new Data
        {
            canSit = true,
            position = transform.position
        };

        SaveManager.Instance.CaptureObjectState(ObjectID, data);
    }
}