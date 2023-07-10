using System;
using UnityEngine;
using System.Collections.Generic;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance {get; private set;}
    public Dictionary<int, Vector2> PlayerLocations {get; set;}
    [SerializeField] private double m_DistanceThreshold = 5;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        PlayerLocations = new Dictionary<int, Vector2>();
    }

    public void UpdateGPSLocation(int playerId, Vector2 gpsLocation)
    {
        /* update GPS location for player with ID 'playerId' */
        if (PlayerLocations.ContainsKey(playerId))
        {
            PlayerLocations[playerId] = gpsLocation;
        }
        else
        {
            PlayerLocations.Add(playerId, gpsLocation);
        }

        CheckAllDistancesFromPlayer(playerId, gpsLocation);
    }

    private void CheckAllDistancesFromPlayer(int playerId, Vector2 gpsLocation)
    {
        /* check distance from player with ID 'playerId' to all other players */
        foreach (var player in PlayerLocations)
        {
            if (player.Key != playerId)
            {
                var distance = Distance(gpsLocation.x, gpsLocation.y, player.Value.x, player.Value.y);
                Debug.Log($"Distance from player {playerId} to player {player.Key} is {distance} meters");
                if (distance < m_DistanceThreshold)
                {
                    Debug.Log($"Player {playerId} is close to player {player.Key}");
                    // Enable nerf ability between the two players
                }
            }
        }
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