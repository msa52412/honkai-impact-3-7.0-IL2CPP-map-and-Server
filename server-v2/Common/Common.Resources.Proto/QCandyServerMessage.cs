using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__HasShowTrialLevelDialog;

	[ProtoMember(1, Name = "has_show_trial_level_dialog")]
	public bool HasShowTrialLevelDialog
	{
		get
		{
			return __pbn__HasShowTrialLevelDialog.GetValueOrDefault();
		}
		set
		{
			__pbn__HasShowTrialLevelDialog = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHasShowTrialLevelDialog()
	{
		return __pbn__HasShowTrialLevelDialog.HasValue;
	}

	public void ResetHasShowTrialLevelDialog()
	{
		__pbn__HasShowTrialLevelDialog = null;
	}
}
