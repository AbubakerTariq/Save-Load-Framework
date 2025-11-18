using UnityEngine;

public class Player : SaveableObject
{
    private class Data
    {
        public float hp;
        public float remainingAmmo;
        public Vector3 position;
    }

    private void Awake()
    {
        // Restore the state when the object is instantiated
        var data = (Data)SaveManager.Instance.RestoreObjectState(ObjectID);

        if (data != null)
        {
            // use the hp and ammo property here
            transform.position = data.position;
        }
    }

    public override void CaptureObjectState()
    {
        // Save the state when the object is destroyed
        var data = new Data
        {
            hp = Random.Range(1, 100),
            remainingAmmo = Random.Range(1, 100),
            position = transform.position,
        };

        SaveManager.Instance.CaptureObjectState(ObjectID, data);
    }
}