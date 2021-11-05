/*
Editor tool for defining custom project color palette.
Defined colors should be accessible to scripts.
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Pallete))]
public class ColorPaletteTool : EditorWindow
{
    // Base const editor properties
    public const string TITLE = "Color Palette";
    public const string PATH = "Window/" + TITLE;
    public const string HEADER_LABEL = "You can define color project colors here.";

    public const string BTN_ADD_COLOR = "Add color";
    public const string BTN_REMOVE_COLOR = "Remove color";

    private Pallete m_Palette;

    //------------------------------------------------------
    // Editor functions 
    //------------------------------------------------------

    //------------------------------------------------------
    [MenuItem(PATH)]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ColorPaletteTool>(TITLE);
    }

    //------------------------------------------------------
    void OnGUI()
    {
        GUILayout.Label(HEADER_LABEL, EditorStyles.boldLabel);

        // Displaying list
        DisplayPaletteGUI();

        // Handling list
        AddColorGUI();
        RemoveColorGUI();
    }

    //------------------------------------------------------
    // Private functions 
    //------------------------------------------------------
    
    //------------------------------------------------------
    private void DisplayPaletteGUI()
    {
        foreach (PaletteColor col in Pallete.m_aPaletteColors)
        {
            EditorGUILayout.BeginHorizontal();

            col.m_sName = EditorGUILayout.TextField(col.m_sName);
            col.m_Color = EditorGUILayout.ColorField(col.m_Color);

            EditorGUILayout.EndHorizontal();
        }
    }

    //------------------------------------------------------
    private void AddColorGUI()
    {
        if (GUILayout.Button(BTN_ADD_COLOR))
        {
            Pallete.m_aPaletteColors.Add(new PaletteColor());
        }
    }

    //------------------------------------------------------
    // Remove selected palette color - TODO: select for remove 
    private void RemoveColorGUI()
    {
        if (GUILayout.Button(BTN_REMOVE_COLOR))
        {
            Pallete.m_aPaletteColors.Clear();
        }
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    public static Color ColorByName(string name)
    {
        PaletteColor colorDef = Pallete.m_aPaletteColors.Find(col => col.m_sName == name);
        if (colorDef != null)
            return colorDef.m_Color;

        return Color.magenta;
    }
    
}
