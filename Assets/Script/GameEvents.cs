using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    // Sound events
    public static Action<AudioClip> OnPlaySFX;

    // Gameplay events
    public static Action<Item_Main_SO> OnObjectThrow;
    public static Action<Recipes_SO> TryCraftingItem;
    public static Action OnPlayerDied;
    public static Action<int> OnScoreChanged;
}
