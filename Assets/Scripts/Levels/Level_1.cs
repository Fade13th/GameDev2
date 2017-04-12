using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1 : Level {
    public override void updateObj() {
        base.updateObj();

        switch (stage) {
            case 0:
                if (!crooked)
                    objectiveText.text = "Objective: Retreive camera footage from the CCTV computer";
                else
                    objectiveText.text = "Objective: Plant incriminating files on the CCTV computer";
                break;

            case 1:
                objectiveText.text = "Objective: Exit through the window you entered";
                break;

            case 2:
                nextLevel();
                break;

            default:
                break;
        }
    }

    private void nextLevel() {
        manager.addExperience(10);

        if (crooked) {
            manager.addInfamy(10);
        }
        else {
            manager.addReputation(10);
        }

        LevelManager.GetLevelManager().levelComplete();
    }
}
