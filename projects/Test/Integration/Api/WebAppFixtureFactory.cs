using System;
using Xunit;
namespace Keeper.Test.Integration.Api
{

	[CollectionDefinition("WebApi Collection")]
	public sealed class CustomWebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactoryFixture>
	{
	}
	public sealed class CustomWebApplicationFactoryFixture : IDisposable
	{
		public CustomWebApplicationFactoryFixture() =>
			this.CustomWebApplicationFactory = new CustomWebApplicationFactory();
		public CustomWebApplicationFactory CustomWebApplicationFactory { get; }

		public void Dispose() => this.CustomWebApplicationFactory?.Dispose();
	}
}
