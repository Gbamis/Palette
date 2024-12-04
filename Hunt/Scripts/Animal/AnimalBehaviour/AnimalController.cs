using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace HT
{
    public class AnimalController : MonoBehaviour, ICommandable
    {
        public GameplayEvent gameplayEvent;
        public GlobalData globalData;
        public MouseMode mouseMode;

        private int idle_shuffle;
        private bool selected;
        private bool isInitiated;

        [HideInInspector] public AnimalStat animalStat;
        private AnimalBehaviour animalBehaviour;
        private AnimalPlacement animalPlacement;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private DebugData debugData;

        public bool isCommand;
        public GameObject selectedIcon;
        public LayerMask collisionMask;
        public LayerMask terrainMask;
        public float debugDis;
        public float radius;

        private static readonly int die = Animator.StringToHash("die");
        private static readonly int a_velocity = Animator.StringToHash("velocity");
        private static readonly int a_eating = Animator.StringToHash("eating");
        private static readonly int a_rest = Animator.StringToHash("rest");

        CancellationTokenSource cancelToken = new();


        public void Init(AnimalStat stat, Gene m_gene)
        {
            animator = GetComponent<Animator>();
            animalBehaviour = GetComponent<AnimalBehaviour>();
            animalPlacement = GetComponent<AnimalPlacement>();
            debugData = GetComponent<DebugData>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            navMeshAgent.speed = UnityEngine.Random.Range(1, 1.3f);
            navMeshAgent.angularSpeed = UnityEngine.Random.Range(600, 800);
            navMeshAgent.acceleration = UnityEngine.Random.Range(4, 7);
            animalStat = stat;
            idle_shuffle = 0;
            StopAllCoroutines();

            animalBehaviour.currentBehavior = null;

            if (!isInitiated)
            {
                isInitiated = true;
                Task_InitalBehaviour().Forget();

            }
        }

        public void OnDestory() => cancelToken.Cancel();
        private async UniTaskVoid Task_InitalBehaviour()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancelToken.Token);
            animalBehaviour.DefaultAction().Execute(this);
        }


        public void OnPointerClick(PointerEventData ped)
        {
            if (mouseMode.mode == MouseMode.Mode.NUTRITION
            || mouseMode.mode == MouseMode.Mode.DELETION) { return; }

            if (ped.pointerId == -1)
            {
                Vector3 player = Routine.Instance.player.position;
                float dist = Vector3.Distance(transform.position, player);

                if (dist > globalData.selectionDistance)
                {
                    SFX.Core.RangeShow();
                    gameplayEvent.OnShowSelectionRange?.Invoke();
                }
                else
                {
                    gameplayEvent.OnUnit_Select(this, animalStat.gender);
                    selectedIcon.SetActive(true);
                }
            }

        }

        public void OnSelect()
        {
            mouseMode.mode = MouseMode.Mode.UNIT;
            selected = true;
            SFX.Core.ObjectSelect();
        }

        public void OnDeSelect()
        {
            mouseMode.mode = MouseMode.Mode.CAMERA;
            selected = false;
            SFX.Core.ObjectUnDeselect();
        }

        public Vector3 Position() => transform.position;

        public void OnExecuteCommandable(AnimalAction command)
        {
            if (animalStat.GetStamina() > 0.3f && !isCommand)
            {
                command.Execute(this);
            }
        }

        private void OnFinhsedExecutingAction()
        {
            AnimalAction animalAction = animalBehaviour.DecideBestAction();
            if (animalAction != null && !isCommand)
            {
                animalAction.Execute(this);
            }
        }

        private void MotionValue(float value)
        {
            if (animator != null)
            {
                animator.SetFloat(a_velocity, value);
            }
        }
        private void EatValue(bool value)
        {
            if (animator != null)
            {
                animator.SetBool(a_eating, value);
            }
        }
        private void RestValue(bool value)
        {
            if (animator != null)
            {
                animator.SetBool(a_rest, value);
            }
        }

        public void Execute_Idle_Action(float duration)
        {
            animalPlacement.AdjustPlacement();

            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            float randIdle = UnityEngine.Random.Range(0, 2);

            switch (randIdle)
            {
                case 0:
                    Task_Idle(duration).Forget();
                    break;
                case 1:
                    Vector3 endPos = GetRandomPositionFromSource(transform.position);
                    bool foundBlocker = CheckBlocker(endPos);
                    if (!foundBlocker)
                    {
                        Task_Roam_Towards(endPos).Forget();
                    }
                    else
                    {
                        OnFinhsedExecutingAction();
                    }
                    break;
            }
        }
        private async UniTaskVoid Task_Idle(float duration)
        {
            MotionValue(0); EatValue(false); RestValue(false);

            float total = duration;
            while (total > 0 && !isCommand)
            {
                total -= Time.deltaTime;
                animalStat.ReduceStamina(2);
                await UniTask.Yield();
            }
            OnFinhsedExecutingAction();
        }
        private async UniTaskVoid Task_Roam_Towards(Vector3 pos)
        {
            bool found = CheckBlocker(pos);
            bool land = CheckLandAtPos(pos);
            bool canGo = !found && land;

            MotionValue(1); EatValue(false); RestValue(false);

            Vector3 dir = pos - transform.position;
            float step = 0;
            while (Vector3.Distance(transform.position, pos) > 0.001f && step < 1 && canGo)
            {
                SetPositionRotation(pos, dir, 0.002f);
                animalStat.ReduceStamina(10);
                animalPlacement.AdjustPlacement();
                step += Time.deltaTime;
                await UniTask.Yield();
            }

            MotionValue(0); EatValue(true);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            EatValue(false);
            OnFinhsedExecutingAction();
        }


        public void Execute_Rest_Action(float duration)
        {
            Task_Rest(duration).Forget();
        }
        private async UniTaskVoid Task_Rest(float duration)
        {
            isCommand = true;
            MotionValue(0); EatValue(false); RestValue(true);
            float total = duration;
            while (total > 0)
            {
                total -= Time.deltaTime;
                await UniTask.Yield();
            }
            isCommand = false;
            OnFinhsedExecutingAction();
        }


        public void Execute_Feed_Action(float duration)
        {
            if (animalStat.place_of_action != null)
            {
                Task_Goto_Food(animalStat.place_of_action.position, duration).Forget();
            }
        }
        private async UniTaskVoid Task_Goto_Food(Vector3 pos, float duration)
        {
            RestValue(false);
            bool found = CheckBlocker(pos);
            bool land = CheckLandAtPos(pos);
            if (!found)
            {
                if (land)
                {
                    Vector3 dir = pos - transform.position;
                    while (Vector3.Distance(transform.position, pos) > 0.8f
                    && !isCommand)
                    {
                        MotionValue(1);
                        animalStat.ReduceStamina();
                        SetPositionRotation(pos, dir, 0);
                        animalPlacement.AdjustPlacement();

                        debugDis = Vector3.Distance(transform.position, pos);
                        await UniTask.Yield();
                    }
                    MotionValue(0);
                    await Task_Eat();
                }
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                OnFinhsedExecutingAction();
            }



        }
        private async UniTask Task_Eat()
        {
            if (animalStat.place_of_action != null)
            {
                IFood food = animalStat.place_of_action.GetComponent<IFood>();

                EatValue(true);
                while (animalStat.GetStamina() != 1
                && animalStat.place_of_action != null && !isCommand)
                {
                    animalStat.AddStamina(food.Consume(globalData.grassConsumptionAmount));
                    await UniTask.Yield();
                }
            }

            EatValue(false);
            OnFinhsedExecutingAction();
        }


        public void Execute_Flee_Action(float duration)
        {
            Task_Flee(duration).Forget();
        }
        private async UniTaskVoid Task_Flee(float duration)
        {
            navMeshAgent.speed += 2;
            navMeshAgent.enabled = true;


            Vector3 pos = animalStat.place_of_action.position;
            Vector3 dir = transform.position - (pos - transform.position);
            float step = 0;
            navMeshAgent.SetDestination(dir);
            while (step < duration)
            {
                animalStat.ReduceStamina(20);
                MotionValue(2);
                step += Time.deltaTime;
                await UniTask.Yield();
            }
            MotionValue(0);
            navMeshAgent.speed -= 2;
            navMeshAgent.enabled = false;
            OnFinhsedExecutingAction();
        }


        public void Unit__Move_Command(Vector3 location)
        {
            if (!isCommand && animalStat.GetStamina() > 0.1f)
            {
                Task_MoveTo(location).Forget();
            }
        }
        private async UniTaskVoid Task_MoveTo(Vector3 pos)
        {
            isCommand = true;
            bool should_run = Vector3.Distance(transform.position, pos) > 5;
            navMeshAgent.enabled = true;
            navMeshAgent.speed = should_run ? navMeshAgent.speed += 2 : navMeshAgent.speed;

            if (animator.GetBool(a_rest))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }
            EatValue(false); RestValue(false);

            navMeshAgent.SetDestination(pos);

            while (Vector3.Distance(transform.position, pos) > 0.9f)
            {
                navMeshAgent.destination = pos;
                animalStat.ReduceStamina(20);
                MotionValue(should_run ? 2 : 1);
                await UniTask.Yield();
            }

            MotionValue(0);
            navMeshAgent.speed = should_run ? navMeshAgent.speed -= 2 : navMeshAgent.speed;
            navMeshAgent.enabled = false;
            isCommand = false;

            OnFinhsedExecutingAction();
        }


        public void Execute_FollowPlayer_Command()
        {
            isCommand = true;
            Task_Follow().Forget();
        }
        private async UniTaskVoid Task_Follow()
        {
            float defSpeed = navMeshAgent.speed;

            navMeshAgent.enabled = true;

            MotionValue(1); EatValue(false); RestValue(false);
            Transform player = Routine.Instance.player;
            while (Vector3.Distance(transform.position, player.position) > 3f)
            {
                if (Vector3.Distance(transform.position, player.position) > 5)
                {
                    navMeshAgent.speed = defSpeed + 2;
                    MotionValue(2);
                }
                else
                {
                    navMeshAgent.speed = defSpeed;
                    MotionValue(1);
                }
                navMeshAgent.destination = player.position;
                await UniTask.Yield();
            }
            MotionValue(0);

            isCommand = false;
            navMeshAgent.enabled = false;
            OnFinhsedExecutingAction();
        }


        public async void Execute_Reproduce_Action(IGoat male, IGoat female, float reproductionTime)
        {
            bool max = gameplayEvent.OnCheckIsBreedMaxed();
            if (!max)
            {
                isCommand = true;
                RestValue(true);

                await Task_Reproduce(reproductionTime);

                gameplayEvent.OnBreedGoat(male, female, transform);
                animalStat.ReplenishHeatLevel();
                SFX.Core.GoatBleat(transform.position);

                isCommand = false;
                OnFinhsedExecutingAction();
            }
            else
            {

            }
        }
        private async UniTask Task_Reproduce(float reproductionTime)
        {
            animalStat.ResetHeatLevel();
            float time = 0;
            while (time < reproductionTime * 1000)
            {
                time += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        private Vector3 GetRandomPositionFromSource(Vector3 source)
        {
            UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
            Vector2 pos = UnityEngine.Random.insideUnitCircle * 2;
            Vector3 endPos = new Vector3(pos.x + source.x, source.y, source.z + pos.y);
            return endPos;
        }
        private void SetPositionRotation(Vector3 pos, Vector3 dir, float add = 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, (Time.fixedDeltaTime * 1f) + add);
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * 3f);
        }

        private bool CheckBlocker(Vector3 endPos)
        {
            Vector3 origin = transform.position;
            Vector3 dir = endPos - transform.position;
            origin.y += 0.3f;

            Color green = Color.green;
            green.a = 0.5f;

            Routine.Instance.AddInfo(new DebugCheck() { radius = radius, debugColor = green, center = endPos });
            Debug.DrawRay(origin, dir, Color.yellow, 2);


            Collider[] colliders = new Collider[7];
            Physics.OverlapSphereNonAlloc(endPos, radius, colliders, collisionMask);
            foreach (Collider col in colliders)
            {
                return col != null && col.transform != transform;
            }

            Ray ray = new(origin, dir);
            if (Physics.Raycast(ray, out RaycastHit info, 1, collisionMask))
            {
                return info.collider != null && info.collider.transform != transform;
            }


            return false;
        }

        private bool CheckLandAtPos(Vector3 pos)
        {
            Vector3 origin = pos;
            origin.y += 1;
            Vector3 dir = Vector3.down;

            Debug.DrawRay(origin, dir, Color.blue, 2);

            Ray ray = new(origin, dir);
            if (Physics.Raycast(ray, out RaycastHit info, 8, terrainMask))
            {
                return info.collider != null;
            }

            return false;
        }

    }
}
