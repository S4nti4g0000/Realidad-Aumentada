using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controladorgps : MonoBehaviour{

    public float puntoLat;
    public float puntoLong;
    public static float distancia;
    public bool Initialized;

    public TMP_Text text_;
    public TMP_Text status;
    public TMP_Text conf;

    /*float FormulaHaversine(float lat1, float long1, float lat2, float long2)
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
    }*/

    // Start is called before the first frame update
    void Awake()
    {
        StartLocation();
    }

    // Update is called once per frame
    void Update()
    {
        status.text = "Status: " + Input.location.status;
        var lastLocationData = Input.location.lastData;
        if (Input.location.status == LocationServiceStatus.Stopped)
        {
            StartLocation();
        }

        puntoLat = Input.location.lastData.latitude;
        puntoLong = Input.location.lastData.longitude;
        float altitude = Input.location.lastData.altitude;

        text_.text = "Latitud: " + puntoLat.ToString() + "\nLongitud: " + puntoLong.ToString() + "\nAlt: " + altitude.ToString();



        //distancia = FormulaHaversine(puntoLat, puntoLong, actualLat, actualLong);
    }

    /*private void OnGUI()
    {
        string mensaje = "Latitud : " + actualLat +
            "\nLongitud : " + actualLong +
            "\nDistancia :" + distancia;

        GUI.contentColor = Color.red;
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(100, 80, 1000, 1000), mensaje);
    }*/

    IEnumerator StartLocation()
    {
        // Start location services
        Input.location.Start(5,1);

        if(!Input.location.isEnabledByUser)
        {
            conf.text = "phone location off";
        }

        // Wait for location services to initialize
        while (Input.location.status == LocationServiceStatus.Initializing)
            yield return new WaitForSeconds(1f);

        if (Input.location.status == LocationServiceStatus.Running)
        {
            Initialized = true;

            var lastLocationData = Input.location.lastData;
        }

    }

    public void StartTheMotherfuckerNow()
    {
        Input.location.Start(5,1);

        puntoLat = Input.location.lastData.latitude;
        puntoLong = Input.location.lastData.longitude;
        float altitude = Input.location.lastData.altitude;

        text_.text = "Latitud: " + puntoLat.ToString() + "\nLongitud: " + puntoLong.ToString() + "\nAlt: " + altitude.ToString();

        conf.text = "pressed";
    }

}
