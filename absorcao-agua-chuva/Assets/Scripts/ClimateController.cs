using System.Collections;
using UnityEngine;

public class ClimateController : MonoBehaviour{
    
    [SerializeField] private GameObject rain;
    [SerializeField] private Transform river;
    private float minPosition;
    private float maxPosition;
    private float speed = 2f;
    [SerializeField] private float range = 30f;
    
    private void Start() {
        minPosition = river.position.y;
        maxPosition = river.position.y + range;
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
            yield return null;
        }
    }
}