using System;
using Xunit;
using StudentManagment.Controllers;
using StudentManagment.Services;
using StudentManagment.Interfaces;
using StudentManagment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagmentTests
{

    public class StudentControllerTests
    {

        StudentsController _controller;
        IStudentService _service;

        public StudentControllerTests()
        {
            _service = new StudentServiceFake();
            _controller = new StudentsController(_service);
        }

        #region getStudents Tests methods
        [Fact]
        public void GetStudents_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetStudents();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetStudents_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetStudents().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Student>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        #endregion

        #region getStudent tests methods
        [Fact]
        public void GetStudent_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetStudent(1);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetStudent_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1001;

            // Act
            var okResult = _controller.GetStudent(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetStudent_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1001;

            // Act
            var okResult = _controller.GetStudent(testId).Result as OkObjectResult;

            // Assert
            Assert.IsType<Student>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as Student).Id);
        }
        #endregion;

        #region PostStudent tests methods
        [Fact]
        public void PostStudent_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Student();

            this._controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.PostStudent(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void PostStudent_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Student testItem = new Student()
            {
                Name = "Thierry POUTONG"
            };

            // Act
            var createdResponse = _controller.PostStudent(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void PostStudent_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Student()
            {
                Name = "Thierry POUTONG"
            };

            // Act
            var createdResponse = _controller.PostStudent(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Student;

            // Assert
            Assert.IsType<Student>(item);
            Assert.Equal("Thierry POUTONG", item.Name);
        }
        #endregion;

        #region PutStudent tests methods
        [Fact]
        public void PutStudent_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 1;

            // Act
            var badResponse = _controller.PutStudent(notExistingId, new Student());

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void PutStudent_ExistingIdAndInvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var existingId = 1001;
            var nameMissingItem = new Student();

            this._controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.PutStudent(existingId, nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void PutStudent_ExistingIdAndValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var existingId = 1001;

            Student testItem = new Student()
            {
                Name = "AMZA MOHAMED UPDATED"
            };

            // Act
            var createdResponse = _controller.PutStudent(existingId,testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void PutStudent_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var existingId = 1001;

            Student testItem = new Student()
            {
                Name = "AMZA MOHAMED UPDATED"
            };

            // Act
            var createdResponse = _controller.PutStudent(existingId, testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Student;

            // Assert
            Assert.IsType<Student>(item);
            Assert.Equal("AMZA MOHAMED UPDATED", item.Name);
        }
        #endregion;

        #region DeleteStudent tests methods
        [Fact]
        public void DeleteStudent_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 1;

            // Act
            var badResponse = _controller.DeleteStudent(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void DeleteStudent_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var existingId = 1001;

            // Act
            var okResponse = _controller.DeleteStudent(existingId);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        [Fact]
        public void DeleteStudent_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1001;

            // Act
            var okResponse = _controller.DeleteStudent(existingId);

            // Assert
            Assert.Equal(2,_service.GetAllItems().Count());
        }
        #endregion;

    }



}

