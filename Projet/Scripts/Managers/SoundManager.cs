using Godot;
using Com.IsartDigital.Hackaton.Libraries;
using System;
using System.Diagnostics;

namespace Com.IsartDigital.Hackaton
{
	public partial class SoundManager : Node
	{
        [Export] private AudioStreamPlayer musicPlayer;
        [Export] private AudioStreamPlayer ambiencePlayer;

        public static float musicVolume = 0f;
        public static float sfxVolume = 0f;

        #region Singleton
        static private SoundManager instance;

		private SoundManager() { }

		static public SoundManager GetInstance()
		{
			if (instance == null) instance = new SoundManager();
			return instance;

		}
		#endregion

		public override void _Ready()
		{
			#region Singleton Ready
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(SoundManager) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
			#endregion


		}

        public void PlaySound(SFX pSFX)
        {
            string lSFXName = pSFX.ToString();
            AudioStreamOggVorbis lSound = GD.Load<AudioStreamOggVorbis>("Assets/Audio/SFX/" + lSFXName + ".ogg");
            lSound.Loop = false;

            if (lSound != null)
            {
                AudioStreamPlayer lPlayer = new AudioStreamPlayer();
                lPlayer.VolumeDb = sfxVolume;

                lPlayer.Stream = lSound;
                lPlayer.Finished += lPlayer.QueueFree;
                if (GetTree().Paused) lPlayer.ProcessMode = ProcessModeEnum.Always;

                AddChild(lPlayer);
                lPlayer.Play();
            }
        }

        public void PlayMusic(Tracks pTrack, bool pLoops = true)
        {
            string lMusicName = pTrack.ToString();
            AudioStreamOggVorbis lSong = GD.Load<AudioStreamOggVorbis>("Assets/Audio/Tracks/" + lMusicName + ".ogg");
            lSong.Loop = pLoops;

            if (lSong != null)
            {
                musicPlayer.Stream = lSong;
                musicPlayer.Play();
            }
        }

        public void PlayAmbience()
        {
            string lMusicName = Tracks.ambience.ToString();
            AudioStreamOggVorbis lSong = GD.Load<AudioStreamOggVorbis>("Assets/Audio/Tracks/" + lMusicName + ".ogg");
            lSong.Loop = true;

            ambiencePlayer.Play();
        }

        public void StopMusic()
        {
            musicPlayer.Stop();
        }

        public void StopAmbience()
        {
            ambiencePlayer.Stop();
        }

        public void UpdateVolume()
        {
            musicPlayer.VolumeDb = musicVolume;
        }

        protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
