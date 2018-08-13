using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SoundGroup
{
    public string Name;
    public AudioClip[] Clips;

    public AudioClip GetRandom()
    {
        return Clips[Random.Range(0, Clips.Length)];
    }
}

[RequireComponent(typeof(AudioSource))]
public class SoundShotPlayer : MonoBehaviour
{
    public AudioSource VoiceSource;
    public AudioSource SoundSource;

    public List<SoundGroup> SoundGroups;
    public bool IsPlayingVoice { get { return VoiceSource.isPlaying; } }

    public void PlayVoice(string voice)
    {
        var selectedGroup = GetGroup(voice);

        if (selectedGroup == null || selectedGroup.Clips.Length == 0)
            return;

        VoiceSource.PlayOneShot(selectedGroup.GetRandom());
    }

    public void PlaySound(string sound)
    {
        var selectedGroup = GetGroup(sound);

        if (selectedGroup == null || selectedGroup.Clips.Length == 0)
            return;

        SoundSource.PlayOneShot(selectedGroup.GetRandom());
    }

    private SoundGroup GetGroup(string name)
    {
        foreach (var soundGroup in SoundGroups)
            if (soundGroup.Name == name)
                return soundGroup;

        return null;
    }
}
