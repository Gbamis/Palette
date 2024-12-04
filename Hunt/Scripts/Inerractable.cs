using System;
using UnityEngine.EventSystems;
using UnityEngine;

namespace HT
{
    public interface ICommandable : IPointerClickHandler
    {
        void OnSelect() { }
        void OnDeSelect() { }
        void OnExecuteCommandable(AnimalAction command) { }
        void Unit__Move_Command(Vector3 loc) { }
        Vector3 Position() { return Vector3.zero; }
    }

    public interface IClickable
    {
        void OnClick() { }
    }

    public interface IHoverable : IPointerEnterHandler, IPointerExitHandler
    {
        void OnHighlight() { }

        void OnDeHightlight() { }
    }

    public interface IDamageable
    {
        void TakeDamage(int val) { }
    }
}
