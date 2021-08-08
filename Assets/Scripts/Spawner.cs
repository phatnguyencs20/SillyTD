using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [Header("Specifications")]
    [SerializeField]
    int mobsPerWave;
    [SerializeField]
    float timeBetweenWaves;
    [SerializeField]
    int mobsPerGroup;
    [SerializeField]
    float timeBetweenGroups;
    [SerializeField]
    float timeBetweenMobs;

    [Header("Unity Objects")]
    [SerializeField]
    GameObject[] mobWaves;

    TMP_Text waveCountdownText;
    int nextMobWaves;

    void Awake()
    {
        nextMobWaves = 0;
        waveCountdownText = GameObject.Find("WaveCountdownText").GetComponent<TMP_Text>();
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
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
