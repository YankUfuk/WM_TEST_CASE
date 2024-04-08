using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr; // Reference to the SpriteRenderer component
    
    [SerializeField] private Color changeColor; // Color to change to when traveling over the plot

    
    private GameObject tower; // Reference to the instantiated tower object
    
    private Color startColor; // Original color of the plot

    private void Start()
    {
        startColor = sr.color; // Initial color of the plot
    }

    private void OnMouseEnter()
    {
        sr.color = changeColor; // Change the color of the plot when mouse enters
    }

    private void OnMouseExit()
    {
        sr.color = startColor; // Reset the color of the plot when mouse exits
    }

    private void OnMouseDown()
    {
        // Checks if a tower is already built on this plot
        if (tower != null) return;

        
        Tower towerToBuild = BuildManager.main.GetSelectedTower(); // Get the tower selected to build from the BuildManager

        // Checks if the cost of the selected tower exceeds available currency
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            return;
        }

        // Reduce the cost of the tower from the player's currency
        LevelManager.main.SpendCurrency(towerToBuild.cost);

        // Instantiate the selected tower at the plot's position
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
}
