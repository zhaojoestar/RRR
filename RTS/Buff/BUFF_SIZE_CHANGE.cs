public class BUFF_SIZE_CHANGE : Buff
{
    float _draw;
    float draw = 1;//抽3次卡可解除

    new void Start()
    {
        base.Start();
        SetEvent(true);
    }

    void DrawToDestroy()
    {
        _draw++;
        if (_draw >= draw)
        {
            Destroy(this);
        }
    }

    void SetEvent(bool isAdd)
    {
        if (isAdd)
        {
            EventMgr.AddEventListener(ENUM_EVENT.DECK_CHANGE, DrawToDestroy);
        }
        else
        {
            EventMgr.RemoveEventListener(ENUM_EVENT.DECK_CHANGE, DrawToDestroy);
        }
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        SetEvent(false);
    }
}
