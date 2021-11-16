using Enemy;
using UnityEngine.UI;


public abstract class DetectableState
{
    // protected enum StateEnum
    // {
    //     DetectedState,
    //     DetectionProcessesState,
    //     NotDetectedState
    // }
    //
    // protected StateEnum State = StateEnum.NotDetectedState;
    protected Image Image { set; get; }

    protected ILevelEventHandler LevelEventHandler;

    protected EnemyMovement EnemyMovement;

    public float ValueIndicate
    {
        get => Image.fillAmount;
        set => Image.fillAmount = value;
    }

    public DetectableState(Image image, ILevelEventHandler levelEventHandler, EnemyMovement enemyMovement)
    {
        Image = image;
        LevelEventHandler = levelEventHandler;
        EnemyMovement = enemyMovement;
    }

    public virtual bool ChangeState(Detectable detectable, float value) => true;
}