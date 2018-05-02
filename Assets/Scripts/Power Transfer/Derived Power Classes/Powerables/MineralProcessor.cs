﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is for the mineral processor, a magical device which turns minerals into gold. I mean power.
/// The script is applied to the (you guessed it) mineral processor.
/// </summary>

public class MineralProcessor : PowerableObject
{
    [SerializeField, Tooltip("The minerals which can make power.")]
    private Collider minerals;

    [SerializeField, Tooltip("This is the power-on sound, for the child power switch.")]
    private AudioClip onClip;

    private AudioSource myAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        // If minerals come near, gobble them up and make power out of them.
        if (other == minerals)
        {
            ProcessMinerals();
            Invoke("DestroyMinerals", .1f);
        }
    }

    /// <summary>
    /// Allow power transfer and play sound.
    /// </summary>
    private void ProcessMinerals()
    {
        if (!IsFullyPowered)
        {
            // Now that the minerals have been destroyed, generate power from their disintegration.
            base.PowerOn();

            PowerSwitch powerSwitch = GetComponentInChildren<PowerSwitch>();

            if (powerSwitch != null)
            {
                powerSwitch.UnblockPowerSwitch();
            }

            myAudioSource = GetComponentInChildren<AudioSource>();

            if (myAudioSource != null)
            {
                myAudioSource.clip = onClip;
                myAudioSource.Play();
            }
        }
    }

    /// <summary>
    /// The minerals aren't destroying themselves properly, so we do it for them.
    /// </summary>
    private void DestroyMinerals()
    {
        Destroy(minerals.gameObject);
    }
}
