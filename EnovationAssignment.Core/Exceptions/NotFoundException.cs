using System;

namespace EnovationAssignment.Exceptions
{
	public class NotFoundException: Exception
	{
		private readonly string _entityName;
		private readonly int _id;

		public NotFoundException(string entityName, int id)
		{
			this._entityName = entityName;
			this._id = id;
		}

		public override string Message => $"{_entityName} with id '{_id}' was not found.";

	}
}
