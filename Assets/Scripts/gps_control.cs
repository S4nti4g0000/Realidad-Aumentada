using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gps_control : MonoBehaviour
{
    public bool Initialized { get; private set; }
    public float Latitude { get; private set; }
    public float Longitude { get; private set; }
    public float Altitude { get; private set; }

    private float targetLat = 4.6587561f;
    private float targetLong = -74.0578893f;
    public float distance;

    public TMP_Text text_;
    public TMP_Text status;
    public TMP_Text conf;
    public Rigidbody sphere_;

    public GameObject imgTarget;

    void Awake()
    {
       StartCoroutine(StartLocationServiceAsync());
    }

    private void Start()
    {
        imgTarget.SetActive(false);
    }

    float FormulaHaversine(float lat1, float long1, float lat2, float long2)
    {
        float earthRad = 6371000;
        float lRad1 = lat1 * Mathf.Deg2Rad;
        float lRad2 = lat2 * Mathf.Deg2Rad;
        float dLat = (lat2 - lat1) * Mathf.Deg2Rad;
        float dLong = (long2 - long1) * Mathf.Deg2Rad;
        float a = Mathf.Sin(dLat / 2.0f) * Mathf.Sin(dLat / 2.0f) +
                  Mathf.Cos(lRad1) * Mathf.Cos(lRad2) *
                  Mathf.Sin(dLong / 2.0f) * Mathf.Sin(dLong / 2.0f);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return earthRad * c; //en metros
    }

    void Update()
    {
        if (!Initialized)
            return;

        status.text = "Status: " + Input.location.status;
        var lastLoc_ = Input.location.lastData;
        
        Latitude = lastLoc_.latitude;
        Longitude = lastLoc_.longitude;
        Altitude = lastLoc_.altitude;

        text_.text = "Latitud: " + Latitude.ToString() + "\nLongitud: " + Longitude.ToString() + "\nAlt: " + Altitude.ToString();

        distance = FormulaHaversine(Latitude, Longitude,  targetLat, targetLong);

        if(distance == 25)
        {
            imgTarget.SetActive(true);
            conf.text = " Encontraste el oxxo! ";

        }else { conf.text = "Distance: " + distance.ToString(); }
    }

    public void StartLocationService()
    {
        StartCoroutine(StartLocationServiceAsync());
    }

    IEnumerator StartLocationServiceAsync()
    {

        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location access not enabled by user.");
            yield break;
        }

        Input.location.Start(5, 1);

        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);

        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            Initialized = true;

            var lastLocationData = Input.location.lastData;
        }
    }

    public void activatePhysics()
    {
        sphere_.useGravity = true;

    }


}
