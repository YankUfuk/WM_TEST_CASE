using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;// checks whether menu is on the left or right part and defult value is on the right

    //this method changes state of the menu from open to close or opposite and makes it slide.
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    // This method is called by Unity every time the GUI (Graphical User Interface) is updated.
    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();// Update the text of currencyUI to display the current amount of currency from LevelManager.
    }

}
