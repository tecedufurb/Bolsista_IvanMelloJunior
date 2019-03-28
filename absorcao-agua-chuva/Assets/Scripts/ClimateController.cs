using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClimateController : MonoBehaviour{
    
    [SerializeField] private GameObject rain;
    [SerializeField] private Transform river;
    [SerializeField] private Text rainfallIndexText;
    [SerializeField] private Text riverHeightText;
    [SerializeField] private Terrain terrain;
    private float minPosition;
    private float maxPosition;
    private float speed = 2f;
    private float rainfallIndex = 0f;
    private float range = 40f;

    private void Start() {
        minPosition = river.position.y;
        maxPosition = river.position.y + range;

        TerrainUtils terrainUtils = new TerrainUtils(terrain.terrainData);
        terrainUtils.SetTerrainTexture(MenuManager.terrainType);
    }

    public void StartRain() {
        StartCoroutine(MoveRiverUp());
    }
    
    public void StartDry() {
        StartCoroutine(MoveRiverDown());
    }

    public void StopRain() {
        StopAllCoroutines();
        rain.SetActive(false);
    }

    private IEnumerator MoveRiverUp () {
        rain.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        while (river.position.y < maxPosition) {
            river.position = new Vector3(river.position.x, 
                river.position.y + Time.deltaTime * speed, river.position.z);

            rainfallIndex += .5f;
            riverHeightText.text = river.position.y.ToString();
            rainfallIndexText.text = rainfallIndex.ToString();

            yield return null;
        }
        rain.SetActive(false);
    }

    private IEnumerator MoveRiverDown() {
        rain.SetActive(false);
        yield return new WaitForSeconds(.5f);
        while (river.position.y > minPosition) {
            river.position = new Vector3(river.position.x, 
                river.position.y - Time.deltaTime * speed, river.position.z);

            riverHeightText.text = river.position.y.ToString();
            yield return null;
        }
    }
}