using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2 : Level {
    public bool crooked = false;

    protected override void updateObj() {
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

            default:
                break;
        }
    }
}
