using Enemy;
using UnityEngine.UI;

class NotDetectedState : DetectableState
{
    public NotDetectedState(Image image, ILevelEventHandler levelEventHandler, EnemyMovement enemyMovement) : base(image, levelEventHandler, enemyMovement)
    {
        // State = StateEnum.NotDetectedState;
        ValueIndicate = 0;
    }

    public override bool ChangeState(Detectable detectable, float value)
    {
        if (value >= 0)
        {
            detectable.State = new DetectedProcessesState(Image, LevelEventHandler, EnemyMovement);
        }
        else
        {
            ValueIndicate = 0;
        }

        return true;
    }
}