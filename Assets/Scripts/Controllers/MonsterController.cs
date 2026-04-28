using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10;

    [SerializeField]
    float _attackRange = 2;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.CreateWorldSpaceUI<UI_HPBar>(transform);

    }

    void OnHitEvent()
    {
        Debug.Log("Monster Hit Event");
        if (_lockTarget)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance < _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;

            }
            else
                State = Define.State.Idle;
        }
        else
            State = Define.State.Idle;
    }

    void FootL() { }
    void FootR() { }

    protected override void UpdateDie()
    {
        Debug.Log("Monster Update Die");
    }

    protected override void UpdateIdle()
    {
        Debug.Log("Monster Update Idle");
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        float distance = (player.transform.position - transform.position).magnitude;
        if (distance < _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        Debug.Log("Monster Update Moving");
        // Attack Monster
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance < _attackRange)
            {
                NavMeshAgent nma = gameObject.GetorAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        // Moving
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetorAddComponent<NavMeshAgent>();
            nma.SetDestination(_lockTarget.transform.position);
            nma.speed = _stat.MoveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 20);
        }
    }

    protected override void UpdateSkill()
    {
        Debug.Log("Monster Update Skill");
    }
}
