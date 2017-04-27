using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BossScene1 : Cutscene {
    
    public override void stepCutscene(int response) {
        base.stepCutscene(response);

        if (EventSystem.current.currentSelectedGameObject != null)
            conversation.setDialogue(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text, true);

        switch (stage) {
            case 0:
                conversation.setPlayerImage(sprites[0]);
                conversation.show();
                conversation.setDialogue("If I keep getting caught where I'm not supposed to be, eventually my boss will notice and, I might be kicked off the case.",
                    true);
                resp1 = "Then I should keep to the shadows, where no one will ever see me";
                func.Add(updateResp1);

                resp2 = "Then I had best makesure now one speaks up about my activities.";
                func.Add(updateResp2);
                setResp2Icon(-1);
                break;

            case 1:
                if (response == 0) {
                }
                else if (response == 1) {
                    manager.addInfamy(3);
                    crooked = true;
                }

                conversation.hide();
                walk(-1, 1.1f);
                break;

            case 2:
                loadLevel(crooked);
                break;

            default:
                break;
        }
    }

    private void loadLevel(bool crooked) {
        manager.next(crooked);
    }
}
