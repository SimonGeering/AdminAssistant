#pragma warning disable CA1707 // Identifiers should not contain underscores
using AutoMapper;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.WebAPI
{
    public class WebAPIMappingProfile_Should
    {
        private readonly IConfigurationProvider configuration;
        private readonly IMapper mapper;

        public WebAPIMappingProfile_Should()
        {
            this.configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            this.mapper = this.configuration.CreateMapper();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HaveValidConfiguration()
        {
            // Arrange

            // Act
            this.configuration.AssertConfigurationIsValid();

            // Assert
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(Currency), typeof(v1.CurrencyResponseDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = this.mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
