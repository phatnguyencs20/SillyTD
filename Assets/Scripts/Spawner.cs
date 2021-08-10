using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [Header("Specifications")]
    [SerializeField]
    private int mobsPerWave;
    [SerializeField]
    private float timeBetweenWaves;
    [SerializeField]
    private int mobsPerGroup;
    [SerializeField]
    private float timeBetweenGroups;
    [SerializeField]
    private float timeBetweenMobs;

    [Header("Unity Objects")]
    [SerializeField]
    private GameObject[] mobWaves;

    private TMP_Text waveCountdownText;
    private int nextMobWaves;

    private void Awake()
    {
        nextMobWaves = 0;
        waveCountdownText = GameObject.Find("WaveCountdownText").GetComponent<TMP_Text>();   
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int tempNextMobWaves = nextMobWaves;
        while (tempNextMobWaves < mobWaves.Length)
        {
            // Spawn mobs according to timing
            int tempMobsPerWave = mobsPerWave;
            while (tempMobsPerWave > 0)
            {
                Instantiate(mobWaves[tempNextMobWaves], transform.position, transform.rotation);
                tempMobsPerWave--;

                if (tempMobsPerWave % mobsPerGroup == 0)
                {
                    yield return new WaitForSeconds(timeBetweenGroups);
                } else
                {
                    yield return new WaitForSeconds(timeBetweenMobs);
                }
            }

            // Wait for the last mob to disappear
            while (FindObjectOfType<Mob>() != null)
            {
                yield return new WaitForSeconds(1f);
            }

            // End coroutine if the last wave ends
            tempNextMobWaves++;
            if (tempNextMobWaves == mobWaves.Length)
            {
                waveCountdownText.text = "END";
                yield break;
            }

            // Countdown next wave and show UI
            int countDown = Mathf.RoundToInt(timeBetweenWaves);
            while (countDown > 0)
            {
                waveCountdownText.text = countDown.ToString();
                yield return new WaitForSeconds(1f);
                countDown--;
            }
            waveCountdownText.text = "";  
        }
    }
}
