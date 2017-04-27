using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene3 : Cutscene {

    public override void stepCutscene(int response) {
        base.stepCutscene(response);

        if (EventSystem.current.currentSelectedGameObject != null)
            conversation.setDialogue(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text, true);

        print(stage);

        //Cutscene if did the last mission clean
        if (!crooked) {
            switch (stage) {
                case 0:
                    conversation.hide();
                    walk(-1, 1.5f);
                    break;

                case 1:
                    conversation.setOtherImage(sprites[1]);
                    conversation.show();

                    conversation.setOtherName("P.C. B. Leater");

                    conversation.setDialogue("", true);
                    conversation.setDialogue("Good work getting hold of those documents. We've managed to track down their origin to a warehouse on the edge of town.", false);

                    resp1 = "Good work. Any news on the border guard who stonewalled us?";
                    setResp1Icon(1);
                    func.Add(updateResp1);

                    resp2 = "Good. Did the border guard who lied to use get what was coming to him?";
                    setResp2Icon(-1);
                    func.Add(updateResp2);

                    break;

                case 2:
                    if (response == 0) {
                        manager.addReputation(3);
                        conversation.setDialogue("He fessed up, but he was just doing his job. We let him go but filed a warning to the company for giving us the run around.", false);
                    }
                    else if (response == 1) {
                        manager.addInfamy(3);
                        conversation.setDialogue("Yeah, poor sod's in for interrogation. Doubt he'll go away, but he'll probably be fined for interfering with police business.", false);
                    }

                    resp1 = "One loose end tied up at least. I'll head over to that warehouse now.";
                    func.Add(updateResp1);

                    resp2 = "Get me a squad car, it sounds like this warehouse needs a visit.";
                    func.Add(updateResp2);

                    break;

                case 3:
                    conversation.hide();
                    walk(1, 1.5f);
                    break;

                case 4:
                    loadLevel(false);
                    break;

                default:
                    break;
            }
        }
        else {
            switch (stage) {
                case 0:
                    conversation.hide();
                    walk(1, 1.3f);
                    break;

                case 1:
                    conversation.setOtherImage(sprites[2]);
                    conversation.show();

                    conversation.setOtherName("Manager");

                    conversation.setDialogue("", true);
                    conversation.setDialogue("How can I help you officer?", false);

                    resp1 = "I need to know where a delivery you recently recieved came from.";
                    func.Add(updateResp1);

                    resp2 = "You recently got a delivery by truck. You're going to tell me where it came from.";
                    setResp2Icon(-1);
                    func.Add(updateResp2);

                    break;

                case 2:
                    if (response == 0) {
                        conversation.setDialogue("I'm afraid that's confidential information, I can't just tell you information about my clients.", false);
                    }
                    else if (response == 1) {
                        manager.addInfamy(3);
                        conversation.setDialogue("No, I'm not. That's confidential information and it's going to stay that way.", false);
                    }

                    resp1 = "Confidential? Like this heroin I found hidden in your offices?";
                    func.Add(updateResp1);

                    break;

                case 3:
                    conversation.setDialogue("What are you..? Wait, I know what's happening here. Fine, take the address, I don't want any trouble. Just know the media will hear about this.", false);

                    resp1 = "See, wasn't that easy?";
                    func.Add(updateResp1);

                    resp2 = "Thanks. I'll make sure to forget any of this happened.";
                    setResp2Icon(1);
                    func.Add(updateResp2);

                    resp3 = "Are you sure you're in the best position to be making threats?";
                    setResp3Icon(-1);
                    func.Add(updateResp3);

                    break;

                case 4:
                    if (response == 1) {
                        manager.addReputation(3);
                    }
                    else if (response == 2) {
                        manager.addInfamy(3);
                    }

                    conversation.hide();
                    walk(-1, 1.3f);
                    break;

                case 5:
                    loadLevel(false);
                    break;

                default:
                    break;
            }
        }
    }

    private void loadLevel(bool crooked) {
        manager.setCrooked(crooked);
        manager.next(false);
    }
}
