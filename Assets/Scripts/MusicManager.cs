using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    public static MusicManager instance;
    public static string currScene = "";
    public static AudioSource currBGM = null;

    //bool playTheme;

    public float ogPitch = 1;

    void Awake()
    {
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        print(sounds.Length);
        print(sounds[0].clip);

        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.group;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    
    void FixedUpdate(){
        BGM();
    }

    public void speedUp(){
        if(PublicVars.playTheme)
            currBGM.pitch += .01f;
        }
    public void revert(){
        if(PublicVars.playTheme)
            currBGM.pitch = ogPitch;
    }
   
    
    void BGM(){
        string sceneName = SceneManager.GetActiveScene().name;

        if(PublicVars.playTheme){
            if(sceneName != currScene){
                if(sceneName == "SampleScene"){
                    
                    if(currBGM != null){
                        currBGM.Stop();
                    }

                    

                    currBGM = PlayBGM("MainTheme");
                    ogPitch = currBGM.pitch;


                    //currBGM = StopBGM("MainTheme");
                    
                }
                currScene = sceneName;
            }
            }
        else{
            if(currBGM != null)
            {
                currBGM.Stop();
                currBGM = null;
                currScene = "";
            }

        }
        
    } 

    

    public void PlaySoundEffects(string name){
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }
    public AudioSource PlayBGM(string name){
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
       

        s.source.Play();
        return s.source;
    }

    public void StopBGM()
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);


        s.source.Stop();
        
    }

    
}
