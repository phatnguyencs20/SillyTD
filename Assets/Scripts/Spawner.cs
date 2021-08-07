using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [Header("Specifications")]
    [SerializeField]
    float timeBetweenWaves;
    [SerializeField]
    float timeBetweenMobs;
    [SerializeField]
    float timeBetweenGroups;
    [SerializeField]
    int mobsPerWave;
    [SerializeField]
    int mobsPerGroup;

    [Header("Unity Objects")]
    [SerializeField]
    GameObject[] mobWaves;
    [SerializeField]
    TMP_Text waveCountdownText;

    int nextMobWaves;

    Spawner()
    {
        nextMobWaves = 0;
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
                yield return new WaitForSeconds(timeBetweenMobs);

                if (tempMobsPerWave % mobsPerGroup == 0)
                {
                    yield return new WaitForSeconds(timeBetweenGroups);
                }

                Instantiate(mobWaves[tempNextMobWaves], transform);
                tempMobsPerWave--;
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
