using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public Material skyboxMaterialDay;
    public Material skyboxMaterialNight;

    public Color dayTintColour;
    public Color nightTintColour;
    public float morningTime;
    public float nightTime;
    public float startHour = 0;
    public float hour = 0;
    public float DayLength;
    private float _rotationSpeed;
    float add;

    public Color dayFogColour;
    public Color eveningFogColour;
    public Color nightFogColour;

    public Light sun;

    bool daySkyboxSet = false;
    bool nightSkyboxSet = false;
    bool dayTime;

    // Start is called before the first frame update
    void Start()
    {
        //RenderSettings.skybox = skyboxMaterialDay;
        setSkyBox();

        transform.localRotation = new Quaternion(0.0917145982f, -0.845065713f, -0.44573009f, -0.280672699f);

        hour = startHour;
        float initialRotation = startHour * 360/24;
        transform.Rotate(0, initialRotation, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _rotationSpeed = Time.deltaTime / DayLength;
        transform.Rotate (0, 360 * _rotationSpeed, 0);

        hour += 24 * _rotationSpeed;
        hour = hour % 24;

        float closenessToPeak = Vector3.Dot(transform.TransformDirection(-Vector3.forward), Vector3.up); // 1 at peak of midday, 0 at evening, and -1 at midnight
        sun.intensity = Mathf.Max(closenessToPeak, 0.2f);

        float tintLerpAmount = 1 - closenessToPeak; // 0 at midday, 1 at evening
        skyboxMaterialDay.SetVector("_Tint", Color.Lerp(dayTintColour, nightTintColour, tintLerpAmount));

        setSkyBox();

        if (dayTime)
            RenderSettings.fogColor = Color.Lerp(eveningFogColour, dayFogColour, closenessToPeak);
        else
            RenderSettings.fogColor = Color.Lerp(eveningFogColour, nightFogColour, -closenessToPeak);
    }

    void setSkyBox()
    {
        if (hour >= morningTime && daySkyboxSet == false)
        {
            daySkyboxSet = true;
            nightSkyboxSet = false;
            dayTime = true;
            RenderSettings.skybox = skyboxMaterialDay;
        }
        if ((hour <= morningTime || hour >= nightTime) && nightSkyboxSet == false)
        {
            nightSkyboxSet = true;
            daySkyboxSet = false;
            dayTime = false;
            RenderSettings.skybox = skyboxMaterialNight;
        }
    }
}