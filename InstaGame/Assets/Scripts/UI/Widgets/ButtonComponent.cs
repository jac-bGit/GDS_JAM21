/*
Component for base button behavior.
*/
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonComponent : MonoBehaviour
{
    // Colors
    [SerializeField] protected string m_sColorIdDefault;
    [SerializeField] protected string m_sColorIdHover;

    // Text
    [SerializeField] protected string m_sText;

    // Component references 
    protected Button m_Button;
    protected Text m_Text;

    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------
    
    //------------------------------------------------------
    void Start()
    {
        
    }

    //------------------------------------------------------
    void Update()
    {
        //BasicWidgetSetup();
    }

    //------------------------------------------------------
    void OnGUI()
    {
        BasicWidgetSetup();
    }

    //------------------------------------------------------
    // Private functions 
    //------------------------------------------------------
    
    //------------------------------------------------------
    // Setting up basic widget properties
    // Happening in editor 
    protected void BasicWidgetSetup()
    {
        // Find button component 
        /*if (m_Button == null)
            m_Button = GetComponent<Button>();

        // Button check 
        if (m_Button == null)
            return;
        */
        // Setup button colors 
        //ColorBlock colBlock = ColorBlock.defaultColorBlock;

        //colBlock.normalColor = Pallete.ColorByName(m_sColorIdDefault);
        //colBlock.highlightedColor = Pallete.ColorByName(m_sColorIdHover);
        //colBlock.pressedColor = Pallete.ColorByName(m_sColorIdDefault);

        // Text 
        m_Text = GetComponentInChildren<Text>();
        m_Text.text = m_sText;

        // Image 
        //Image img = GetComponent<Image>();
        //img.color = Pallete.ColorByName(m_sColorIdDefault);
    }
}
