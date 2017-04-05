using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene1 : Cutscene {
    
    public override void stepCutscene(int response) {
        base.stepCutscene(response);

        if (EventSystem.current.currentSelectedGameObject != null)
            conversation.setDialogue(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text, true);

        switch (stage) {
            case 0:
                conversation.setPlayerImage(sprites[0]);
                conversation.setOtherImage(sprites[1]);

                conversation.setOtherName("Officer G. Billy");

                conversation.setDialogue("Good that you're here detective, this one's out of my league.", false);

                resp1 = "What do we have?";
                func.Add(updateResp1);

                resp2 = "Report, officer.";
                func.Add(updateResp2);
                break;

            case 1:
                if (response == 0) {
                    conversation.setDialogue("Looks pretty bad sir, the teeth and claw marks suggest this wasn't the work of a goat.", false);
                }
                else if (response == 1) {
                    conversation.setDialogue("Homocide. Large bite marks and deep, slashing wounds. This wasn't the work of a goat, sir.", false);
                }

                resp1 = "Any leads on what it was?";
                func.Add(updateResp1);

                resp2 = "That fits the description of a troll attack to the letter.";
                func.Add(updateResp2);
                break;

            case 2:
                if (response == 0) {
                    conversation.setDialogue("Evidence is pointing to this being a troll attack, sir.", false);
                }
                else if (response == 1) {
                    conversation.setDialogue("That's the conclusion we'd come to as well.", false);
                }

                resp1 = "This close to the city? How did a troll get past the border guards?";
                func.Add(updateResp1);

                break;

            case 3:
                conversation.setDialogue("We don't know yet, we've been questioning the guard that was on duty at the time, but he's been unresponsive so far.", false);
                
                resp1 = "He has to know something, I'll try talking to him.";
                func.Add(updateResp1);

                resp2 = "Leave it to me, I know how to make people talk.";
                func.Add(updateResp2);

                break;

            case 4:
                conversation.hide();
                walk(1, 1.1f);
                break;

            case 5:
                conversation.setOtherImage(sprites[2]);
                conversation.show();

                conversation.setOtherName("Border Guard J. Mason");

                conversation.setDialogue("", true);
                conversation.setDialogue("I've already told you lot, I've got no idea what happened! He didn't even make any noise.", false);
                
                resp1 = "Calm down, I'm not here to point fingers. I just want to ask some questions.";
                func.Add(updateResp1);

                resp2 = "Trolls don't just appear. I need to know anything suspicious that happened.";
                func.Add(updateResp2);

                resp3 = "You're lying. There's no way a troll just snuck past you.";
                func.Add(updateResp3);
                break;

            case 6:
                if (response == 0 || response == 1) {
                    conversation.setDialogue("I don't know anything I haven't told the other officers, I swear!", false);
                }
                else if (response == 2) {
                    conversation.setDialogue("No! I'm telling the truth, I swear! Aren't I supposed to be innocent until proven guilty?!", false);
                    manager.infamy++;
                }

                resp1 = "I'm going to need access to the security camera footage.";
                func.Add(updateResp1);
                break;

            case 7:
                conversation.setDialogue("I'm not letting you search through those files without a warrant: it contains highly sensitive material.", false);
                
                resp1 = "Fine, we'll do it your way. I'll be back with a warrant.";
                func.Add(updateResp1);
                break;

            case 8:
                conversation.hide();
                walk(1, 2);
                break;

            case 9:
                conversation.show();
                conversation.hideOther();

                conversation.setDialogue("[Getting a warrant is going to take too long, and I can't risk letting those files get tampered with. Maybe there's a window I can sneak in.]", true);
                conversation.setDialogue("*How would you like to proceed*", false);

                resp1 = "Sneak in an copy the files from the computer for later analysis.";
                func.Add(updateResp1);

                resp2 = "Sneak in and plant incriminating data on the computer to use as leverage.";
                func.Add(updateResp2);
                break;

            case 10:
                conversation.hide();
                bool crooked = response == 0 ? false : true;
                loadLevel(crooked);

                break;

            default:
                break;
        }
    }

    private void loadLevel(bool crooked) {
        StartCoroutine(manager.nextLevel());
    }
}
