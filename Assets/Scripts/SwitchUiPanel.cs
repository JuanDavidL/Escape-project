using UnityEngine;

public class SwitchUiPanel : MonoBehaviour
{
    public void SwitchToPanel(GameObject panelToSwitch)
    {
        panelToSwitch.SetActive(true);
        gameObject.SetActive(false);
    }
}
