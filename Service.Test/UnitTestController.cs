using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PairSoftAPI.Controllers;
using PairSoftAPI.Models;
using PairSoftAPI.ServiceRepository;

namespace Service.Test
{
    public class UnitTestController
    {
        private ToDoListRepository repository;
        public static DbContextOptions<PairSoftDbContext> dbContextOptions { get; }
        public static string connectionString = "server=localhost;database=PairSoftDB;Trusted_Connection=True;TrustServerCertificate=True;";

        static UnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<PairSoftDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public UnitTestController()
        {
            var context = new PairSoftDbContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            repository = new ToDoListRepository(context);

        }

        #region Get By Id

        [Fact]
        public async void Task_GetToDoListById_Return_OkResult()
        {
            //Arrange
            var controller = new ToDoController(repository);
            var Id = 2;

            //Act
            var data = await controller.GetListById(Id);
            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetToDoListById_Return_NotFoundResult()
        {
            //Arrange
            var controller = new ToDoController(repository);
            var Id = 3;

            //Act
            var data = await controller.GetListById(Id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion

        #region Get All

        [Fact]
        public async void Task_GetToDoList_Return_OkResult()
        {
            //Arrange
            var controller = new ToDoController(repository);
            //Act
            var data = await controller.GetToDoList();

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }


        #endregion

        #region Add New List

        [Fact]
        public async void Task_AddToDoList_ValidData_Return_OkResult()
        {
            //Arrange
            var controller = new ToDoController(repository);
            var list = new ToDoList() { Title = "Test Title 3", Description = "Test Description 3", Id = 111, DueDate = DateTime.Now.Date, IsCompleted = true };

            //Act
            var data = await controller.InsertList(list);

            //Assert
            Assert.IsType<ObjectResult>(data);
        }

        [Fact]
        public async void Task_AddToDoList_InvalidData_Return_BadRequest()
        {
            //Arrange
            var controller = new ToDoController(repository);
            ToDoList list = null;

            //Act            
            var data = await controller.InsertList(list);

            //Assert
            Assert.IsType<BadRequestResult>(data);
        }

        #endregion
        #region Update Existing List

        [Fact]
        public async void Task_UpdateToDoList_ValidData_Return_OkResult()
        {
            //Arrange
            var controller = new ToDoController(repository);
            var Id = 2;

            //Act
            var existingPost = await controller.GetListById(Id);
            var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<ToDoList>().Subject;

            var list = new ToDoList();
            list.Title = "Test Title 2 Updated";
            list.Description = result.Description;
            list.Id = result.Id;
            list.DueDate = result.DueDate;
            list.IsCompleted = result.IsCompleted;

            var updatedData = await controller.UpdateList(list);

            //Assert
            Assert.IsType<OkObjectResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateToDoList_InvalidData_Return_NotFound()
        {
            //Arrange
            var controller = new ToDoController(repository);
            var Id = 200;

            //Act
            var lst = new ToDoList();
            lst.Title = "Test Title More Than 20 Characteres";
            lst.Description = "Description test";
            lst.Id = Id;
            lst.DueDate = DateTime.Now;
            lst.IsCompleted = true;

            var data = await controller.UpdateList(lst);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion

        #region Delete Post

        [Fact]
        public async void Task_DeleteToDoList_Return_OkResult()
        {
            //Arrange
            var controller = new ToDoController(repository);
            var Id = 2;

            //Act
            var data = await controller.DeleteList(Id);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_DeleteToDoList_Return_NotFoundResult()
        {
            //Arrange
            var controller = new ToDoController(repository);
            var Id = 5;

            //Act
            var data = await controller.DeleteList(Id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(data);
        }


        #endregion

    }
}
