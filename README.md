# Labo's Server Side Advanced (NMCT semester 4)

## Recipes

### New project

Create a new *ASP.NET MVC 5* project, choose *MVC* and check *Web API* and *Unit Test*

### Models

Example model:

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    namespace SalesWeb.Models
    {
        public class Inventory
        {
            public int LocationID {get; set;}             
            public string Row { get; set; }
            public int Position { get; set; }

            [Display(Name = "Quantities in stock")]
            public int Count { get; set; }
            public DateTime TransactionDate { get; set; }

            public int ProductID { get; set; }
            public virtual Product Product { get; set; }
        }
    }

### Entity Framework contexts

Custom *Context*s should inherit from `DbContext`.

    public class ScoreContext : DbContext {
        public DbSet<Score> Score { get; set; }
        
        // ...
    }

When setting up a connection string, it **should have the same name as the context class**:

    <connectionStrings>
        <add name="ScoreContext" connectionString="data source=.;Initial Catalog=ScoreApp;Integrated Security=SSPI" providerName="System.Data.SqlClient"/>
    </connectionStrings>

### Entity Framework migrations

In the *Package Manager Console*:

    > enable-migrations -contexttypename Scores.Models.DAL.ScoreContext

This will create a configuration file, which will contain a `Seed` method that you can use to set up your database. This file's location is `Migrations/Configuration.cs`.

    > add-migration "Migrationname"
    > update-database

### Add a controller

Right-click on the *Controllers* folder, choose *Add controller*.

### Add a View for a method

Right-click in the method, choose *Add View...*.

### Creating a repository

Create a folder `DAL` under `Models`. Place your repositories in there. An examle repository method looks like this:

        public List<Competition> GetCompetitions()
        {
            using (ScoreContext context = new ScoreContext())
            {
                var query = (from c in context.Competition.Include(c => c.Country) select c);
                return query.ToList<Competition>();
            }
        }

### Specifying a model in the View

As the first line in the View, use `@model` with the fully qualified class name:

    @model IEnumerable<NMCT.Scores.Models.Competition> 

### Creating roles in the Seed method

        IdentityResult roleResult;

        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        if (!roleManager.RoleExists(ApplicationRoles.ADMINISTRATOR))
        {
            roleResult = roleManager.Create(new IdentityRole(ApplicationRoles.ADMINISTRATOR));
        }

        if (!context.Users.Any(u => u.Email.Equals("admin@dev.null")))
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser() {
                Name = "Administrator",
                FirstName = "Root",
                Email = "admin@dev.null",
                UserName = "admin@dev.null",
                Address = "Graaf Karel De Goedlaan 1",
                City = "Kortrijk",
                ZipCode = "8500"
            };

            manager.Create(user, "-Password1");
            manager.AddToRole(user.Id, ApplicationRoles.ADMINISTRATOR);
        }

### Generic repository

Here's the interface:

    public interface IGenericRepository<TEntity>
      where TEntity : class
    {
        IEnumerable<TEntity> All();
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        TEntity GetByID(object id);
        TEntity Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void SaveChanges();
    }

And here's the implementation:

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        internal ApplicationDbContext context;
        internal DbSet<TEntity> dbSet;


        public GenericRepository()
        {
            this.context = new ApplicationDbContext();
            this.dbSet = context.Set<TEntity>();
        }

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> All()
        {
            return dbSet;
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
    }

Example usage:

    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {

        public List<Device> GetDevices()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from c in context.Devices select c);
                return query.ToList<Device>();
            }
        }
        
        public Device GetDevice(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from c in context.Devices where c.ID == id select c);
                return query.Single<Device>();
            }
        }
    }

### Services

Create a new folder *Services* in the root of your project. Inject (interfaces extracted from) repositories through the constructor.

    public class ExampleService {
        private IGenericRepository<IExampleRepository> exampleRepo = null;
        
        public ExampleService(IGenericRepository<IExampleRepository> exampleRepo) {
            this.exampleRepo = exampleRepo;
        }
    }

Generate a (public) interface out of the service. Use services in all controllers instead of repositories.

### Dependency injection

Use NuGet to install `Unity.Mvc5`. Then register it in `Global.asax.cs`:

    protected void Application_Start() {
        AreaRegistration.RegisterAllAreas();
        UnityConfig.RegisterComponents();
        // ...
    }

You can register types to be injected in the `App_Start/UnityConfig.cs` file:

    public static void RegisterComponents() {
        var container = new UnityContainer();
        
        container.RegisterType<IGenericRepository<Score>, GenericRepository<Score>>();
        container.RegisterType<IExampleService, ProductService>();
        
        DependencyResolver.SetResolver(new UnityDependencyResolver(container));
    }

Now inject service into the constructor of controllers:

    private IProductService ps;
    
    public ExampleController(IProductService ps) {
        this.ps = ps;
    }

### Adding unit tests

Right-click on your solution, choose *Add* > *New project* > *Unit Test Project*. Add the following NuGet dependencies: ASP.NET MVC 5, ASP.NET Identity EntityFramework, EntityFramework.


### Unit test database

Add a folder *Database* with a file *SetupDatabase.cs* to the test project. Inherit from `DropCreateDatabaseAlways<T>`

    public class SetupDatabase : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            FillData(context);
        }

        private void FillData(ApplicationDbContext context)
        {
            // copy the body of your Seed method in here (horrible right?)
        }
    }

Add the correct connection string in the `App.config` of the test project: use the same name, but create a different database (and point the `Initial Catalog` to it, too).

### Initialize tests

In your test class, create a `[TestInitialize]` method that will prep before the tests:

    [TestClass]
    public class IndexIntegrationTest
    {
        private DevicesController controller;
        private IProductService productService;
        private IGenericRepository<OS> repoOs;
        private IGenericRepository<ProgrammingFramework> repoFramework;
        private IDeviceRepository repoDevice;

        [TestInitialize]
        public void Setup()
        {
            new SetupDatabase().InitializeDatabase(new Models.ApplicationDbContext());

            repoDevice = new DeviceRepository();
            repoFramework = new GenericRepository<ProgrammingFramework>();
            repoOs = new GenericRepository<OS>();
            productService = new ProductService(repoOs, repoFramework, repoDevice);
            controller = new DevicesController(productService);
        }

    }

### Create a test method

Example:

    [TestMethod]
    public void TestMethod1()
    {
        ViewResult result = (ViewResult)controller.Index();
        IEnumerable<Device> devices = result.Model as IEnumerable<Device>;

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Device>));
        Assert.AreEqual(5, devices.Count());
    }

## Doelstellingen

* Week 1
 * Herhaling ASP.NET MVC semester 3
 * Leren werken met Entity Framework 6 (EF6)
 * Basis LINQ query’ schrijven. 
* Week 2
 * Herhaling ASP.NET MVC Semester 3 (controllers, presentation models, ASP.NET Identity)
 * Opzetten modellen op basis van een korte project omschrijving
 * Opzetten van Entity Framework
 * Opvullen test data in de database op basis van Entity Framework `Seed()`
 * Opvullen van 2 test gebruiker op basis van de Seed methode
 * Zelfstandig toepassen van bootstrap template
* Week 3
 * Zelfde als week 2

