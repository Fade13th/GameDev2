using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2 : Level {

    public override void updateObj() {
        base.updateObj();
        switch (stage) {
            case 0:
                if (!crooked)
                    objectiveText.text = "Objective: Locate documents relating to the location of the truck";
                else
                    objectiveText.text = "Objective: Place heroin in a secure location to frame the owner";
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
        manager.addExperience(15);

        if (crooked) {
            manager.addInfamy(15);
        }
        else {
            manager.addReputation(15);
        }

        LevelManager.GetLevelManager().levelComplete();
    }
}
