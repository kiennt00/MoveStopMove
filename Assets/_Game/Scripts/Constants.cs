using UnityEngine;

public static class Constants
{
    public const string ANIM_IDLE = "idle";
    public const string ANIM_RUN = "run";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_DEAD = "dead";
    public const string ANIM_DANCE = "dance";
    public const string ANIM_ULTI = "ulti";
    public const string ANIM_VICTORY = "victory";

    public const string PP_NOT_FIRST_ATTEMPT = "FirstAttempt";
    public const string PP_IS_UNLOCK_ITEM = "IsUnlockItem";
    public const string PP_CURRENT_ITEM = "CurrentItem";
    public const string PP_PLAYER_NAME = "PlayerName";
    public const string PP_CURRENT_GOLD = "CurrentGold";

    public static readonly WaitForSeconds WFS_0_S = new WaitForSeconds(0f);
    public static readonly WaitForSeconds WFS_0_S_5 = new WaitForSeconds(0.5f);
    public static readonly WaitForSeconds WFS_1_S = new WaitForSeconds(1f);
    public static readonly WaitForSeconds WFS_2_S = new WaitForSeconds(2f);
    public static readonly WaitForSeconds WFS_2_S_5 = new WaitForSeconds(2.5f);
}