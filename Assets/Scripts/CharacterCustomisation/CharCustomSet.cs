using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCustomSet : MonoBehaviour
{
    #region CharRenderer
    //public Mesh model1;
    //public Mesh model2;
    //public MeshFilter meshFilt;
    //public List<MeshFilter> mesh = new List<MeshFilter>();
    public List<Mesh> mesh = new List<Mesh>();
    public MeshFilter meshFilt;

    #endregion
    #region CharClass
    public string[] statArray = new string[6]; //names of the available stats in the array
    public int[] stats = new int[6];//the stat points associated with stat array
    public int[] tempStats = new int[6];//temporary stats (used when new character is made)
    public int statpoints = 10;//available stats points that can be spent
    public CharacterClass charClass = CharacterClass.Warrior;//Available classes to be chosen (default set to warrior)
    public string[] selectedClass = new string[6];
    public int selectedIndex = 0;
    public int modelIndex;
    #endregion
    #region Character
    public string charName =  "Name";//player name
    public Color[] color = new Color[10];
    public int colorMax;
    public int modelMax;
    public PlayerModels charModels = PlayerModels.Box;
    #endregion

    // Use this for initialization
    void Start()
    {

        //for each of the model 
        //for (int i = 0; i < modelMax; i++)
        //{
        //    MeshFilter temp = Resources.Load("Library/unitydefaultresources" + i) as MeshFilter;
        //    mesh.Add(temp);
        //}
        for (int i = 0; i < modelMax; i++)
        {
            Mesh temp = Resources.Load("Library/unitydefaultresources_" + i) as Mesh;
            mesh.Add(temp);
        }

        meshFilt = GameObject.Find("CharModel").GetComponent<MeshFilter>();

        //meshFilt = GameObject.Find("CharModel").GetComponent<MeshFilter>();
        
        //if (meshFilt)
        //{
        //    meshFilt.mesh = model1;
        //}
        SetTexture("Model", 0);
        statArray = new string[] { "Strength", "Agility", "Constitution", "Elemental", "Charisma", "Intelligence"};
    }
    void SetTexture(string type, int next)
    {
        // int i = 0, next = 0,
        int index = 0, max = 0, mIndex = 0;
        Mesh[] pMesh = new Mesh[0];

        switch (type)
        {
            case "Model":
                index = modelIndex;
                max = modelMax;
                pMesh = mesh.ToArray();
                mIndex = 1;
                break;
        }

        index += next;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        //MeshFilter[] mat = mesh.Add;

        //mat[mIndex] = pMesh[index];

        //meshFilt.mesh = mat;

        switch (type)
        {
            case "Model":
                modelIndex = index;
                break;

        }
    }
    private void OnGUI()
    {
        float scrW = Screen.width / 16 ;
        float scrH = Screen.height / 9;

        int i = 0;

        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Model", -1);
        }
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Model");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Model", 1);
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        }
        i++;

    }


    void AvailableClasses(int className)
    {
        switch (className)
        {
            case 0:
                stats[0] = 6;
                stats[1] = 6;
                stats[2] = 6;
                stats[3] = 5;
                stats[4] = 5;
                stats[5] = 5;
                charClass = CharacterClass.Warrior;
                break;
            case 1:
                stats[0] = 4;
                stats[1] = 6;
                stats[2] = 5;
                stats[3] = 10;
                stats[4] = 5;
                stats[5] = 7;
                charClass = CharacterClass.Mage;
                break;
            case 2:
                stats[0] = 9;
                stats[1] = 2;
                stats[2] = 10;
                stats[3] = 1;
                stats[4] = 4;
                stats[5] = 4;
                charClass = CharacterClass.Tank;
                break;
            case 3:
                stats[0] = 4;
                stats[1] = 9;
                stats[2] = 6;
                stats[3] = 2;
                stats[4] = 5;
                stats[5] = 8;
                charClass = CharacterClass.Hunter;
                break;
            case 4:
                stats[0] = 3;
                stats[1] = 6;
                stats[2] = 7;
                stats[3] = 8;
                stats[4] = 5;
                stats[5] = 6;
                charClass = CharacterClass.Healer;
                break;
            case 5:
                stats[0] = 4;
                stats[1] = 8;
                stats[2] = 5;
                stats[3] = 4;
                stats[4] = 9;
                stats[5] = 7;
                charClass = CharacterClass.Thief;
                break;

        }


    }
}
public enum CharacterClass
{
    Warrior,
    Mage,
    Tank,
    Hunter,
    Healer,
    Thief

}
public enum PlayerModels
{
    Box,
    Capsule
}
