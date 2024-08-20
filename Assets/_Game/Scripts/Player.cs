using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody rb;

    public int rank;
    public string killedBy;
    public Material killedByMaterialColor;

    public int Level => level;

    private void Start()
    {
        WeaponType weaponType = DataManager.Ins.GetCurrentItem<WeaponType>();
        HairType hairType = DataManager.Ins.GetCurrentItem<HairType>();
        ShieldType shieldType = DataManager.Ins.GetCurrentItem<ShieldType>();
        PantsType pantsType = DataManager.Ins.GetCurrentItem<PantsType>();

        EquipWeapon(weaponType);
        EquipHair(hairType);
        EquipShield(shieldType);
        EquipPants(pantsType);
    }

    public void FixedUpdate()
    {
        if (isDead || !GameManager.Ins.IsState(GameState.Gameplay))
        {
            return;
        }

        rb.velocity = new Vector3(joystick.Horizontal * MoveSpeed, rb.velocity.y, joystick.Vertical * MoveSpeed);
        Vector3 direction = new(joystick.Horizontal, 0, joystick.Vertical);

        if (direction != Vector3.zero)
        {
            isMoving = true;
            tf.rotation = Quaternion.LookRotation(direction);
            ChangeAnim(Constants.ANIM_RUN);
        }
        else
        {
            isMoving = false;
            if (!isAttacking)
            {
                ChangeAnim(Constants.ANIM_IDLE);
            }
        }
    }

    protected override void HandleGameStateChanged(GameState gameState)
    {
        base.HandleGameStateChanged(gameState);
        if (gameState == GameState.Mainmenu)
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
        else if (gameState == GameState.ShopSkin)
        {
            ChangeAnim(Constants.ANIM_DANCE);
        }
    }

    public override void SetTarget()
    {
        base.SetTarget();
        if (targetedCharacter != null)
        {
            targetedCharacter.targetedImage.SetActive(true);
        }
    }

    public override void RemoveTarget(Character character)
    {
        base.RemoveTarget(character);
        if (character == targetedCharacter)
        {
            character.targetedImage.SetActive(false);
        }
    }

    public override void StopMove()
    {
        base.StopMove();
        rb.velocity = Vector3.zero;
    }

    protected override IEnumerator IEDead()
    {
        yield return StartCoroutine(base.IEDead());
        LevelManager.Ins.Finish();
    }

    protected override void SetActiveCharacterInfo(bool isActive)
    {
        base.SetActiveCharacterInfo(isActive);
        attackZoneCollider.gameObject.SetActive(isActive);
    }

    public void Revive()
    {
        isDead = false;
    }

    public void RefreshItem<ItemType>() where ItemType : Enum
    {
        ItemType itemType = DataManager.Ins.GetCurrentItem<ItemType>();

        if (itemType is HairType hairType)
        {
            EquipHair(hairType);
        }
        else if (itemType is PantsType pantsType)
        {
            EquipPants(pantsType);
        }
        else if (itemType is ShieldType shieldType)
        {
            EquipShield(shieldType);
        }
        else if (itemType is WeaponType weaponType)
        {
            EquipWeapon(weaponType);
        }
    }
}
