using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene2 : Cutscene {

    public override void stepCutscene(int response) {
        base.stepCutscene(response);

        if (EventSystem.current.currentSelectedGameObject != null)
            conversation.setDialogue(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text, true);

        //Cutscene if did the last mission clean
        if (!crooked) {
            switch (stage) {
                case 0:
                    conversation.hide();
                    walk(-1, 1.6f);
                    break;

                case 1:
                    conversation.setOtherImage(sprites[2]);
                    conversation.show();

                    conversation.setOtherName("Border Guard\nJ. Mason");

                    conversation.setDialogue("", true);
                    conversation.setDialogue("You're back? I assume you've got a warrant now? Fine, you can look through my computer.", false);

                    resp1 = "No need, I've already gotten everything I need from your boss.";
                    func.Add(updateResp1);

                    resp2 = "I already know what happened, I've seen the recordings.";
                    func.Add(updateResp2);

                    break;

                case 2:
                    if (response == 0) {
                        conversation.setDialogue("Wait, my boss? He's not meant to be in the city until tomorrow...", false);
                    }
                    else if (response == 1) {
                        conversation.setDialogue("What? How? I'm the only one who has access to those at the moment.", false);
                    }

                    resp1 = "That doesn't matter. I know that a truck came through here an hour before the murder.";
                    func.Add(updateResp1);

                    resp2 = "You lied to me. You let a truck through an hour before the murder.";
                    setResp2Icon(-1);
                    func.Add(updateResp2);

                    break;

                case 3:
                    if (response == 0) {
                        conversation.setDialogue("Huh? Well yeah, that truck was scheduled. All it's paperwork checked out.", false);
                    }
                    else if (response == 1) {
                        manager.addInfamy(5);
                        conversation.setDialogue("No! Wait! I was doing my job! That truck had the right paperwork, I just didn't want to break confidentiality: it'd cost me my job!", false);
                    }
                    resp1 = "I need to know where that truck went. It could be a lead.";
                    func.Add(updateResp1);

                    resp2 = "I understand, but I need to know where that truck went. It could be important to the case.";
                    setResp2Icon(1);
                    func.Add(updateResp2);

                    resp3 = "You'll have a lot more to worry about than losing your job if you don't tell me where that truck went.";
                    setResp3Icon(-1);
                    func.Add(updateResp3);

                    break;

                case 4:
                    if (response == 0) {
                        conversation.setDialogue("Okay, I'll give you the address it was supposed to deliver to. I don't know anything else, I promise.", false);
                    }
                    if (response == 1) {
                        manager.addReputation(5);
                        conversation.setDialogue("Okay, I'll give you the address it was supposed to deliver to. I don't know anything else, I promise.", false);
                    }
                    else if (response == 2) {
                        manager.addInfamy(5);
                        conversation.setDialogue("Wait! Please, I'll tell you where it was going but I swear that's all I know! I'll help any way I can but I need this job to support my family!", false);
                    }

                    resp1 = "Thanks, I'll need to go and check it out.";
                    setResp1Icon(1);
                    func.Add(updateResp1);

                    resp2 = "Got it. Looks like I've got a new destination.";
                    func.Add(updateResp2);

                    resp3 = "About time. If this lead turns out to be bull I'll be back for you.";
                    setResp3Icon(-1);
                    func.Add(updateResp3);

                    break;

                case 5:
                    if (response == 0) {
                        manager.addReputation(2);
                    }
                    else if (response == 2) {
                        manager.addInfamy(2);
                    }
                    conversation.hide();
                    walk(1, 2f);
                    break;

                case 6:
                    conversation.show();
                    conversation.hideOther();

                    conversation.setDialogue("[There might be documents about that truck at this address, it's probably worth having a look around. Then again, some well placed 'evidence' could be enough to loosen the owner's tongue and save searching.]", true);
                    conversation.setDialogue("*How would you like to proceed*", false);

                    resp1 = "Look for information regarding the truck's whereabouts and contents.";
                    func.Add(updateResp1);

                    resp2 = "Plant incriminating evidence to blackmail the owner into talking.";
                    func.Add(updateResp2);
                    break;

                case 7:
                    conversation.hide();
                    bool crooked = response == 0 ? false : true;
                    loadLevel(crooked);
                    break;

                default:
                    break;
            }
        }
        else {

        }
    }

    private void loadLevel(bool crooked) {
        manager.setCrooked(crooked);
        manager.next();
    }
}
