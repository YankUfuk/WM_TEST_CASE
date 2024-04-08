using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    
    public Text livesText;// this parts wants a reference to which text to use
    void Update()
    {
        livesText.text = PlayerStats.lives.ToString() + " LIVES";// this part updates live number when an enemy pass the end line.
    }
}
