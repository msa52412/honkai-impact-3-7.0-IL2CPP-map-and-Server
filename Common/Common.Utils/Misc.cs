using System;

namespace Common.Utils;

public static class Misc
{
	public static byte[] StringToByteArray(string hex)
	{
		if (hex.Length % 2 == 1)
		{
			throw new Exception("The binary key cannot have an odd number of digits");
		}
		byte[] array = new byte[hex.Length >> 1];
		for (int i = 0; i < hex.Length >> 1; i++)
		{
			array[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));
		}
		return array;
	}

	public static int GetHexVal(char hex)
	{
		return hex - ((hex < ':') ? 48 : ((hex < 'a') ? 55 : 87));
	}
}
