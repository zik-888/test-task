using Enemy;
using UnityEngine.UI;

class NotDetectedState : DetectableState
{
    public NotDetectedState(Image image, ILevelController levelController, EnemyMovement enemyMovement) : base(image, levelController, enemyMovement)
    {
        // State = StateEnum.NotDetectedState;
        ValueIndicate = 0;
    }

    public override bool ChangeState(Detectable detectable, float value)
    {
        if (value >= 0)
        {
            detectable.State = new DetectedProcessesState(Image, LevelController, EnemyMovement);
        }
        else
        {
            ValueIndicate = 0;
        }

        return true;
    }
}