using Enemy;
using UnityEngine;
using UnityEngine.UI;

class DetectedProcessesState : DetectableState
{
    public DetectedProcessesState(Image image, ILevelEventHandler levelEventHandler, EnemyMovement enemyMovement) : base(image, levelEventHandler, enemyMovement)
    {
    }
    
    public override bool ChangeState(Detectable detectable, float value)
    {
        if (value >= 1)
        {
            Debug.Log($"{EnemyMovement} - new DetectedState");
            detectable.State = new DetectedState(Image, LevelEventHandler, EnemyMovement);
            return false;
        }
        else if (value <= 0)
        {
            detectable.State = new NotDetectedState(Image, LevelEventHandler, EnemyMovement);
        }
        else
        {
            ValueIndicate = value;
        }

        return true;
    }
}