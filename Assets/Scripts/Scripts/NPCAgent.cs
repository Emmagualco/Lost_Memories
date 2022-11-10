using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EG
{
    public class NPCAgent : MonoBehaviour
    {

        public PlayerCharacter npcData;

        private void Awake()
        {
            PlayerCharacter tmp = new PlayerCharacter();
            tmp.name = "Vergil";
            tmp.strength = 40;
            tmp.health = 100;
            tmp.defense = 20;
            tmp.description = "El mejor personaje de la historia";
            tmp.dexterity = 30;
            tmp.intelligence = 80;
            tmp.speed = 1;

            npcData = tmp;
        }
    }
}
