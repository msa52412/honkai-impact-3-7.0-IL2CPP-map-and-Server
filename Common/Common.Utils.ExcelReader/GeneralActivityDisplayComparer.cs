using System.Collections.Generic;

namespace Common.Utils.ExcelReader;

public class GeneralActivityDisplayComparer : IEqualityComparer<GeneralActivityExcel>
{
	public bool Equals(GeneralActivityExcel x, GeneralActivityExcel y)
	{
		return x.DisplayData == y.DisplayData;
	}

	public int GetHashCode(GeneralActivityExcel obj)
	{
		return obj.DisplayData;
	}
}
