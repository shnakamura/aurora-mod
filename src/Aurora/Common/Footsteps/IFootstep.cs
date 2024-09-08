namespace Aurora.Common;

public interface IFootstep
{
	/// <summary>
	///		The file extension associated with this data type.
	/// </summary>
	public const string Extension = ".ambience.footstep";
	
	/// <summary>
	///		The sound data of this footstep.
	/// </summary>
    FootstepSoundData SoundData { get; set; }

	/// <summary>
	///		The material of this footstep.
	/// </summary>
    string Material { get; set; }
}
