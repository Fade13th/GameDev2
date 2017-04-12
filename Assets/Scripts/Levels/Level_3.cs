using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3 : Level {

    public override void updateObj() {
        base.updateObj();
        switch (stage) {
            case 0:
                objectiveText.text = "Objective: Find the keys to the truck in the manager's office";
                break;

            case 1:
                objectiveText.text = "Objective: Search the truck for information about the troll";
                break;

            case 2:
                objectiveText.text = "Objective: Find an open window to escape through";
                break;

            case 3:
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
