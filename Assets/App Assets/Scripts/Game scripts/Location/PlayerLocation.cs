using System;
using UnityEngine;
using Photon.Pun;


public class PlayerLocation : MonoBehaviour
{
    private Vector2 m_LastGPSLocation;
    private PhotonView m_PhotonView;
    private LocationManager m_LocationManager;

    private void Awake() 
    {
        m_PhotonView = GetComponent<PhotonView>();
        m_LocationManager = LocationManager.Instance;
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
            {
                // send a GPC location update to the master client
                m_PhotonView.RPC("UpdateGPSLocationRPC",
                 PhotonNetwork.MasterClient, m_PhotonView.ViewID, currentGPSLocation);
                m_LastGPSLocation = currentGPSLocation;
            }
        }
    }

    [PunRPC]
    private void UpdateGPSLocationRPC(int playerId, Vector2 gpsLocation)
    {
        /* update GPS location for player with ID 'playerId' */
        m_LocationManager.UpdateGPSLocation(playerId, gpsLocation);
    }
}
