using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TargetSpawner : MonoBehaviour
{
    private List<GameObject> targets;
    private int range;
    private int intensities=4;

    private GameObject Player;

    void Start()
    {        
        HitManager.SharedInstance.SetTargetSpawner(this);
        Player=GameObject.FindGameObjectWithTag("Player");

        targets=new List<GameObject>();
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
            targets.Add(transform.GetChild(i).gameObject);

        range=targets.Count;

        SpawnRandomTarget();
    }

    int lastId=-1;
    int lastIntensity=-1;

    private void DisableAllTargets(){
         foreach(var target in targets)
            target.SetActive(false);
    }

    public void SpawnAfterDelay(float delay){
        if(gameObject.activeSelf)
            StartCoroutine(SpawnAfterDelayCoroutine(delay));
    }
    
    public IEnumerator SpawnAfterDelayCoroutine(float delay){
        DisableAllTargets(); 
        yield return new WaitForSeconds(delay);
        SpawnRandomTarget();
    }

    public void SpawnRandomTarget(){     
        DisableAllTargets(); 
        var id=RandomRangeExcept(0,range,lastId);
        var target=targets[id];

        ConfigTarget(target);
        target.SetActive(true);
        lastId=id;
    }

    private void ConfigTarget(GameObject target){
        var targetInfo=target.GetComponent<TargetInfo>();
        var intensity=RandomRangeExcept(0,intensities,lastIntensity);
        targetInfo.SetIntensity(intensity);
        lastIntensity=intensity;
    }

    public GameObject GetActiveTarget(){
        foreach(var target in targets){
            if(target.activeSelf) 
                return target;
        }

        return null;       
    }

    int RandomRangeExcept(int min, int max, int except=-1)  {
        var exclude = new HashSet<int>() { except };
        var range = Enumerable.Range(min, max).Where(i => !exclude.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(0, (max) - exclude.Count);
        return range.ElementAt(index);
    }

    // Update is called once per frame
}
