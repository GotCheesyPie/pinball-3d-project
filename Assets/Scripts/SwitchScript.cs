using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public Collider bola;
    public Material offMaterial;
    public Material onMaterial;
    public ScoreManager scoreManager;
    public AudioManager audioManager;
    public VFXManager vFXManager;
    public float score;
    private Renderer _renderer;
    private enum SwitchState{
        Off,
        On,
        Blink
    }
    private SwitchState state;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        Set(false);
        StartCoroutine(BlinkTimerStart(5));
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if(other == bola){
            Toggle();
            scoreManager.AddScore(score);
            audioManager.PlaySFXSwitch(other.transform.position);
            vFXManager.PlayVFXSwitch(other.transform.position);
        }
    }
    private void Set(bool active){
        if (active == true){
            state = SwitchState.On;
            _renderer.material = onMaterial;
            StopAllCoroutines();
        }
        else{
            state = SwitchState.Off;
            _renderer.material = offMaterial;
	        StartCoroutine(BlinkTimerStart(5));
        }
    }
    private IEnumerator Blink(int times){
        state = SwitchState.Blink;
        for (int i = 0; i < times; i++){
            _renderer.material = onMaterial;
            yield return new WaitForSeconds(0.5f);
            _renderer.material = offMaterial;
            yield return new WaitForSeconds(0.5f);
        }
        state = SwitchState.Off;
        StartCoroutine(BlinkTimerStart(5));
    }
    private void Toggle(){
        if (state == SwitchState.On){
            Set(false);
        }
        else {
            Set(true);
        }
    }
    private IEnumerator BlinkTimerStart(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Blink(2));
    }
}
