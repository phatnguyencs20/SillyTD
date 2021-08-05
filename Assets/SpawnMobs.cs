using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnMobs : MonoBehaviour
{
    [SerializeField]
    GameObject[] mobWaves;

    [SerializeField]
    TMP_Text waveCountdownText;

    [SerializeField]
    float timeBetweenWaves = 5f;

    [SerializeField]
    float timeBetweenMobs = 0.75f;

    [SerializeField]
    float timeBetweenGroups = 0.75f;

    [SerializeField]
    int mobsPerWave = 12;

    [SerializeField]
    int mobsPerGroup = 4;

    int nextMobWaves;

    void Start()
    {
        nextMobWaves = 0;
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
