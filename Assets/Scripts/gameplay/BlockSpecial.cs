using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockSpecial : Block {
    [SerializeField] Sprite freezeSprite, speedSprite;
    SpecialEffectEnum blockSpecialEffect;
    float freezeDuration;
    float speedDuration;
    float speedFactor;
    FreezerEffectActivated freezeEffectAct;
    //SpeedupEffectActivated speedEffectAct;

    public override void Initialize() {
        scoreCost = ConfigurationUtils.ScoreForSpecialBlock;
        freezeDuration = ConfigurationUtils.FreezeTime;
        speedDuration = ConfigurationUtils.SpeedTime;
        speedFactor = ConfigurationUtils.SpeedFactor;

        events.Add(EventsEnum.SpeedUp, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.SpeedUp, this);
        events.Add(EventsEnum.Freeze, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.Freeze, this);
        base.Initialize();
    }
    public void Activate() {
        switch (blockSpecialEffect) {
            case SpecialEffectEnum.Freezer:
                GetComponent<SpriteRenderer>().sprite = freezeSprite;
                //freezeEffectAct = new FreezerEffectActivated();
                //EventManager.AddFreezeInvoker(this);
                break;
            case SpecialEffectEnum.Speedup:
                GetComponent<SpriteRenderer>().sprite = speedSprite;
                //speedEffectAct = new SpeedupEffectActivated();
                //EventManager.AddSpeedInvoker(this);
                break;
        }
    }

    public SpecialEffectEnum BlockPickupEffect {
        set { blockSpecialEffect = value; }
    }
    //public void AddFreezerEffectListener(UnityAction<float> fooListener) {
    //    freezeEffectAct.AddListener(fooListener);
    //}
    //public void AddSpeedEffectListener(UnityAction<float, float> fooListener) {
    //    speedEffectAct.AddListener(fooListener);
    //}
    protected override void OnCollisionEnter2D(Collision2D collision) {
        switch (blockSpecialEffect) {
            case SpecialEffectEnum.Freezer:
                //freezeEffectAct.Invoke(freezeDuration);
                events[EventsEnum.Freeze].Invoke((int)freezeDuration);
                break;
            case SpecialEffectEnum.Speedup:
                //speedEffectAct.Invoke(speedDuration, speedFactor);
                events[EventsEnum.SpeedUp].Invoke((int)speedDuration);
                break;
        }
        base.OnCollisionEnter2D(collision);
    }
}
