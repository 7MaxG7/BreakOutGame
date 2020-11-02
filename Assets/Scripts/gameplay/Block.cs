using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : TEventInvoker<int> {
    protected static int scoreCost;
    //PointsAddedEvent blockDestroyedEvent;
    //AllBlocksDestroyedEvent levelClearedEvent;

    public virtual void Initialize() {
        events.Add(EventsEnum.BlockDestroyed, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.BlockDestroyed, this);
        //blockDestroyedEvent = new PointsAddedEvent();
        //EventManager.AddPointsAddedInvoker(this);
        //levelClearedEvent = new AllBlocksDestroyedEvent();
        //EventManager.AddLevelClearedInvoker(this);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        ObjectsPool.ReturnObjToPool(gameObject);
        events[EventsEnum.BlockDestroyed].Invoke(scoreCost);
        //Destroy(gameObject);
        //blockDestroyedEvent.Invoke(scoreCost);
        //if (GameObject.FindGameObjectsWithTag("Block").Length == 1) {
        //    levelClearedEvent.Invoke(Hud.Score);
        //}
        AudioManager.Play(AudioClipEnum.BlockBreak);
    }
    //public void AddPointsAddedListener(UnityAction<int> fooListener) {
    //    blockDestroyedEvent.AddListener(fooListener);
    //}
    //public void AddLevelClearedListener(UnityAction<int> fooListener) {
    //    levelClearedEvent.AddListener(fooListener);
    //}
}
