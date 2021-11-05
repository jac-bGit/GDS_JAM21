using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------
[System.Serializable]
[CreateAssetMenu(fileName = "Palette", menuName = "InstaGame/Palette", order = 0)]
public class Pallete : ScriptableObject
{
    [SerializeField] public string m_sPaletteName;
    [SerializeField] public static List<PaletteColor> m_aPaletteColors = new List<PaletteColor>();

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    public Color ColorByName(string name)
    {
        PaletteColor colorDef = m_aPaletteColors.Find(col => col.m_sName == name);
        if (colorDef != null)
            return colorDef.m_Color;

        return Color.magenta;
    }   

}

//------------------------------------------------------
[System.Serializable]
public class PaletteColor
{
    [SerializeField] public string m_sName;
    [SerializeField] public Color m_Color;

    //------------------------------------------------------
    public PaletteColor()
    {
        m_sName = "Color";   
        m_Color = Color.white;  
    }
}
