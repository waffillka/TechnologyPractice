using System;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using TechnologyPractice.Controllers;
using Xunit;

namespace TestTechnologyPractice.API
{
    public class OrganizationControllerTests
    {
        [Fact]
        public async Task GetOrganization_WithUnexistingId_ReturnNotFound()
        {
            //Arrange
            var repository = new Mock<IRepositoryManager>();
            repository.Setup(repo => repo.Organizations.GetOrganizationAsync(It.IsAny<Guid>(), new OrganizationParameters(), false))
                .ReturnsAsync((Organization)null);

            var logger = new Mock<ILoggerManager>();
            var mapper = new Mock<IMapper>();

            var controller = new OrganizationController(logger.Object, repository.Object, mapper.Object);
            var parameters = new OrganizationParameters() { PageNumber = 1, PageSize = 10, SearchTerm = "" };
            //Act
            var result = await controller.GetOrganization(Guid.NewGuid(), parameters);

            //Assert
            
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
