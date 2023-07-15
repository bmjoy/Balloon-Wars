using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class LocationServiceActivator : MonoBehaviour
{
    public static IEnumerator ActivateLocationServices()
    {
        Debug.Log("Activating location services");

        // Request permission to use location
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(5);

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            double latitude = Input.location.lastData.latitude;
            double longitude = Input.location.lastData.longitude;
            Debug.Log("Latitude: " + latitude + ", Longitude: " + longitude);
        }
    }
}
