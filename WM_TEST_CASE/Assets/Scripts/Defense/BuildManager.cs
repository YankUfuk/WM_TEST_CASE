using UnityEngine;

// This class manages the construction of towers in the game
public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [SerializeField] private Tower[] towers; // Array to store available towers
    private int selectedTower = 0; // Index of the currently selected tower

    private void Awake()
    {
        main = this; // Singleton pattern to ensure only one instance of BuildManager exists
    }

    // Returns the currently selected tower
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    // Sets the currently selected tower based on the given index
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
