using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaserSpot : Cutscene {
    public override void stepCutscene(int response) {
        base.stepCutscene(response);

        if (EventSystem.current.currentSelectedGameObject != null)
            conversation.setDialogue(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text, true);

        switch (stage) {
            case 0:
                conversation.setPlayerImage(sprites[0]);
                conversation.show();
                conversation.setDialogue("Alarms start Wailing", true);
                resp1 = "Run";
                func.Add(updateResp1);
                break;

            case 1:
                loadLevel(crooked);
                break;

            default:
                break;
        }
    }

    private void loadLevel(bool crooked) {
        manager.resetLevelFromConv();
        Destroy(gameObject);
    }
}
