using System;
using System.Collections;
using DigitalRuby.RainMaker;
using UnityEngine;
using UnityEngine.UI;

public class ClimateController : MonoBehaviour{

    [SerializeField] private GameObject rain;
    [SerializeField] private Transform river;
    [SerializeField] private Text rainfallIndexText;
    [SerializeField] private Text riverHeightText;
    [SerializeField] private Terrain terrain;

    [SerializeField] private RainScript rainScript;

    private Color32 inactiveColor = new Color32(130, 130, 130, 255);

    [SerializeField] private GameObject[] rainButtons;
    [SerializeField] private GameObject[] rainButtonsInactive;

    [SerializeField] private GameObject sunButton;
    [SerializeField] private GameObject sunButtonInactive;

    private int rainLevel;
    private float minPosition;
    private float maxPosition;
    private float speed = 1.0f;
    private float rainfallIndex = 0.0f;
    private float range = 40.0f;

    private float riverHeightCountMax = 13.0f;
    private float rainfallIndexCountMax = 350.0f;
    private float rainfallInc = 0.5f;
    private float rainfallSpeed = 30f;

    private void Start() {

        switch(MenuManager.terrainType) {
            case TerrainType.PERMEAVEL:
                range = 20;
                riverHeightCountMax = 4.0f;
                break;
            case TerrainType.SEMI_PERMEAVEL:
                range = 35;
                riverHeightCountMax = 8.0f;
                break;
            case TerrainType.IMPERMEAVEL:
                range = 50;
                riverHeightCountMax = 16.0f;
                break;
        }

        minPosition = river.position.y;
        maxPosition = river.position.y + range;

        TerrainUtils terrainUtils = new TerrainUtils(terrain.terrainData);
        terrainUtils.SetTerrainTexture(MenuManager.terrainType);
    }

    public void StartRain(int rainIntensity) {
        StopAllCoroutines();
        rainLevel = rainIntensity;

        switch(rainLevel) {
            case 0:
                speed = 1.0f;
                rainfallInc = 0.5f;
                rainScript.RainIntensity = .3f;
                break;
            case 1:
                speed = 3.0f;
                rainfallInc = 1.5f;
                rainScript.RainIntensity = .6f;
                break;
            case 2:
                speed = 5.0f;
                rainfallInc = 3.0f;
                rainScript.RainIntensity = 1f;
                break;
        }

        StartCoroutine(MoveRiverUp());
    }
    
    public void StartDry() {
        StopAllCoroutines();
        StartCoroutine(MoveRiverDown());
    }

    public void StopRain() {
        StopAllCoroutines();
        rainScript.RainIntensity = 0f;
    }

    private IEnumerator MoveRiverUp () {
        rain.SetActive(true);

        sunButton.SetActive(true);
        sunButtonInactive.SetActive(false);

        if(river.position.y < maxPosition)
            rain.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        while (river.position.y < maxPosition) {
            river.position = new Vector3(river.position.x, 
                river.position.y + Time.deltaTime * speed, river.position.z);

            UpdateRiverHeightText();
            UpdateRainfallIndexText();

            yield return null;
        }
        rainScript.RainIntensity = 0f;
        DisableRainButtons(true);
        sunButton.SetActive(true);
        sunButtonInactive.SetActive(false);
    }

    private IEnumerator MoveRiverDown() {
        rain.SetActive(false);
        
        DisableRainButtons(false);

        yield return new WaitForSeconds(.5f);
        while (river.position.y > minPosition) {
            river.position = new Vector3(river.position.x, 
                river.position.y - Time.deltaTime * speed, river.position.z);

            UpdateRiverHeightText();
            yield return null;
        }
        DisableRainButtons(false);
        sunButton.SetActive(false);
        sunButtonInactive.SetActive(true);
    }

    private void DisableRainButtons(bool value) {
        foreach (GameObject button in rainButtons)
            button.SetActive(!value);
        foreach (GameObject inactive in rainButtonsInactive)
            inactive.SetActive(value);
    }

    private void UpdateRiverHeightText() {
        float riverHeight = (river.position.y * riverHeightCountMax) / range;
        riverHeightText.text = String.Format("{0:0.0}", riverHeight+1) + "m";
    }

    private void UpdateRainfallIndexText() {
        rainfallIndex += Time.deltaTime * rainfallInc * rainfallSpeed;
        float rainfall = (river.position.y * rainfallIndexCountMax) / range;
        rainfallIndexText.text = String.Format("{0:0.0}", rainfall) + "mm";
    }
}