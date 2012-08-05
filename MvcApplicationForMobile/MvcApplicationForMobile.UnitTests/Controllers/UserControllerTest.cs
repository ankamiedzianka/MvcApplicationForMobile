using MvcApplicationForMobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using MvcApplicationForMobile.DAL;
using MvcApplicationForMobile.Models;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace MvcApplicationForMobile.UnitTests
{

    /// <summary>
    ///This is a test class for UserControllerTest and is intended
    ///to contain all UserControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserControllerTest
    {

        private TestContext testContextInstance;
        private IUserRepository userRepository;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test

        [TestInitialize()]
        public void MyTestInitialize()
        {
            //Mock repository creation
            Mock<IUserRepository> mock = new Mock<IUserRepository>();


            var users = new List<User>();
            users.Add(new User { UserID = 1, FirstName = "FirstName1", LastName = "LastName1", Email = "email1@email.com" });
            users.Add(new User { UserID = 2, FirstName = "FirstName2", LastName = "LastName2", Email = "email2@email.com", IsDeleted = true });
            users.Add(new User { UserID = 3, FirstName = "FirstName3", LastName = "LastName3", Email = "email3@email.com" });
            users.Add(new User { UserID = 4, FirstName = "FirstName4", LastName = "LastName4", Email = "email4@email.com" });

            mock.Setup(m => m.GetUsers()).Returns(users);

            mock.Setup(m => m.GetUserByID(1))
               .Returns(users[0]);

            userRepository = mock.Object;
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}

        #endregion


        /// <summary>
        ///A test for UserController Constructor
        ///</summary>
        [TestMethod]
        public void TestUserController_Constructor()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);
            Assert.IsNotNull(unitOfWork.AddressRepository, "Address repository can not be null.");
            Assert.IsNotNull(unitOfWork.UserRepository, "User repository can not be null.");
            Assert.IsNotNull(target, "Controller can not be null.");
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod]
        public void TestCreate_ReturnCreateView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            // Act
            ViewResult actual = target.Create();

            // Assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod]
        public void TestCreate_WhenNoError_InsertUserAndReturnIndexView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            var newUser = userRepository.GetUserByID(1);

            User user = new User()
            {
                Email = "email1@email.com",
                FirstName = "FirstName1",
                LastName = "LastName1"
            };

            // Act
            RedirectToRouteResult actual = target.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsTrue(user.UserID != 0);
            Assert.IsTrue(target.ModelState.IsValid);

            Assert.IsNotNull(user.DateAdded);
            Assert.IsNull(user.DateModified);
            Assert.IsNull(user.IsDeleted);

            Assert.AreEqual(user.FirstName, newUser.FirstName);
            Assert.AreEqual(user.LastName, newUser.LastName);
            Assert.AreEqual(user.Email, newUser.Email);

            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod]
        public void TestCreate_WhenModelError_EmptyStrings_ReturnCreateView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            User user = new User()
            {
                Email = "",
                FirstName = "",
                LastName = ""
            };

            // Act
            ViewResult actual = target.Create(user) as ViewResult;

            // Assert
            Assert.IsTrue(user.UserID == 0);
            Assert.IsFalse(target.ModelState.IsValid);
            Assert.AreEqual("Create", actual.ViewName);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod]
        public void TestCreate_WhenModelError_TooLongStrings_ReturnCreateView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            User user = new User()
            {
                Email = "longemailaddresslongemailaddresslongemailaddresslongemailaddresslongemailaddress@longemailaddress.com",
                FirstName = "longfirstnamelongfirstnamelongfirstnamelongfirstnamelongfirstname",
                LastName = "longlastnamelonglastnamelonglastnamelonglastnamelonglastnamelonglastname"
            };

            // Act
            ViewResult actual = target.Create(user) as ViewResult;

            // Assert
            Assert.IsFalse(target.ModelState.IsValid);
            Assert.IsTrue(user.UserID == 0);
            Assert.AreEqual("Create", actual.ViewName);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod]
        public void TestCreate_WhenModelError_InvalidEmail_ReturnCreateView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            User user = new User()
            {
                Email = "notvalid@email@email.com",
                FirstName = "FirstName1",
                LastName = "LastName1"
            };

            // Act
            ViewResult actual = target.Create(user) as ViewResult;

            // Assert
            Assert.IsFalse(target.ModelState.IsValid);
            Assert.IsTrue(user.UserID == 0);
            Assert.AreEqual("Create", actual.ViewName);
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod]
        public void TestIndex_WhenNoError_ReturnIndexView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);
            Nullable<bool> errorOccurred = null;


            // Act   
            ViewResult actual = target.Index(errorOccurred);

            // Assert
            Assert.AreEqual("Index", actual.ViewName);
            Assert.AreEqual(null, actual.ViewBag.ErrorMessage);

            //TODO: check number of records returned by model
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod]
        public void TestIndex_WhenError_ReturnIndexViewAndErrorMessage()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);
            Nullable<bool> errorOccurred = true;

            // Act
            ViewResult actual = target.Index(errorOccurred);

            // Assert
            Assert.AreEqual("Index", actual.ViewName);
            Assert.AreEqual("Error occurred.", actual.ViewBag.ErrorMessage);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenNoError_ReturnEditView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);
            Nullable<bool> errorOccurred = null;

            // Act
            ViewResult actual = target.Edit(1, errorOccurred) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenError_ReturnEditViewAndErrorMessage()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);
            Nullable<bool> errorOccurred = true;

            // Act
            ViewResult actual = target.Edit(1, errorOccurred) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", actual.ViewName);
            Assert.AreEqual("Error occurred.", actual.ViewBag.ErrorMessage);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenUserIDIsZero_ReturnIndexViewAndErrorMessage()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);
            Nullable<bool> errorOccurred = null;

            // Act
            RedirectToRouteResult actual = target.Edit(0, errorOccurred) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
            Assert.AreEqual(true, actual.RouteValues["errorOccurred"]);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenUserIsDeleted_AddModelErrorAndReturnEditView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);
            Nullable<bool> errorOccurred = null;

            // Act
            ViewResult actual = target.Edit(2, errorOccurred) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", actual.ViewName);
            Assert.IsFalse(target.ModelState.IsValid);

        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenNoError_EditUserAndReturnIndexView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            var editedUser = userRepository.GetUserByID(1);
            editedUser.FirstName = "Edited_First_Name";
            editedUser.LastName = "Edited_Last_Name";
            editedUser.Email = "Edited_Email@email.com";

            // Act
            RedirectToRouteResult actual = target.Edit(editedUser) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
            Assert.IsNotNull(editedUser.DateModified);
            Assert.IsNull(editedUser.IsDeleted);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenError_ModelStateNotValid_ReturnEditView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            var editedUser = userRepository.GetUserByID(1);
            editedUser.FirstName = "";
            editedUser.LastName = "";
            editedUser.Email = "";
            //TODO: it shows that modelstate is valid, throws an exception when saving

            // Act
            ViewResult actual = target.Edit(editedUser) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", actual.ViewName);
            Assert.IsFalse(target.ModelState.IsValid);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenError_ConcurrencyException_ReturnEditView()
        {
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void TestEdit_WhenError_OtherException_ReturnEditView()
        {
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod]
        public void TestDelete_ReturnDeleteView()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            // Act
            ViewResult actual = target.Delete();

            // Assert
            Assert.AreEqual("Delete", actual.ViewName);
        }

       //TODO: test actual deleting

        /// <summary>
        ///A test for Problem
        ///</summary>
        [TestMethod]
        public void TestProblem_ReturnError()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            UserController target = new UserController(unitOfWork);

            ViewResult expected = new ViewResult();
            expected.ViewName = "Error";

            ViewResult actual = target.Problem() as ViewResult;
            Assert.AreEqual(expected.ViewName, actual.ViewName);
        }

    }
}
