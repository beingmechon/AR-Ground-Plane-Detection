using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.Android;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public static Location Instance { set; get; }

    public float lati;
    public float longi;

    public Text statusText;
    //public Text temp;
    //public Text pressure;
    //public Text humidity;
    //public Text windspeed;
    public Text para;
    //public Text latlon;

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }
    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enabled GPS");
            yield break;
        }
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            lati = Input.location.lastData.latitude;
            longi = Input.location.lastData.longitude;

            yield break;
        }
    }

    private void UpdateWeatherData()
    {
        StartCoroutine(FetchWeatherDataFromApi(lati.ToString(), longi.ToString()));
    }
    private IEnumerator FetchWeatherDataFromApi(string latitude, string longitude)
    {
        string url = "https://api.openweathermap.org/data/2.5/onecall?lat=" + latitude + "&lon=" + longitude + "&appid=aecdcc8eba118cc9977a7643c922f9b0&units=metric";
        //string url = "https://api.openweathermap.org/data/2.5/onecall?lat={" + latitude + "}&lon={" + longitude + "}&appid={aecdcc8eba118cc9977a7643c922f9b0}";
        //string url = "https://api.openweathermap.org/data/2.5/onecall?lat=12.67676&lon=79.28838&appid=aecdcc8eba118cc9977a7643c922f9b0&units=metric"

        UnityWebRequest fetchWeatherRequest = UnityWebRequest.Get(url);
        yield return fetchWeatherRequest.SendWebRequest();
        if (fetchWeatherRequest.isNetworkError || fetchWeatherRequest.isHttpError)
        {
            //Check and print error 
            statusText.text = fetchWeatherRequest.error;
        }
        else
        {
            Debug.Log(fetchWeatherRequest.downloadHandler.text);
            var response = JSON.Parse(fetchWeatherRequest.downloadHandler.text);

            //temp.text = response["current"]["temp"] + " C";
            //pressure.text = "Pressure is " + response["current"]["pressure"] + " Pa";
            //humidity.text = response["current"]["humidity"] + " % Humidity";
            //windspeed.text = "Windspeed is " + response["current"]["wind_speed"] + " Km/h";
            //para.text = "The temperature in your place is " + response["current"]["temp"] + " C. @Humidity is " + response["current"]["humidity"] + "%. @And it feels likes " + response["current"]["feels_like"];
            para.text = "The temperature in your place is " + response["current"]["temp"] + " C. @Humidity is " + response["current"]["humidity"] + "%.";

            //latlon.text = "Lat: " + response["lat"] + " Lon: " + response["lon"];


        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateWeatherData();
    }
}
