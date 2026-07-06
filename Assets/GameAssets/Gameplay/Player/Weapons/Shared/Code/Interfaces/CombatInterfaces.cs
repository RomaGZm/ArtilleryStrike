
using UnityEngine;

namespace AS.Gameplay.Player.Weapons.Shared.Interfaces
{

    public interface IAttack
    {
        void Attack();
    }

    public interface IReload
    {
        void Reload();
    }

    public interface IAim
    {
        void Aim(Vector3 point);
    }
}