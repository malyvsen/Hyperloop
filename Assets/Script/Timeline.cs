using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



[System.Serializable]
public class Timeline
{
    public List<Action> actions = new List<Action>();


    public Action currentAction
    {
        get
        {
            if (!unexecutedActions.Any())
            {
                return null;
            }

            Action candidate = unexecutedActions.First();
            if (Time.time >= candidate.time)
            {
                return candidate;
            }

            return null;
        }
    }


    public IEnumerable<Action> unexecutedActions
    {
        get
        {
            return actions.Where(x => !x.executed);
        }
    }


    public void AddCurrentAction(Action.Type type)
    {
        actions.Insert(0, new Action() { time = Time.time, type = type, executed = false });
    }
}
