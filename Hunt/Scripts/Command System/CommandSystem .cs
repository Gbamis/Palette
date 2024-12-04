using UnityEngine;
using Zenject;

namespace HT
{
    public class CommandSystem : MonoBehaviour
    {
        private ICommandable currentSelection;

        [Inject] private readonly GameplayEvent gameplayEvent;
        [Inject] private readonly GlobalData globalData;

        public LayerMask movementMask;
        public BezierCurveLine curvedLine;
        public AnimalAction reproduceCommand;
        public Gender currentSelectedGender;


        private void OnEnable()
        {
            gameplayEvent.OnUnit_Hover += HoverUnit;
            gameplayEvent.OnUnit_UnHover += UnHoverUnit;
            gameplayEvent.OnUnit_Select += SelectUnit;
            gameplayEvent.OnUnit_Move += MoveCurrentUnit;
        }
        private void OnDisable()
        {
            gameplayEvent.OnUnit_Hover -= HoverUnit;
            gameplayEvent.OnUnit_UnHover -= UnHoverUnit;
            gameplayEvent.OnUnit_Select -= SelectUnit;
            gameplayEvent.OnUnit_Move -= MoveCurrentUnit;
        }

        private void Start() => currentSelectedGender = Gender.NONE;


        public void SelectUnit(ICommandable obj, Gender gender)
        {
            if (gender == Gender.MALE)
            {
                UnselectUnit();
                obj.OnSelect();
                currentSelection = obj;
                currentSelectedGender = gender;
            }
            else
            {
                if (currentSelectedGender == Gender.MALE)
                {
                    BreedFromFemale();
                    UnselectUnit();
                }
                else
                {
                    obj.OnSelect();
                    currentSelection = obj;
                    currentSelectedGender = gender;
                }
            }
        }
        public void UnselectUnit()
        {
            if (currentSelection != null)
            {
                currentSelection.OnDeSelect();
                currentSelection = null;
                currentSelectedGender = Gender.NONE;
            }
        }

        public void HoverUnit(Vector3 end)
        {
            if (currentSelection == null) { return; }
            curvedLine.gameObject.SetActive(true);
            Vector3 start = currentSelection.Position();
            start.y += globalData.lineRendererHieght;
            end.y += globalData.lineRendererHieght;

            curvedLine.DrawCurve(start, end);
            SFX.Core.Play_FemaleGoatHovered();

        }
        public void UnHoverUnit() => curvedLine.gameObject.SetActive(false);


        public void MoveCurrentUnit()
        {
            if (currentSelection == null) { return; }
            Vector3 loc = GetClickedDestination();
            currentSelection.Unit__Move_Command(loc);
            UnselectUnit();
        }

        private Vector3 GetClickedDestination()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200, movementMask))
            {
                if (hit.collider != null)
                {
                    return hit.point;
                }
            }
            return Vector3.zero;
        }


        private void BreedFromFemale()
        {
            AnimalAction cmd = Instantiate(reproduceCommand);
            currentSelection.OnExecuteCommandable(cmd);

            Destroy(cmd);
            UnselectUnit();
        }
    }

}