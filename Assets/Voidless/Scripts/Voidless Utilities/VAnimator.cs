using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless
{
public static class VAnimator
{
	/// <summary>Sets IK position of this Joint if it is one.</summary>
	/// <param name="_animator">Animator to Extend.</param>
	/// <param name="_IKGoal">IK Goal to displace.</param>
	/// <param name="_position">Position.</param>
	/// <param name="_weight">Position's Weight [1.0f by default].</param>
	public static void SetIKPosition(this Animator _animator, AvatarIKGoal _IKGoal, Vector3 _position, float _weight = 1.0f)
	{
		_animator.SetIKPosition(_IKGoal, _position);
        _animator.SetIKPositionWeight(_IKGoal, _weight);
	}

	/// <summary>Creates a string that shows all the Animator State's Info.</summary>
	/// <param name="_info">Animator State's Info.</param>
	/// <returns>Animator State Info into a string.</returns>
	public static string StateInfoToString(this AnimatorStateInfo _info)
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("Animator State Info: ");
		builder.AppendLine();
		builder.Append("Full Path Hash: ");
		builder.AppendLine(_info.fullPathHash.ToString());
		builder.Append("Length (Duration): ");
		builder.AppendLine(_info.length.ToString());
		builder.Append("Looping? ");
		builder.AppendLine(_info.loop.ToString());
		builder.Append("Normalized Time: ");
		builder.AppendLine(_info.normalizedTime.ToString());
		builder.Append("Short Name Hash: ");
		builder.AppendLine(_info.shortNameHash.ToString());
		builder.Append("Playback Speed: ");
		builder.AppendLine(_info.speed.ToString());
		builder.Append("Speed Multiplier: ");
		builder.AppendLine(_info.speedMultiplier.ToString());
		builder.Append("Tag Hash: ");
		builder.Append(_info.tagHash.ToString());

		return builder.ToString();
	}
}
}