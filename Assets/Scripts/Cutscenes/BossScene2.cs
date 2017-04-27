using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BossScene2 : Cutscene {
    
    public override void stepCutscene(int response) {
        base.stepCutscene(response);

        if (EventSystem.current.currentSelectedGameObject != null)
            conversation.setDialogue(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text, true);

        switch (stage) {
            case 0:
                conversation.hide();
                walk(1, 1.1f);
                break;
            case 1:
                conversation.setPlayerImage(sprites[0]);
                conversation.setOtherImage(sprites[1]);

                conversation.setOtherName("Sargent\nG. Billy\n(Your Boss)");

                conversation.show();
                conversation.setDialogue(
                    "So, I've been getting reports of you snooping around without a warrent, Got anything to say for yourself?",
                    false);

                resp1 = "It is vital to the troll case, and the warrent always takes to long!";
                func.Add(updateResp1);

                resp2 = "No.";
                func.Add(updateResp2);
                setResp2Icon(1);
                break;

            case 2:
                if (response == 0) {
                    conversation.setDialogue("I don't care how long the warrent takes, you can't be seen breaking into other people's property with no good reason!", false);
                }
                else if (response == 1) {
                    conversation.setDialogue("Umphh, at least you're honest", false);
                    manager.addReputation(3);
                }

                resp1 = "Am I still on the case?";
                func.Add(updateResp1);

                break;

            case 3:
                conversation.setDialogue("Barely, but if I get another phone call saying that you've been caught on camera," +
                                         "found by another security guard or been profiled by a laser, you won't be. Am I clear?  ", false);

                resp1 = "Absolutely";
                func.Add(updateResp1);

                resp2 = "Crystal";
                func.Add(updateResp2);

                resp3 = "As mud";
                func.Add(updateResp3);
                setResp3Icon(-1);

                break;

            case 4:
                if (response == 2)
                {
                    manager.addInfamy(1);
                }
                conversation.setDialogue("Then why are you still standing here? Shoo!", false);

                resp1 = "Leave";
                func.Add(updateResp1);

                break;

            case 5:
                conversation.hide();
                walk(1, 1.1f);
                break;

            case 6:
                conversation.hide();
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
