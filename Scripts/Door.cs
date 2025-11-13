using UnityEngine;

public class Door : SaveableObject
{
    private class Data
    {
        public bool isOpen;
        public float rotationY;
    }

    private void Awake()
    {
        // Restore the state when the object is instantiated
        var data = (Data)SaveManager.RestoreObjectState(ObjectID);

        if (data != null)
        {
            transform.rotation = Quaternion.Euler(0, data.rotationY, 0);
        }
    }

    private void OnDestroy()
    {
        // Save the state when the object is destroyed
        var data = new Data
        {
            isOpen = true,
            rotationY = transform.rotation.eulerAngles.y
        };

        SaveManager.CaptureObjectState(ObjectID, data);
    }
}