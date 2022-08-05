var builder = WebApplication.CreateBuilder(args);
var app = APISetup.Setup(builder);

string dbConnectionString = builder.Configuration["connectionString"];
SqlConnection connectionString = new SqlConnection(dbConnectionString);
connectionString.Open();

// HEALTHCHECK
app.MapHealthChecks("/healthcheck/");

// CLIENTES 
ClientController.ActivateEndpoints(app: app, connectionString: connectionString, builder: builder);

// APOLICES 
PolicyController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// COBERTURAS
CoverageController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// OCORRENCIAS
IncidentController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// TERCEIRIZADOS
OutsourcedController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

// USUARIOS
UserController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString, builder: builder);

// VEICULOS
VehicleController.ActivateEndpoints(app: app, dbConnectionString: dbConnectionString);

app.Run();

