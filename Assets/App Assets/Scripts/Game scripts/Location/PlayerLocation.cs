using System;
using UnityEngine;
using Photon.Pun;

public class PlayerLocation : MonoBehaviour
{
    private Vector2 m_LastGPSLocation;
    private PhotonView m_PhotonView;

    private void Awake() 
    {
        m_PhotonView = GetComponent<PhotonView>();    
    }

    private void Start()
    {
        InvokeRepeating("UpdateGPSLocation", 0, 1f);
    }

    private void UpdateGPSLocation()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            LocationInfo locationInfo = Input.location.lastData;
            Vector2 currentGPSLocation = new Vector2((float)locationInfo.latitude, (float)locationInfo.longitude);
            if (currentGPSLocation != m_LastGPSLocation)
            if (currentGPSLocation != m_LastGPSLocation)
            {
                UpdateGPSLocationRPC(m_PhotonView.ViewID, currentGPSLocation);
                m_LastGPSLocation = currentGPSLocation;
            }
        }
    }

    [PunRPC]
    private void UpdateGPSLocationRPC(int playerId, Vector2 gpsLocation)
    {
        /* update GPS location for player with ID 'playerId' */
    }

    public static double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371; // Earth's radius in kilometers
        var dLat = (lat2 - lat1) * Math.PI / 180;
        var dLon = (lon2 - lon1) * Math.PI / 180;
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                 Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = R * c;
        return distance * 1000; // Convert to meters
    }
}
