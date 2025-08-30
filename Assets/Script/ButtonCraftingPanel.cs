using UnityEngine;

public class ButtonCraftingPanel : MonoBehaviour
{
    [SerializeField] Animator CraftingPanelAnimator;

    public void OpenCloseCraftingPanel()
    {
        CraftingPanelAnimator.SetBool("Opening", (!CraftingPanelAnimator.GetBool("Opening")));
        GameEvents.OpenCraftingPanel?.Invoke();
    }

}
