using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardLaboral.Repository.UnitTest
{
    public class Tests
    {
        private IRepositoryEmpresa repository;
        private const string ConnectionString = @"Data Source=LAP0301TRD021\SQLEXPRESS;Initial Catalog=DashboardLaboral;User ID=sa;Password=Pa$$w0rd01.";

        public Tests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<insitedb>(options => options.UseSqlServer(ConnectionString));
            services.AddRepositories();

            repository = services.BuildServiceProvider().GetService<IRepositoryEmpresa>();

        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [Order(1)]
        public async Task AddAsync_Ok()
        {
            var result = await repository.AddAsync(new Empresa
            {
                CodigoEmpresa = "DEMOEMP",
                Empresa1 = "Empresa Demostracion",
                Color = ""
            });

            result.Should().Be(1);
        }

        [Test]
        [Order(2)]
        public async Task FindAsync_Ok()
        {

            var result = await repository.FindAsync("DEMOEMP");
            result.Should().NotBeNull();

        }

        [Test]
        [Order(3)]
        public async Task UpdateAsync_Ok()
        {

            var entity = new Empresa
            {
                CodigoEmpresa = "DEMOEMP",
                Empresa1 = "Empresa Demostracion",
                Color = "COLOR"
            };

            var result = await repository.UpdateAsync(entity);

            result.Should().Be(1);

        }

        [Test]
        [Order(4)]
        public async Task DeleteAsync_Ok()
        {

            var entity =  await repository.FindAsync("DEMOEMP");

            var result = await repository.DeleteAsync(entity);

            result.Should().Be(1);

        }

        [Test]
        [Order(5)]
        public async Task GetAllAsync_Ok()
        {

            var result = await repository.GetAllAsync();
            result.ToList().Should().HaveCountGreaterThan(1);

        }

    }
}