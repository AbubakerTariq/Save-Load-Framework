using UnityEngine;

public class Chair : SaveableObject
{
    private class Data
    {
        public bool canSit;
        public Vector3 position;
        public Quaternion quaternion;
    }

    private void Awake()
    {
        // Restore the state when the object is instantiated
        var data = (Data)SaveManager.Instance.RestoreObjectState(ObjectID);

        if (data != null)
        {
            // use the canSit property here
            transform.SetPositionAndRotation(data.position, data.quaternion);
        }
    }

    public override void CaptureObjectState()
    {
        // Save the state when the object is destroyed
        var data = new Data
        {
            canSit = true,
            position = transform.position,
            quaternion = transform.rotation,
        };

        SaveManager.Instance.CaptureObjectState(ObjectID, data);
    }
}