using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {
    public int[] expLevels;
    public string[] expRanks;

    public int[] repLevels;
    public string[] repRanks;

    public int[] infLevels;
    public string[] infRanks;

    public int strikePoint = 10, camCost = 2, guardCost = 3, laserCost = 10;

    private static Config _config;

    public void Start() {
        if (expLevels.Length != expRanks.Length || repLevels.Length != repRanks.Length || infLevels.Length != infRanks.Length) {
            throw new System.Exception("Level and Rank lists must be equal length in config");
        }

        _config = this;
    }

	public string getRank(int exp)  {
        string rank = "Rookie";

        for (int i = 0; i < expLevels.Length; i++) {
            if (exp >= expLevels[i]) {
                rank = expRanks[i];
            }
        }

        return rank;
    }

    public string getRep(int rep) {
        string rank = "Neutral";

        for (int i = 0; i < repLevels.Length; i++) {
            if (rep >= repLevels[i]) {
                rank = repRanks[i];
            }
        }

        return rank;
    }

    public string getInf(int inf) {
        string rank = "Above Board";

        for (int i = 0; i < infLevels.Length; i++) {
            if (inf >= infLevels[i]) {
                rank = infRanks[i];
            }
        }

        return rank;
    }

    public int getStrikes(int cam, int guard, int laser) {
        int score = (cam * camCost) + (guard * guardCost) + (laser * laserCost);

        return (int) Mathf.Floor(score / strikePoint);
    }

    public static Config getConfig() {
        return _config;
    }
}
