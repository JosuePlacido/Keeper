using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;

namespace Keeper.Domain.Core
{
	public abstract class Entity
	{
		public string Id { get; protected set; }

		protected Entity(string id)
		{
			Id = id;
		}
		protected Entity()
		{
		}
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Entity))
				return false;
			if (Object.ReferenceEquals(this, obj))
				return true;
			if (this.GetType() != obj.GetType())
				return false;
			Entity item = (Entity)obj;
			return item.Id == this.Id;
		}
		[JsonIgnore]
		private List<INotification> _domainEvents;
		public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

		public void AddDomainEvent(INotification eventItem)
		{
			_domainEvents = _domainEvents ?? new List<INotification>();
			_domainEvents.Add(eventItem);
		}

		public void RemoveDomainEvent(INotification eventItem)
		{
			_domainEvents?.Remove(eventItem);
		}
		public void ClearDomainEvents()
		{
			_domainEvents?.Clear();
		}

		public override int GetHashCode()
		{
			return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
		}
		public static bool operator ==(Entity left, Entity right)
		{
			if (Object.Equals(left, null))
				return (Object.Equals(right, null));
			else
				return left.Equals(right);
		}
		public static bool operator !=(Entity left, Entity right)
		{
			return !(left == right);
		}
		public override string ToString()
		{
			return $"{GetType().Name} {Id}";
		}
	}
}
