using System;

namespace EnovationAssignment.Exceptions
{
	public class InvalidInputException: Exception
	{
		private readonly string _paramName;
		private readonly string _value;

		public InvalidInputException(string paramName, string value)
		{
			this._paramName = paramName;
			this._value = value;
		}

		public override string Message => $"'{_value}' is not a valid value for '{_paramName}'";
	}
}
