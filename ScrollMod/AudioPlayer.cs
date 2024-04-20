using UnityEngine;
using Services;

namespace ScrollMod;

public class AudioPlayer {
	public static void PlaySound(string sound)
	{
		try
		{
			Service.Home.AudioService.PlaySound(sound, false);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}
}
