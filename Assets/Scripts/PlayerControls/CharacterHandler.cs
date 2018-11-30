using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{

    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public CharacterClass characterClass;
    // Use this for initialization
    void Start ()
    {
        //statArray = new string[] { "Strength", "Agility", "Constitution", "Elemental", "Charisma", "Intelligence" };
        //for (int i = 0; i < statArray.Length; i++)
        //{
        //    stats[i] = PlayerPrefs.GetInt(statArray[i], 10);
        //}
        //characterClass = (CharacterClass)System.Enum.Parse(typeof(CharacterClass), PlayerPrefs.GetString("CharacterClass", "Barbarian"));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
