﻿using Microsoft.AspNetCore.Mvc;
using Pensatiu.API.Controllers;
using Pensatiu.Repository.Pacientes;
using Pensatiu.Services;
using Pensatiu.Services.Dto.Paciente;
using System;
using System.Linq;
using Xunit;
using System.Collections.Generic;

namespace Pensatiu.Test
{
    public class PacienteTest : IClassFixture<StartupFixture>
    {
        private const int ID_FOR_GET = 1;
        private const int ID_FOR_UPDATE = 2;
        private const int ID_FOR_DELETE = 3;

        private PacientesController GetNewControllerInstance()
        {
            var dbContext = UnitTestHelpers.GetNewInMemoryDbContextInstance();
            var sqlData = new SqlPacienteData(dbContext);
            var service = new PacienteService(sqlData);
            return new PacientesController(service, null);
        }

        [Fact]
        public void Get_WithError()
        {
            var controller = GetNewControllerInstance();

            //not found
            var notFoundResult = controller.Get(int.MaxValue);
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void Get_Ok()
        {
            var controller = GetNewControllerInstance();
            var okResult = controller.Get(ID_FOR_GET) as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<PacienteDto>(okResult.Value);
            Assert.Equal(ID_FOR_GET, (okResult.Value as PacienteDto).Id);
        }

        [Fact]
        public void GetAll_Ok()
        {
            var controller = GetNewControllerInstance();
            var okResult = controller.GetAll() as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsAssignableFrom<IEnumerable<PacienteDto>>(okResult.Value);
            Assert.True((okResult.Value as IEnumerable<PacienteDto>).Where(x => x.Id > 0).Count() > 0);
        }

        [Fact]
        public void Create_WithError()
        {
            var controller = GetNewControllerInstance();
            var badRequestResponse = controller.Create(null);
            Assert.IsType<BadRequestResult>(badRequestResponse);
        }
        
        [Fact]
        public void Create_Ok()
        {
            var controller = GetNewControllerInstance();
            var resourceToCreate = new PacienteForCreateDto { Nome = $"Novo { DateTime.UtcNow.ToString() }" };
            var createdResponse = controller.Create(resourceToCreate) as CreatedAtRouteResult;
            var createdResource = createdResponse.Value as PacienteDto;
            Assert.IsType<CreatedAtRouteResult>(createdResponse);
            Assert.True(createdResource.Id > 0);
            Assert.Equal(createdResource.Nome, resourceToCreate.Nome);
        }
        /*
        [Fact]
        public void Update_Ok()
        {
            var resourceToUpdate = new ConsultorioForUpdateDto($"Alterado { DateTime.UtcNow.ToString() }", "#DADADA", TipoConsultorioEnumDto.AluguelMensal);
            var createdResponse = _controller.Update(ID_FOR_UPDATE, resourceToUpdate);
            Assert.IsType<NoContentResult>(createdResponse);
        }

        [Fact]
        public void Update_WithError()
        {
            var resourceToUpdate = new ConsultorioForUpdateDto($"Alterado { DateTime.UtcNow.ToString() }", "#DADADA", TipoConsultorioEnumDto.AluguelMensal);

            //not found
            var notFoundResult = _controller.Update(int.MaxValue, resourceToUpdate);
            Assert.IsType<NotFoundResult>(notFoundResult);

            //bad request due to Id
            var badRequestId = _controller.Update(0, resourceToUpdate);
            Assert.IsType<BadRequestResult>(badRequestId);

            //bad requests due to model
            var badRequestModel = _controller.Update(ID_FOR_UPDATE, null);
            Assert.IsType<BadRequestResult>(badRequestModel);
        }

        [Fact]
        public void Delete_WithError()
        {
            //not found
            var notFoundResult = _controller.Delete(int.MaxValue);
            Assert.IsType<NotFoundResult>(notFoundResult);

            //bad request due to id
            var badRequestId = _controller.Delete(0);
            Assert.IsType<BadRequestResult>(badRequestId);
        }

        [Fact]
        public void Delete_Ok()
        {
            var noContentResult = _controller.Delete(ID_FOR_DELETE);
            Assert.IsType<NoContentResult>(noContentResult);
            var notFoundResult = _controller.Get(ID_FOR_DELETE); //Try to find
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
       */
    }
}
