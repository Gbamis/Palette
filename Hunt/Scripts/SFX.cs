using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace HT
{
    public class SFX : MonoBehaviour
    {
        [Inject] private AppEvent appEvent;

        private static SFX m_instance;
        public static SFX Core { set => m_instance = value; get => m_instance; }
        public List<AudioSource> ui_audioSources;
        public List<AudioSource> gameplay_audioSources;

        public List<AudioClip> game_theme_clips;
        public AudioSource gamethemeSource;

        private void OnEnable()
        {
            appEvent.OnGameStarted += PlayNextTheme;
        }
        private void OnDisable()
        {
            appEvent.OnGameStarted -= PlayNextTheme;
        }

        private void Awake() => Core = this;


        public void ButtonHover() => ui_audioSources[0].Play();
        public void ButtonClicked() => ui_audioSources[1].Play();
        public void ButtonVanish() => ui_audioSources[2].Play();


        public void ObjectHover() => gameplay_audioSources[0].Play();
        public void ObjectSelect() => gameplay_audioSources[1].Play();
        public void ObjectUnDeselect() => gameplay_audioSources[2].Play();
        public void RangeShow() => gameplay_audioSources[3].Play();
        public void TreeFall(Vector3 pos)
        {
            gameplay_audioSources[4].transform.position = pos;
            gameplay_audioSources[4].Play();
        }

        public void CoinAdded() => gameplay_audioSources[6].Play();

        public void GoatBleat(Vector3 pos)
        {
            gameplay_audioSources[7].transform.position = pos;
            gameplay_audioSources[7].Play();
        }
        public void Play_FemaleGoatHovered() => gameplay_audioSources[8].Play();
        public void Play_GoatDestroyed() => gameplay_audioSources[9].Play();
        public void Play_GoatInfoDisplayed() => gameplay_audioSources[10].Play();

        public void PlayNextTheme()
        {
            int index = Random.Range(0, game_theme_clips.Count);
            gamethemeSource.clip = game_theme_clips[index];
            gamethemeSource.Play();
            EaseVolume(gamethemeSource, 0.4f).Forget();
        }

        private async UniTaskVoid EaseVolume(AudioSource source, float maxVolume)
        {
            gamethemeSource.volume = 0;
            float value = 0;
            while (value < maxVolume)
            {
                value += Time.deltaTime * 0.04f;
                source.volume = value;
                await UniTask.Yield();
            }
        }
    }

}