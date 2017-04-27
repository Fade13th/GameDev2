using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuardSpot : Cutscene{
    public override void stepCutscene(int response) {
        base.stepCutscene(response);

        if (EventSystem.current.currentSelectedGameObject != null)
            conversation.setDialogue(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text, true);

        switch (stage) {
            case 0:
                conversation.setPlayerImage(sprites[0]);
                conversation.setOtherImage(sprites[2]);
                conversation.show();
                conversation.setDialogue("Hey, what are you doing here?", false);
                resp1 = "Run";
                func.Add(updateResp1);
                resp2 = "Look, a plane!";
                func.Add(updateResp2);
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
