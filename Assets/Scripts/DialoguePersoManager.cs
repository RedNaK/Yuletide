using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePersoManager : MonoBehaviour
{
    public DialoguePerso dialogue;
    public int myQuestID = -1;
    public bool jesuislaloutre = false;
    public bool jesuislaboite = false;
    public bool firstQui = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void click()
    {
        string Contenu = "";
        if (!jesuislaboite)
        {
            if (!jesuislaloutre || GameManager.instance.questID == 0)
            {
                Debug.Log(GameManager.instance.questID + " " + GameManager.instance.questStep);
                if (GameManager.instance.questID < myQuestID)
                {
                    Contenu = dialogue.nope;
                }
                else if (GameManager.instance.questID == myQuestID)
                {
                    switch (GameManager.instance.questStep)
                    {
                        case 0:
                            Contenu = dialogue.probleme;
                            GameManager.instance.dialogueNext();
                            break;

                        case 1:
                            Contenu = dialogue.probleme2;
                            break;

                        case 2:
                            Contenu = dialogue.indication;
                            GameManager.instance.dialogueNext();
                            break;

                        case 3:
                            Contenu = dialogue.rappel;
                            break;
                    }
                }
                else
                {
                    Contenu = dialogue.fin;
                }
            }
            else if (jesuislaloutre)
            {
                if (myQuestID == 5)
                {
                    Contenu = dialogue.fin;
                }
                else
                {

                    DialogueLoutreComponent lC = GetComponent<DialogueLoutreComponent>();
                    DialogueLoutre bonPerso = lC.Blaireau;

                    switch (GameManager.instance.questID)
                    {
                        case 1:
                            bonPerso = lC.Blaireau;
                            break;
                        case 2:
                            bonPerso = lC.Herisson;
                            break;

                        case 3:
                            bonPerso = lC.Chouette;
                            break;

                        case 4:
                            bonPerso = lC.ChauveSouris;
                            break;
                    }

                    switch (GameManager.instance.questStep)
                    {
                        case 0:
                            if (firstQui)
                            {
                                Contenu = bonPerso.qui;
                                Invoke("goToSecondQui", 10f);
                            }
                            else
                            {
                                Contenu = bonPerso.qui2;
                            }
                            break;

                        case 1:
                            Contenu = bonPerso.ouobjet;
                            break;

                        case 2:
                            Contenu = bonPerso.aquiobjet;
                            break;

                        case 3:
                            Contenu = bonPerso.ouobjetdeco;
                            break;
                    }
                }
            }
            GameManager.instance.setPopupContent(dialogue.popupImage, dialogue.nomPerso, Contenu);
            GameManager.instance.openPopup();
        }
        else
        {
            DialogueBoiteComponent bC = GetComponent<DialogueBoiteComponent>();
            DialogueMultiple bonDial = bC.Debut;
            switch (GameManager.instance.questID)
            {
                case 0:
                    if (firstQui)
                    {
                        bonDial = bC.Debut;
                        firstQui = false;
                    }
                    else
                    {
                        bonDial = bC.Onika;
                    }
                    break;
                case 1:
                    bonDial = bC.Blaireau;
                    break;
                case 2:
                    bonDial = bC.Herisson;
                    break;

                case 3:
                    bonDial = bC.Chouette;
                    break;

                case 4:
                    bonDial = bC.ChauveSouris;
                    break;
                case 5:
                    bonDial = bC.Fin;
                    GameManager.instance.dialogueNext();
                    break;
            }
            GameManager.instance.setPopupContent(bonDial.persoGauche, bonDial.nomPersoGauche, bonDial.g1);
            GameManager.instance.openPopup(bonDial.persoDroite, bonDial.nomPersoDroite, bonDial.d1);
        }
    }

    public void goToSecondQui()
    {
        firstQui = false;
    }

}
