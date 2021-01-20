using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : Interactable
{
    public abstract void Attack();
    public abstract void AlternativeAttack();

    public override void Interact(GameObject player)
    {
        PickUp(player.GetComponent<GameplayCharacterController>());
    }

    void PickUp(GameplayCharacterController controller)
    {
        controller.OnPickup(this, gameObject);
        gameObject.layer = 0;
    }

    public void Throw()
    {

    }
}
